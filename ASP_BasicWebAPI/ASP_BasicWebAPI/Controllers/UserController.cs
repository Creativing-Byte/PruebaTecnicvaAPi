﻿using ASP_BasicWebAPI.Dbcontext;
using ASP_BasicWebAPI.DTO;
using Basic_Web_Api.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ASP_BasicWebAPI.Controllers
{
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
