using Microsoft.EntityFrameworkCore;
using project_management_api.Data;
using project_management_api.Interface;
using project_management_api.Model;
using System.Linq;

namespace project_management_api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> FindByName(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u=>u.UserName.Equals(username));
        }


        public async Task<User> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExists(string id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetUsersByIds(IEnumerable<string> ids)
        {
            return await _context.Users.Where(u => ids.Contains(u.Id)).ToListAsync();
        }

    }
}
