using AuthApi.Data;
using AuthApi.Models;
using AuthApi.Models.Dtos;
using Microsoft.AspNetCore.Identity;

namespace AuthApi.Service;

public class AuthService:IAuthService
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager; 
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(AppDbContext db,IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    //------------Registration-------------------------------------------------
    public async Task<string> Register(RegistrationDto registrationDto)
    {
        ApplicationUser user = new()
        {
            UserName = registrationDto.Email,
            Email = registrationDto.Email,
            NormalizedEmail = registrationDto.Email.ToUpper(),
            Name = registrationDto.Name,
            PhoneNumber = registrationDto.PhoneNumber,
        };
        try
        {
            var result =  await _userManager.CreateAsync(user, registrationDto.Password);
            if (result.Succeeded)
            {
               var userToReturn = _db.ApplicationUsers.First(u =>u.UserName == registrationDto.Email);
               UserDto userDto = new()
               {
                   Email = userToReturn.Email,
                   ID = userToReturn.Id,
                   Name = userToReturn.Name,
                   PhoneNumber = userToReturn.PhoneNumber,
               };
               return "";

            }
            else
            {
                return result.Errors.FirstOrDefault()?.Description;
            }
        }
        catch (Exception e)
        {
            // ignored
        }

        return "Error!!";
    }
//-----------------Login-----------------------------------------------
    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.Username.ToLower());

        bool isValid = await _userManager.CheckPasswordAsync(user,loginRequestDto.Password);

        if(user==null || isValid == false)
        {
            return new LoginResponseDto() { User = null,Token="" };
        }

        //if user was found , Generate JWT Token
         var token=  _jwtTokenGenerator.GenerateToken(user);

        UserDto userDTO = new()
        {
            Email = user.Email,
            ID = user.Id,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber
        };

        LoginResponseDto loginResponseDto = new LoginResponseDto()
        {
            User = userDTO,
            Token = token
        };

        return loginResponseDto;
    }

    public async Task<bool> AssignRole(string email, string roleName)
    {
        var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        if (user != null)
        {
            if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
            }
            await _userManager.AddToRoleAsync(user, roleName);
            return true;
        }
        return false;
    }
}