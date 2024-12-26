using Web.Models;

namespace Web.Service.IService;

public interface IAuthService
{
    Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequest);
    Task<ResponseDto?> RegisterAsync(RegistrationDto  registrationDto);
    Task<ResponseDto?> AssignRoleAsync(RegistrationDto  registrationDto);
}