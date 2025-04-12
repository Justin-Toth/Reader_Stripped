using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFID_Reader.Models.DbModels;

namespace UFID_Reader.Services;

public interface IStudentService
{
    Task<Student?> GetStudentByUfidOrIsoAsync(string id);
    List<string> GetSectionNumbers(Student student);
}

public class StudentService(IDbService dbService) : IStudentService
{
    public async Task<Student?> GetStudentByUfidOrIsoAsync(string id)
    {
        if (id.Length == 16)
        {
            const string sql = "SELECT * FROM students WHERE iso = @id";
            return await dbService.QuerySingleAsync<Student>(sql, new { id });
        }
        else
        {
            const string sql = "SELECT * FROM students WHERE ufid = @id";
            return await dbService.QuerySingleAsync<Student>(sql, new { id });
        }
    }
    
    public List<string> GetSectionNumbers(Student student)
    {
        return new List<string?>
        {
            student.Course1,
            student.Course2,
            student.Course3,
            student.Course4,
            student.Course5
        }.Where(course => course != null).Select(course => course!).ToList();
    }
}