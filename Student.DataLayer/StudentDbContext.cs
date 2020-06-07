using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Student.DataLayer.Data;

namespace Student.DataLayer
{
    public partial class StudentDbContext : DbContext
    {
        public StudentDbContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<Data.Student> Student { get; set; }
        public virtual DbSet<StudentGroup> StudentGroup { get; set; }
        public virtual DbSet<Person> Person { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentDb");
                //optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Data.Student>(entity =>
            {
                entity.HasIndex(e => e.Nickname)
                    .HasName("idx_Nickname_notnull")
                    .IsUnique()
                    .HasFilter("([Nickname] IS NOT NULL)");
            });

            modelBuilder.Entity<Group>().HasData(new Group[]
            {
                new Group() {Id = 1, Name = "Группа 1"},
                new Group() {Id = 2, Name = "Группа 2"},
                new Group() {Id = 3, Name = "Group 1"},
                new Group() {Id = 4, Name = "Group 2"}
            });

            modelBuilder.Entity<Data.Student>().HasData(new Data.Student[]
            {
                new Data.Student() {Id = 1, Gender = "М", Surname = "Иванов", Name = "Иван", MiddleName = "Иванович"},
                new Data.Student() {Id = 2, Gender = "М", Surname = "Петров", Name = "Василий", Nickname = "petr"},
                new Data.Student() {Id = 3, Gender = "Ж", Surname = "Иванова", Name = "Ира", MiddleName = "Ивановна"},
                new Data.Student() {Id = 4, Gender = "Ж", Surname = "Программистка", Name = "Василиса", Nickname = "progger"},
                new Data.Student() {Id = 5, Gender = "М", Surname = "Программер", Name = "Иван", Nickname = "pro"},
                new Data.Student() {Id = 6, Gender = "М", Surname = "Tiger", Name = "Ivan"},
                new Data.Student() {Id = 7, Gender = "М", Surname = "Terminator", Name = "Iron", Nickname = "termi"},
                new Data.Student() {Id = 8, Gender = "М", Surname = "Black", Name = "Tom"},
                new Data.Student() {Id = 9, Gender = "Ж", Surname = "Smith", Name = "Anna"},
                new Data.Student() {Id = 10, Gender = "Ж", Surname = "Middleton", Name = "Kate"}
            });

            int id = 0;

            modelBuilder.Entity<StudentGroup>().HasData(new StudentGroup[]
            {
                new StudentGroup() {Id = ++id, IdGroup = 1, IdStudent = 1},
                new StudentGroup() {Id = ++id, IdGroup = 1, IdStudent = 2},
                new StudentGroup() {Id = ++id, IdGroup = 1, IdStudent = 3},

                new StudentGroup() {Id = ++id, IdGroup = 2, IdStudent = 4},
                new StudentGroup() {Id = ++id, IdGroup = 2, IdStudent = 5},
                new StudentGroup() {Id = ++id, IdGroup = 2, IdStudent = 1},

                new StudentGroup() {Id = ++id, IdGroup = 3, IdStudent = 1},
                new StudentGroup() {Id = ++id, IdGroup = 3, IdStudent = 6},
                new StudentGroup() {Id = ++id, IdGroup = 3, IdStudent = 7},

                new StudentGroup() {Id = ++id, IdGroup = 4, IdStudent = 5},
                new StudentGroup() {Id = ++id, IdGroup = 4, IdStudent = 6},
                new StudentGroup() {Id = ++id, IdGroup = 4, IdStudent = 8},
                new StudentGroup() {Id = ++id, IdGroup = 4, IdStudent = 9},
            });

            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, Login = "admin", Password = "12345", Role = "admin" },
                new Person { Id = 2, Login = "user", Password = "11111", Role = "user" });
        }
    }
}
