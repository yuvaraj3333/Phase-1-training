using API_Demo.Context;
using API_Demo.Models;
using API_Demo.DTOs;
using API_Demo.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace API_Demo.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : Controller
    {
        private readonly AppDBContext _dbContext;

        public UserController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser([FromBody] UserModel user)
        {
            var hasher = new PasswordHasher<UserModel>();
            user.Password = hasher.HashPassword(user, user.Password);

            _dbContext.UserDB.Add(user);
            _dbContext.SaveChanges();

            return Ok(UserMapper.ToDTO(user));
        }

        [HttpGet("GetUsers")]
        public IEnumerable<UserDTO> GetUsers()
        {
            //return _dbContext.UserDB.ToList();
            return _dbContext.UserDB.Select(u => UserMapper.ToDTO(u)).ToList();
        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _dbContext.UserDB.Find(id);
            if (user == null)
            {
                return NotFound($"User with Id {id} not found.");
            }

            _dbContext.UserDB.Remove(user);
            _dbContext.SaveChanges();

            return Ok($"User with Id {id} deleted successfully.");
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            var user = _dbContext.UserDB.SingleOrDefault(u => u.Email == login.Email);

            if (user == null)
                return Unauthorized("Invalid email or password");

            var hasher = new PasswordHasher<UserModel>();
            var result = hasher.VerifyHashedPassword(user, user.Password, login.Password);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid email or password");

            return Ok(new
            {
                message = "Login successful",
                user = UserMapper.ToDTO(user)
            });
        }
    }
}
