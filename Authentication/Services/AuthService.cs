using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;

namespace Authentication.Services;

public interface IAuthService
{
    UserModel Authenticate(string username);
    Task<bool> CheckLoginAsync(string email, string password);
    Task LogoutAsync();
}

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(HttpClient httpClient
        , IHttpContextAccessor httpContextAccessor
        )
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public UserModel Authenticate(string username)
    {
        //    var user = context.Users.SingleOrDefault(u => u.UserName == username);

        //    if (user == null)
        //        return null;

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(identitySettings.Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //    {
        //            new Claim(ClaimTypes.Name, user.UserName),
        ////For claim-based authorization
        //new Claim(ClaimTypes.Role, user.Role)
        //    }),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    user.Token = tokenHandler.WriteToken(token);

        return new()
        {
            Password = "xyz"
        };
    }

    //This will only work on an SSR page, See Updating headers in a Form in OneNote (error "Headers are read-only, response has already started")
    public async Task<bool> CheckLoginAsync(string email, string password)
    {

        //Check email and password

        //Get user data from Db:
        var user = new UserModel()
        {
            Id = new Guid("66909134-719a-4936-9feb-501a585f9b99"),
            FirstName = "John",
            LastName = "Wayne",
            Email = "Marion@Robert.Morrison",
        };

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //new Claim(ClaimTypes.Actor, user.ClientId.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName ?? user.Email!),
            new Claim(ClaimTypes.Role, Constants.RoleUser),
            new Claim(ClaimTypes.Email, email!),
        };

        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.Now.AddDays(1),
            IsPersistent = true,
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        return true;
    }

    public async Task LogoutAsync()
    {
        await _httpContextAccessor.HttpContext!.SignOutAsync();
    }

    public static Guid GetUserID(HttpContext httpContext)
    {
        string x = httpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        return Guid.Parse(x);
    }


}