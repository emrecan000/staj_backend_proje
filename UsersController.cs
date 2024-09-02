using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using staj_backend_proje.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;

namespace staj_backend_proje.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    [Authorize(Roles = "Librarian,Admin")] 
    public class UsersController : ControllerBase
    {
        private readonly BookContext _context;

        public UsersController(BookContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            return _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public ActionResult<User> CreateUser(CreateUserDto newUser)
        {
            var user = new User
            {
                Email = newUser.Email,
                Password = newUser.Password,
                RoleId = newUser.RoleId
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = newUser.RoleId
            };

            _context.UserRoles.Add(userRole);
            _context.SaveChanges();

            user = _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefault(u => u.Id == user.Id);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
