using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class Todo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    [Column(TypeName = "datetime")]
    public DateTime StartTime { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime EndTime { get; set; }
    public int? UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual User IdUserNavigation { get; set; } = null!;
}