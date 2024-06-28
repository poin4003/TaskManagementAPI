using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Todos;

public class UpdateTodoRequestDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Name must be over than 5 character!")]
    [MaxLength(100, ErrorMessage = "Name can not be over 100 character!")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MinLength(10, ErrorMessage = "Description must be over than 5 character!")]
    [MaxLength(300, ErrorMessage = "Description can not be over 300 character!")]
    public string Description { get; set; } = string.Empty;
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
}