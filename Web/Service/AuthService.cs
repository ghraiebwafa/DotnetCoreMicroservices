using Web.Models;
using Web.Service.IService;
using Web.Utility;

namespace Web.Service;

public class AuthService:IAuthService
{
    private readonly IBaseService _baseService;
    public AuthService(IBaseService baseService)
    {
        _baseService = baseService;
    }
    public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequest)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticData.ApiType.Post,
            Data = loginRequest,
            Url = StaticData.AuthAPIBase+"/api/auth/login",
        });
    }

    public async Task<ResponseDto?> RegisterAsync(RegistrationDto registrationDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticData.ApiType.Post,
            Data = registrationDto,
            Url = StaticData.AuthAPIBase+"/api/auth/register",
        });
    }

    public  async Task<ResponseDto?> AssignRoleAsync(RegistrationDto registrationDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticData.ApiType.Post,
            Data = registrationDto,
            Url = StaticData.AuthAPIBase+"/api/auth/AssignRole",
        });
    }
}