using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class UserController: BaseApi
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> GetUserAsync()
        {
            await Task.Delay(100);

            return Ok(new
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Email = "admin@gmail.com"
            });
        }
    }
}
