using api.Dtos.Todos;

namespace api.Dtos.User;

public class UserDto 
{ 
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
    public string Email { get; set; } = string.Empty;
    public List<TodoDto>? Todos { get; set; }
}