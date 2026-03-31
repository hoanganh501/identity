using Application.Interface;
using Application.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class AuthController: BaseApi
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(AuthRequest request)
        {
            return Ok(await _authService.LoginAsync(request));
        }
    }
}
