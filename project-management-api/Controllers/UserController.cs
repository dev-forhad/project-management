using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_management_api.Interface;
using project_management_api.Model;

namespace project_management_api.Controllers
{

   
    [Route("api/")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _context;

        public UserController(IUserRepository context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.GetUsers();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
               await _context.UpdateUser(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            await _context.AddUser(user);

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            await _context.DeleteUser(id);

            return user;
        }

        private async Task<bool> UserExists(string id)
        {
            return await _context.UserExists(id);
        }
    }
}
