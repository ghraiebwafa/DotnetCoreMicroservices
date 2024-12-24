using AuthApi.Models.Dtos;
using Microsoft.AspNetCore.Identity.Data;

namespace AuthApi;

public interface IAuthService
{
    Task<string> Register(RegistrationDto registrationDto);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<bool>AssignRole(string email, string roleName);
}