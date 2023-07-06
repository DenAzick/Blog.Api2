using Blog.Api.Models.IdentityModels;
using Blog.Api.Providers;
using BlogApi.Managers.UserManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager _userManager;
    private ILogger<AccountController> _logger;
    private readonly UserProvider _userProvider;

    public AccountController(UserManager userManager, 
        ILogger<AccountController> logger,
        UserProvider userProvider)
    {
        _userManager = userManager;
        _logger = logger;
        _userProvider = userProvider;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Register(model);
            return Ok(new UserModel(user));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while registering the user.");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _userManager.Login(model);

            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while logining the user.");
        }

        
    }


    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        try
        {
            var userId = _userProvider.UserId;

            var user = await _userManager.GetUser(userId);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(new UserModel(user));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the user profile.");
        }
    }


    [HttpPost("GetUser")]
    public async Task<IActionResult> GetUser(string userName)
    {
        try
        {
            var user = await _userManager.GetUser(userName);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new UserModel(user));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the user.");
        }
    }

}