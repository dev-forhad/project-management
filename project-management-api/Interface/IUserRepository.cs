using project_management_api.Model;
using System.Collections.Generic;

namespace project_management_api.Interface
{

    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<List<User>> GetUsersByIds(IEnumerable<string> ids);
        Task<User> FindByName(string username);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(int id);
        Task<bool> UserExists(string id);
    }
}
