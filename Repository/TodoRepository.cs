using api.Data;
using api.Dtos.Todos;
using api.Interfaces;
using api.Models;
using api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class TodoRepository : ITodoRepository
{
    private readonly ApplicationDBContext _context;
    public TodoRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<Todo> CreateAsync(Todo todoModel)
    {
        await _context.Todos.AddAsync(todoModel);
        await _context.SaveChangesAsync();
        return todoModel;
    }

    public async Task<Todo> DeleteAsync(int id)
    {
        var todoModel = await _context.Todos.FirstOrDefaultAsync(p => p.Id == id);

        if (todoModel == null)
            return null;

        _context.Todos.Remove(todoModel) ;
        await _context.SaveChangesAsync();
        return todoModel;
    }

    public async Task<List<Todo>> GetAllAsync(TodoQueryObject query)
    {
        var todos = _context.Todos.AsQueryable();

        if (!string.IsNullOrEmpty(query.Name))
        {
            todos = todos.Where(p => p.Name.Contains(query.Name));
        }

        if (!string.IsNullOrEmpty(query.Description))
        {
            todos = todos.Where(p => p.Description.Contains(query.Description));
        }

        if (!string.IsNullOrEmpty(query.SortBy))
        {
           switch (query.SortBy.ToLower())
           {
                case "id":
                    todos = query.IsDecsending ? todos.OrderByDescending(p => p.Id) : todos.OrderBy(p => p.Id);
                    break;
                case "name":
                    todos = query.IsDecsending ? todos.OrderByDescending(p => p.Name) : todos.OrderBy(p => p.Name);
                    break;
                case "description":
                    todos = query.IsDecsending ? todos.OrderByDescending(p => p.Description) : todos.OrderBy(p => p.Description);
                    break;
                case "starttime":
                    todos = query.IsDecsending ? todos.OrderByDescending(p => p.StartTime) : todos.OrderBy(p => p.StartTime);
                    break;
                case "endtime":
                    todos = query.IsDecsending ? todos.OrderByDescending(p => p.EndTime) : todos.OrderBy(p => p.EndTime);
                    break;
                case "userId":
                    todos = query.IsDecsending ? todos.OrderByDescending(p => p.UserId) : todos.OrderBy(p => p.UserId);
                    break;
                default:
                    break;
           }
        }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return await todos.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public async Task<Todo> GetByIdAsync(int id)
    {
        return await _context.Todos.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Todo> UpdateAsync(int id, Todo todoModel)
    {
        var existingTodo = await _context.Todos.FirstOrDefaultAsync(p => p.Id == id);

        if (existingTodo == null)
            return null;

        existingTodo.Name = todoModel.Name;
        existingTodo.Description = todoModel.Description;
        existingTodo.StartTime = todoModel.StartTime;
        existingTodo.EndTime = todoModel.EndTime;

        await _context.SaveChangesAsync();

        return existingTodo;
    }
}