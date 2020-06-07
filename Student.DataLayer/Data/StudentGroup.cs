using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student.DataLayer.Data
{
    [Table("StudentGroupTable")]
    public partial class StudentGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdGroup { get; set; }
        public int IdStudent { get; set; }

        [ForeignKey("IdGroup")]
        public virtual Group Group { get; set; }
        [ForeignKey("IdStudent")]
        public virtual Student Student { get; set; }
    }
}
