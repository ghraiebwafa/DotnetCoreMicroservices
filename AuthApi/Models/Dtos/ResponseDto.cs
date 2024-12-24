namespace AuthApi.Models.Dtos;

public class ResponseDto
{
    public object? Result { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = "";
}