using api.Dtos.Todos;
using api.Models;
using api.Helpers;

namespace api.Interfaces;

public interface ITodoRepository
{
    Task<List<Todo>> GetAllAsync(TodoQueryObject query);
    Task<Todo> GetByIdAsync(int id);
    Task<Todo> CreateAsync(Todo todoModel);
    Task<Todo> UpdateAsync(int id, Todo todoModel);
    Task<Todo> DeleteAsync(int id);
}