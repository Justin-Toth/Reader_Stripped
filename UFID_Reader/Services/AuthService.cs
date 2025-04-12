using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFID_Reader.Models;
using UFID_Reader.Models.DbModels;

namespace UFID_Reader.Services;

public interface IAuthService
{ 
    Task<AuthResult> AuthenticateSwipeAsync(string scannerInput, string mode, string serialNumber);
}

public class AuthService(
    IStudentService studentService,
    IKioskService kioskService,
    IScheduleService scheduleService,
    IDateTimeService dateTimeService) : IAuthService
{
    public async Task<AuthResult> AuthenticateSwipeAsync(string scannerInput, string mode, string serialNumber)
    {
        var student = await studentService.GetStudentByUfidOrIsoAsync(scannerInput);
        if (student == null)
            return AuthResult.Failure($"Student not found for id: {scannerInput}");

        var kiosk = await kioskService.GetKioskBySerialNumberAsync(serialNumber);
        if (kiosk == null)
            return AuthResult.Failure($"Kiosk not found for serial number: {serialNumber}");

        var sectionNumbers = studentService.GetSectionNumbers(student);
        var roomNumber = kiosk.room_num;
        var currentDay = dateTimeService.GetCurrentDay();
        var currentTime = dateTimeService.GetCurrentTime();
        
        DebugPrintInfo1(mode, student, kiosk, roomNumber, sectionNumbers);

        return mode switch
        {
            "class" => await AuthenticateClassMode(sectionNumbers, roomNumber, currentDay, currentTime),
            "exam" => await AuthenticateExamMode(sectionNumbers, roomNumber, currentDay, currentTime),
            _ => AuthResult.Failure("Invalid mode")
        };
    }

    private async Task<AuthResult> AuthenticateClassMode(List<string> sectionNumbers, string roomNumber,
        string currentDay, TimeSpan currentTime)
    {
        var courses = await scheduleService.GetCoursesBySectionNumbersAsync(sectionNumbers);
        if (courses == null || courses.Count == 0)
            return AuthResult.Failure($"Courses not found for section numbers: {string.Join(", ", sectionNumbers)}");
        
        DebugPrintInfo2(courses);

        // Find the first course that matches the room number, day, and time
        var validCourses = courses
            .Where(course => course.meet_room_code == roomNumber && course.meet_days.Contains(currentDay))
            .Where(course =>
            {
                var startTime = DateTime.Parse(course.meet_time_begin).TimeOfDay;
                var endTime = DateTime.Parse(course.meet_time_end).TimeOfDay;
                return currentTime >= startTime && currentTime <= endTime;
            })
            .ToList();
        
        DebugPrintInfo3(validCourses);
            
        return validCourses.Count != 0
            ? AuthResult.Success()
            : AuthResult.Failure("Student is not scheduled for a class at this time and location");
    }
    
    private async Task<AuthResult> AuthenticateExamMode(List<string> sectionNumbers, string roomNumber,
        string currentDay, TimeSpan currentTime)
    {
        var exams = await scheduleService.GetExamsByRoomNumberAsync(roomNumber);
        if (exams == null || exams.Count == 0)
            return AuthResult.Failure($"Exams not found for room number: {roomNumber}");

        DebugPrintInfo4(exams);

        // Find the first exam that matches the section numbers, day, and time
        foreach (var exam in exams)
        {
            var examSections = exam.sections.Split(',');
            var hasMatchingSection = examSections.Intersect(sectionNumbers).Any();
            if (!hasMatchingSection) continue;

            var examDate = DateTime.Parse(exam.date);
            var examStart = DateTime.Parse(exam.start_time).TimeOfDay;
            var examEnd = DateTime.Parse(exam.end_time).TimeOfDay;

            if (examDate == DateTime.Today && currentTime >= examStart && currentTime <= examEnd)
            {
                DebugPrintInfo5(exam);
                return AuthResult.Success();
            }
        }

        return AuthResult.Failure("Student is not scheduled for an exam at this time and location");
    }
    
    
    // Debugging methods to print info to console.
    // These methods are not necessary for the actual functionality of the program.
    // Calls can be removed/commented out in the actual code.
    private static void DebugPrintInfo1(string mode, Student student, Kiosk kiosk, string roomNumber, List<string> sectionNumbers)
    {
        Console.WriteLine();
        Console.WriteLine($"Mode: {mode}");
        Console.WriteLine($"Student: {student.Full_Name}");
        Console.WriteLine($"UFID: {student.UFID}");
        Console.WriteLine($"Kiosk: {kiosk.serial_num}");
        Console.WriteLine($"Kiosk Room Number: {roomNumber}");
        Console.WriteLine($"Section Numbers: {string.Join(", ", sectionNumbers)}");
        Console.WriteLine();
    }

    private static void DebugPrintInfo2(List<Course> courses)
    {
        Console.WriteLine("Printing all course information:");
        foreach (var course in courses)
        {
            Console.WriteLine($"Course: {course.course_code}");
            Console.WriteLine($"Meet Days: {course.meet_days}");
            Console.WriteLine($"Meet Time Begin: {course.meet_time_begin}");
            Console.WriteLine($"Meet Time End: {course.meet_time_end}");
            Console.WriteLine($"Meet Room Number: {course.meet_room_code}");
            Console.WriteLine();
        }
    }

    private static void DebugPrintInfo3(List<Course> validCourses)
    {
        if (validCourses.Count == 0) return;
        Console.WriteLine("Found valid course:");
        Console.WriteLine($"Course: {validCourses[0].course_code}");
        Console.WriteLine($"Meet Days: {validCourses[0].meet_days}");
        Console.WriteLine($"Meet Time Begin: {validCourses[0].meet_time_begin}");
        Console.WriteLine($"Meet Time End: {validCourses[0].meet_time_end}");
        Console.WriteLine($"Meet Room Number: {validCourses[0].meet_room_code}");
        Console.WriteLine();
    }

    private static void DebugPrintInfo4(List<Exam> exams)
    {
        Console.WriteLine("Printing all exam information:");
        foreach (var exam in exams)
        {
            Console.WriteLine($"Exam: {exam.course_code}");
            Console.WriteLine($"Sections: {exam.sections}");
            Console.WriteLine($"Date: {exam.date}");
            Console.WriteLine($"Start Time: {exam.start_time}");
            Console.WriteLine($"End Time: {exam.end_time}");
            Console.WriteLine($"Room: {exam.room}");
            Console.WriteLine();
        }
    }

    private static void DebugPrintInfo5(Exam exam)
    {
        Console.WriteLine("Found valid exam:");
        Console.WriteLine($"Exam: {exam.course_code}");
        Console.WriteLine($"Sections: {exam.sections}");
        Console.WriteLine($"Date: {exam.date}");
        Console.WriteLine($"Start Time: {exam.start_time}");
        Console.WriteLine($"End Time: {exam.end_time}");
        Console.WriteLine($"Room: {exam.room}");
        Console.WriteLine();
    }
}

