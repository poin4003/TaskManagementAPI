using api.Dtos.Todos;
using api.Models;

namespace api.Mappers;

public static class TodoMapper
{
    public static TodoDto ToTodoDto(this Todo todomodel)
    {
        return new TodoDto
        {
            Id = todomodel.Id,
            Name = todomodel.Name,
            Description = todomodel.Description,
            StartTime = todomodel.StartTime,
            EndTime = todomodel.EndTime,
            UserId = todomodel.UserId,
        };
    }

    public static Todo ToTodoFromCreate(this CreateTodoRequestDto todoRequestDto, int userId)
    {
        return new Todo
        {
            Name = todoRequestDto.Name,
            Description = todoRequestDto.Description,
            StartTime = todoRequestDto.StartTime,
            EndTime = todoRequestDto.EndTime,
            UserId = userId,
        };
    }

    public static Todo ToTodoFromUpdate(this UpdateTodoRequestDto todoRequestDto)
    {
        return new Todo
        {
            Name = todoRequestDto.Name,
            Description = todoRequestDto.Description,
            StartTime = todoRequestDto.StartTime,
            EndTime = todoRequestDto.EndTime,
        };
    }
}