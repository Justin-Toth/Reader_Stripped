using System;
using System.Threading.Tasks;

namespace UFID_Reader.Services;

public interface ITimesheetService
{
    Task<int> RegisterTimesheetEntry(string ufid, string full_name, string course_code, string section_num, string date,
        string swipe_time);
}

public class TimesheetService(IDbService dbService) : ITimesheetService
{
    public async Task<int> RegisterTimesheetEntry(string ufid, string full_name, string course_code, string section_num,
        string date, string swipe_time)
    {
        const string sql = "INSERT INTO timesheets (ufid, full_name, course_code, section_num, date, swipe_time) VALUES (@ufid, @full_name, @course_code, @section_num, @date, @swipe_time)";
        return await dbService.ExecuteAsync(sql, new { ufid, full_name, course_code, section_num, date, swipe_time });
    }
}