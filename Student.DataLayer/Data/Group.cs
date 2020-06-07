using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student.DataLayer.Data
{
    [Table("Groups")]
    public partial class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        public virtual ICollection<StudentGroup> StudentGroups { get; set; }

        public Group()
        {
            StudentGroups = new HashSet<StudentGroup>();
        }
    }
}
