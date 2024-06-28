using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class User
{
    public User()
    {
        Todos = new HashSet<Todo>();
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    [Column(TypeName = "datetime")]
    public DateTime Birthday { get; set; }
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;
    public virtual ICollection<Todo> Todos { get; set; } = new List<Todo>();
}