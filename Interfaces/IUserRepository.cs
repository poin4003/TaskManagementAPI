using api.Dtos.User;
using api.Models;
using api.Helpers;

namespace api.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync(UserQueryObject query);
    Task<User?> GetByIdAsync(int id);
    Task<User> CreateAsync(User userModel);
    Task<User?> UpdateAsync(int id, User userModel);
    Task<User?> DeleteAsync(int id);
    Task<bool> UserExists(int id);
}