using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student.DataLayer.Data
{
    [Table("Persons")]
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Login { get; set; }

        [Required]
        [MaxLength(64)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
