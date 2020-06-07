using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student.DataLayer.Data
{
    [Table("Students")]
    public partial class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(1)]
        [Column(TypeName = "nchar(1)")]
        public string Gender { get; set; }

        [Required]
        [MaxLength(40)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [MaxLength(60)]
        public string MiddleName { get; set; }

        [MinLength(6), MaxLength(16)]
        public string Nickname { get; set; }

        public virtual ICollection<StudentGroup> StudentGroups { get; set; }

        public Student()
        {
            StudentGroups = new HashSet<StudentGroup>();
        }
    }

    /// <summary>
    /// Sort settings for students
    /// </summary>
    public enum SortStudentState
    {
        SurnameAsc,   
        SurnameDesc,   
        NicknameAsc, 
        NicknameDesc
    }
}
