namespace UFID_Reader.Models;

public class AuthResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }

    public static AuthResult Success() => new AuthResult { IsSuccess = true, Message = "Authentication successful." };
    public static AuthResult Failure(string reason) => new AuthResult { IsSuccess = false, Message = reason };
}