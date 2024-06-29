using api.Dtos.User;
using api.Models;

namespace api.Mappers;

public static class UserMappers
{
    public static UserDto ToUserDto(this User userModel)
    {
        return new UserDto
        {
            Id = userModel.Id,
            Name = userModel.Name,
            Birthday = userModel.Birthday,
            Email = userModel.Email,
            AccessTime = userModel.AccessTime,
            Todos = userModel.Todos.Select(p => p.ToTodoDto()).ToList(),
        };
    }

    public static User ToUserFromCreateDto(this CreateUserRequestDto userRequestDto)
    {
        return new User 
        {
            Name = userRequestDto.Name,
            Birthday = userRequestDto.Birthday,
            Email = userRequestDto.Email,
            AccessTime = userRequestDto.AccessTime,
        };
    }

    public static User ToUserFromUpdateDto(this UpdateUserRequestDto userRequestDto)
    {
        return new User
        {
            Name = userRequestDto.Name,
            Birthday = userRequestDto.Birthday,
            Email = userRequestDto.Email,
            AccessTime = userRequestDto.AccessTime,
        };
    }
}