/*
    var result = false;

        if (mode == "class")
        {
            
        
        }
        else if (mode == "exam")
        {
            var exams = await GetExamsByRoomNumberAsync(roomNumber);
            if (exams == null)
            {
                Console.WriteLine("Exams not found");
                return false;
            }

            Console.WriteLine($"Exams: {string.Join(", ", exams.Select(exam => exam.course_code))}");

            foreach (var exam in exams)
            {
                Console.WriteLine();
                Console.WriteLine($"Exam For Class: {exam.course_code}");

                foreach (var section in exam.sections.Split(','))
                {
                    Console.WriteLine($"Section: {section}");

                    if (!sectionNumbers.Contains(section)) continue;
                    var examDate = DateTime.Parse(exam.date);
                    var examStart = DateTime.Parse(exam.start_time).TimeOfDay;
                    var examEnd = DateTime.Parse(exam.end_time).TimeOfDay;

                    result = examDate == DateTime.Today && currentTime >= examStart && currentTime <= examEnd;

                    // Debug: print all the data to console
                    Console.WriteLine();
                    Console.WriteLine($"Student: {student.Full_Name}");
                    Console.WriteLine($"UFID: {student.UFID}");
                    Console.WriteLine($"Kiosk: {kiosk.serial_num}");
                    Console.WriteLine($"Kiosk Room Number: {roomNumber}");
                    Console.WriteLine($"Section Numbers: {string.Join(", ", sectionNumbers)}");
                    Console.WriteLine();

                    Console.WriteLine($"Exam For Class: {exam.course_code}");
                    Console.WriteLine($"Exam Date: {exam.date}");
                    Console.WriteLine($"Exam Start: {exam.start_time}");
                    Console.WriteLine($"Exam End: {exam.end_time}");
                    Console.WriteLine($"Exam Room Number: {exam.room}");
                    Console.WriteLine();

                    Console.WriteLine($"Current Day: {currentDay}");
                    Console.WriteLine($"Current Time: {currentTime}");
                    Console.WriteLine($"Result: {result}");
                }
            }

        }
        else
        {
            Console.WriteLine("Invalid mode");
            return false;
        }

        return result;
    }
}

/*
    Mode 0 for now

        get student by ID
            extract {section numbers} from student

        get courses by section numbers
            extract meeting information{meet days, time_start, time_end, room number} from courses

        get kiosk by serial number
            extract {room number} from kiosk

        compare stuff to get result:
            - get current {time, day}
            - compare {time, day} to {meet days, time_start, time_end}

                - find room num that matches kiosk room num
                    - compare {current day} to {meet days}
                        - if they match
                            - compare {current time} to {time_start, time_end}
                                - if they match
                                    - {true}
                                - else
                                    - {false}
                        - else
                            - {false}

        result
            - {true} -> {success}
            - {false} -> {failure}


    Mode 1:

        get student by ID
            extract {section numbers} from student

        get kiosk by serial number
            extract {room number} from kiosk

        get exams by room number
            extract exam info {sections, date, start, end} from exams

        compare stuff to get result:
            - get current {time, day}

            - compare exams {sections} to student {section numbers}
                - if they match
                    - compare {time, day} to {date, start, end}
                        - if they match
                            - {true}
                        - else
                            - {false}
                - else
                    - {false}z
 */


