#nullable disable

namespace UFID_Reader.Models.DbModels;

public class Timesheet
{
    public string Ufid{ get; set; } 
    public string Full_name { get; set; }
    public string Course_code { get; set; }
    public string Section_num { get; set; }
    public string Date { get; set; }
    public string Swipe_time { get; set; }
}