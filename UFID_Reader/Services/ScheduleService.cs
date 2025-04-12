using System.Collections.Generic;
using System.Threading.Tasks;
using UFID_Reader.Models.DbModels;

namespace UFID_Reader.Services;

public interface IScheduleService
{
    Task<List<Course>?> GetCoursesBySectionNumbersAsync(List<string> sectionNumbers);
    Task<List<Exam>?> GetExamsByRoomNumberAsync(string roomNumber);
}

public class ScheduleService(IDbService dbService) : IScheduleService
{
    public async Task<List<Course>?> GetCoursesBySectionNumbersAsync(List<string> sectionNumbers)
    {
        const string sql = "SELECT * FROM courses WHERE class_number IN @sectionNumbers";
        var rows = await dbService.QueryAsync<Course>(sql, new { sectionNumbers });
        return rows;
    }

    public async Task<List<Exam>?> GetExamsByRoomNumberAsync(string roomNumber)
    {
        const string sql = "SELECT * FROM exams WHERE room = @roomNumber";
        var rows = await dbService.QueryAsync<Exam>(sql, new { roomNumber });
        return rows;
    }
}