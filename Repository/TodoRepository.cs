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

        return await todos.ToListAsync();
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