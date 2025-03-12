namespace UFID_Reader.Models;

public class Course
{
    public string course_code { get; set; }
    public string course_name { get; set; }
    public string class_number { get; set; }
    public string instructors { get; set; }
    public int meet_no { get; set; }
    public string meet_days { get; set; }
    public string meet_time_begin { get; set; }
    public string meet_time_end { get; set; }
    public string meet_room_code { get; set; }
}