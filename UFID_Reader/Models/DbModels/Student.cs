﻿#nullable disable

namespace UFID_Reader.Models.DbModels;

public class Student
{
    public required string UFID { get; set; }
    public string ISO { get; set; } 
    public string Full_Name { get; set; } 
    public string Email { get; set; }
    public string Course1 { get; set; }
    public string Course2 { get; set; }
    public string Course3 { get; set; }
    public string Course4 { get; set; }
    public string Course5 { get; set; }
} 