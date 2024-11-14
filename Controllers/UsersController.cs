using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;


[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager; // Injete o RoleManager
    private readonly JwtTokenService _jwtTokenService; // Injeção do JwtTokenService


    public UsersController(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, JwtTokenService jwtTokenService)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager; // Inicialize o RoleManager
        _jwtTokenService = jwtTokenService; // Inicializa o JwtTokenService
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Criação do usuário
            var result = await _userManager.CreateAsync(user, user.PasswordHash);

            if (result.Succeeded)
            {
                // Verifica se a role "User" existe, se não, cria ela (RoleManager injetado no controller)
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }

                // Atribui a role "User" ao usuário recém-criado
                await _userManager.AddToRoleAsync(user, "User");

                return Ok(new { Message = "Usuário criado com sucesso", User = user });
            }

            return BadRequest(result.Errors);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }



    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.FindByEmailAsync(loginModel.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginModel.Password))
        {
            return Unauthorized(new { Message = "Credenciais inválidas" });
        }

        // Obtenha os papéis do usuário
        var roles = await _userManager.GetRolesAsync(user);
        if (!roles.Contains("User"))
        {
            return Unauthorized(new { Message = "User does not have the required role." });
        }
        // Gera o token JWT
        var token = _jwtTokenService.GenerateToken(user, roles);

        return Ok(new { Token = token });
    }



    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
        if (result.Succeeded)
        {
            return Ok("Password reset successful.");
        }

        return BadRequest(result.Errors);
    }


    public static string HashPassword(string password)
    {
        using (var hmac = new HMACSHA256())
        {
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }
    }


    [HttpPost("logout")]
    public IActionResult Logout()
    {
        // No caso do JWT, o logout pode simplesmente ser a remoção do token do lado do cliente
        return Ok("Logout successful");
    }

    [HttpGet("{email}/roles")]
    public async Task<IActionResult> GetUserRoles(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var roles = await _userManager.GetRolesAsync(user);
        return Ok(roles);
    }



    [HttpPost("{email}/roles/{role}")]
    public async Task<IActionResult> AssignRoleToUser(string email, string role)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var result = await _userManager.AddToRoleAsync(user, role);
        if (result.Succeeded)
        {
            return Ok("Role assigned successfully.");
        }

        return BadRequest(result.Errors);
    }

    [HttpGet("{email}")]
[Authorize(Roles = "User,Admin")]
public async Task<IActionResult> GetUserByEmail(string email)
{
    var user = await _userManager.FindByEmailAsync(email);
    if (user == null)
    {
        return NotFound("User not found.");
    }

    var roles = await _userManager.GetRolesAsync(user);
    Console.WriteLine($"Usuário: {user.Email}, Papeis: {string.Join(",", roles)}");

    return Ok(user);
}









}

