namespace Basic_Web_Api.Controllers
{
    using Basic_Web_Api.DbContext;
    using Basic_Web_Api.DTO;
    using Basic_Web_Api.Entity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _AppDebContext;

        public UsersController(AppDbContext context)
        {
            _AppDebContext = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest("El nombre y el correo son obligatorios");
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email
            };

            _AppDebContext.Users.Add(user);
            await _AppDebContext.SaveChangesAsync();

            return Ok(new { message = "Se ha registrado al usuario correctamente", userId = user.Id });
        }
    }

}
