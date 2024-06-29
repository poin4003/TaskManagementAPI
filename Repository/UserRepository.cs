using api.Data;
using api.Dtos.User;
using api.Interfaces;
using api.Models;
using api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDBContext _context;
    public UserRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User userModel)
    {
        await _context.AddAsync(userModel);
        await _context.SaveChangesAsync();
        return userModel;
    }

    public async Task<User?> DeleteAsync(int id)
    {
        var userModel = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);

        if (userModel == null)
            return null;
        
        _context.Users.Remove(userModel);
        await _context.SaveChangesAsync();
        return userModel;
    }

    public async Task<List<User>> GetAllAsync(UserQueryObject query)
    {
        var users = _context.Users.Include(p => p.Todos).AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Name))
        {
            users = users.Where(p => p.Name.Contains(query.Name));
        }

        if (query.Birthday.HasValue)
        {
            var birthday = query.Birthday.Value.Date;
            users = users.Where(p => p.Birthday.Date == birthday);
        }

        if (!string.IsNullOrWhiteSpace(query.Email))
        {
            users = users.Where(p => p.Email.Contains(query.Email));
        }

        if (!string.IsNullOrEmpty(query.SortBy))
        {
            if (query.SortBy.Equals("AccessTime", StringComparison.OrdinalIgnoreCase))
            {
                users = query.IsDecsending ? users.OrderByDescending(p => p.AccessTime) : users.OrderBy(p => p.AccessTime);
            }
        }

        return await users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.Include(p => p.Todos).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<User?> UpdateAsync(int id, User userModel)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);

        if (existingUser == null) 
            return null;

        existingUser.Name = userModel.Name;
        existingUser.Birthday = userModel.Birthday;
        existingUser.Email = userModel.Email;
        existingUser.AccessTime = userModel.AccessTime;

        await _context.SaveChangesAsync();
        
        return existingUser;
    }

    public Task<bool> UserExists(int id)
    {
        return _context.Users.AnyAsync(p => p.Id == id);
    }
}