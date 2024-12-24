using Microsoft.AspNetCore.Identity;

namespace AuthApi.Models;

public class ApplicationUser:IdentityUser
{
    public string? Name { get; set; }
}