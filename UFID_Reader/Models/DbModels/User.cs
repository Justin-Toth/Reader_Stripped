#nullable disable

namespace UFID_Reader.Models.DbModels;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Full_Name { get; set; }
    public bool Is_Admin { get; set; }
    public string Password { get; set; }
}
