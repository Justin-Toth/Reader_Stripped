using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace UFID_Reader.Services;

// NOTE: This is almost a direct port of the python validation.py script
//       This needs a major refactor to improve function and readability
//       This is a temporary solution to get the app working
//       
// SECONDARY NOTE: We need to update the api to allow us to improve the calling structure of this service



public interface IValidationService
{
    // Task<bool> Validate(string scannerInput);   
    
    Task<ValidationResult> Validate(int mode, string serialNum, string scannerInput);
}

public class ValidationService(HttpClient httpClient) : IValidationService
{
    private readonly string _baseUrl = "https://gatorufid.pythonanywhere.com/";
    
    public static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    private async Task<HttpResponseMessage> WebApiGetRequest(string page, Dictionary<string, string?> parameters)
    {
        var url = $"{_baseUrl}{page}";
        var response = await httpClient.GetAsync($"{url}?{string.Join("&", parameters.Select(kv => $"{kv.Key}={kv.Value}"))}");
        return response;
    }
    
    public async Task<ValidationResult> Validate(int mode, string serialNum, string? scannerInput)
    {
        Console.WriteLine("Scanner input: " + scannerInput);
        
        string? ufid = null;
        string? iso = null;
        
        if (scannerInput is { Length: 16 })
            iso = scannerInput;
        else
            ufid = scannerInput;
        
        
        Dictionary<string, string?> parameters = new()
        {
            { "serial_num", serialNum },
            { "ufid", ufid },
            { "iso", iso }
        };

        var studentResponse = await WebApiGetRequest("roster", parameters);
        
        if (studentResponse.StatusCode != System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine("Student not found");
            return new ValidationResult(null, null, false);
        }
        
        
        var student = JObject.Parse(await studentResponse.Content.ReadAsStringAsync());
        var studentSecNums = new List<string>();
        for (int i = 4; i < 12; i++)
        {
            var secNum = student["student_data"]?[i]?.ToString();
            if (!string.IsNullOrEmpty(secNum))
            {
                studentSecNums.Add(secNum);
            }
        }
        
        ufid = student["student_data"]?[0]?.ToString();
        iso = student["student_data"]?[1]?.ToString();
        var firstName = student["student_data"]?[2]?.ToString();
        var lastName = student["student_data"]?[3]?.ToString();
        
        var roomResponse = await WebApiGetRequest("kiosks", new Dictionary<string, string?>
        {
            { "serial_num", serialNum }
        });

        if (roomResponse.StatusCode != System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine("Room not found");
            return new ValidationResult(null, null, false);
        }
        
        var room = JObject.Parse(await roomResponse.Content.ReadAsStringAsync())["room_num"]?.ToString();
        var now = DateTime.Now;
        var date = now.ToString("MM/dd/yyyy");
        var dayMap = new Dictionary<int, string> { { 0, "M" }, { 1, "T" }, { 2, "W" }, { 3, "R" }, { 4, "F" }, { 5, "S" } };
        var day = dayMap.ContainsKey((int)now.DayOfWeek) ? dayMap[(int)now.DayOfWeek] : null;

        if (day == null)
        {
            Console.WriteLine("Day not found");
            return new ValidationResult(null, null, false);
        }
        
        var currentTime = now.TimeOfDay;
        var paramsData = mode == 1
            ? new Dictionary<string, string> { { "serial_num", serialNum }, { "date", date } }
            : new Dictionary<string, string> { { "day", day }, { "roomCode", room! } };

        var endpoint = mode == 1 ? "exams" : "courses";
        var scheduleResponse = await WebApiGetRequest(endpoint, paramsData!);
        
        if (scheduleResponse.StatusCode != System.Net.HttpStatusCode.OK)
        {
            Console.WriteLine("Schedule not found");
            return new ValidationResult(null, null, false);
        }

        var results = JArray.Parse(await scheduleResponse.Content.ReadAsStringAsync());
        var isValid = false;
        var courses = new List<JToken>();
        if (courses == null) throw new ArgumentNullException(nameof(courses));
        var gracePeriod = TimeSpan.FromMinutes(15);
        
        foreach (var result in results)
        {
            var start = DateTime.Parse(result[6]!.ToString()).TimeOfDay;
            var end = DateTime.Parse(result[7]!.ToString()).TimeOfDay;

            if (mode != 1)
            {
                start = start.Subtract(gracePeriod);
                end = end.Add(gracePeriod);
            }

            if (start <= currentTime && currentTime <= end)
            {
                courses.Add(result);
            }
        }
        
        var matchFound = false;
            foreach (var course in courses)
            {
                var courseSecNums = mode == 1 ? course[3].ToString().Split(", ") : course[2].ToString().Split(", ");
                foreach (var studentSecNum in studentSecNums)
                {
                    if (Array.Exists(courseSecNums, secNum => secNum == studentSecNum))
                    {
                        var paramsPost = new Dictionary<string, string>
                        {
                            { "serial_num", serialNum },
                            { "ufid", ufid },
                            { "iso", iso },
                            { "first_name", firstName },
                            { "last_name", lastName },
                            { "course", course[0].ToString() },
                            { "class", studentSecNum },
                            { "time", now.ToString("yyyy-MM-dd HH:mm:ss") }
                        };

                        if (mode == 1)
                        {
                            paramsPost["instructor"] = course[2].ToString();
                            paramsPost["room_num"] = course[4].ToString();
                        }
                        else
                        {
                            paramsPost["instructor"] = course[3].ToString();
                            paramsPost["room_num"] = course[8].ToString();
                        }

                        await httpClient.PostAsync($"{_baseUrl}timesheet", new FormUrlEncodedContent(paramsPost));

                        isValid = true;
                        matchFound = true;
                        break;
                    }
                }

                if (matchFound)
                {
                    break;
                }
            }

            if (!matchFound)
            {
                isValid = false;
            }

            string fullName = firstName + " " + lastName;
            Console.WriteLine("Validation complete");
            return new ValidationResult(ufid, fullName, isValid);
    }








    /* Temperature Check for Validation

    public async Task<bool> Validate(string scannerInput)
    {
        Console.WriteLine("Validating...");

        await Task.Delay(TimeSpan.FromSeconds(3));

        var response = true;


        Console.WriteLine("Done validating \n");

        return response;
    }
    */
}


public record ValidationResult(
    string? Ufid,
    string? FullName,
    bool IsValid
);