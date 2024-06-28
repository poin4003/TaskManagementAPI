using System.ComponentModel.DataAnnotations;

namespace api.Dtos.User;

public class CreateUserRequestDto
{
    [Required]
    [MinLength(2, ErrorMessage = "Name must be over than 5 character!")]
    [MaxLength(100, ErrorMessage = "Name can not be over 100 character!")]
    public string Name { get; set; } = string.Empty;
    [Required]
    public DateTime Birthday { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address!")]
    public string Email { get; set; } = string.Empty;
}