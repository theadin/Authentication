using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Models;

[Table("users_tbl")]
public class UserModel
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("email")]
    [MaxLength(250)]
    [Required]
    public string? Email { get; set; }

    [Column("password")]
    [MaxLength(50)]
    [Required]
    public string? Password { get; set; }

    [Column("first_name")]
    [DisplayName("First Name")]
    [MaxLength(50)]
    [Required]
    public string? FirstName { get; set; }

    [Column("last_name")]
    [DisplayName("Last Name")]
    [Required]
    [MaxLength(50)]
    public string? LastName { get; set; }

    [Column("role")]
    [MaxLength(50)]
    public string? Role { get; set; }

    [Column("cdate")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime Cdate { get; set; }

    [Column("client_id")]
    public Guid ClientId { get; set; }

    [Column("deleted")]
    public bool Deleted { get; set; }

    [Column("verified")]
    [DisplayName("Email Verified")]
    public bool Verified { get; set; }

    [Column("country")]
    [MaxLength(50)]
    public string? Country { get; set; }
}
