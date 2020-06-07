using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student.DataLayer.Data;

namespace Student.DataLayer.Repositories
{
    public interface IStudentRepository : IBaseRepository<Data.Student>
    {
    }

    public class StudentRepository : IStudentRepository
    {
        private StudentDbContext db;

        public StudentRepository(StudentDbContext db)
        {
            this.db = db;
        }

        public Task<List<Data.Student>> GetAllAsync()
        {
            return db.Student.ToListAsync();
        }

        public Data.Student Get(int id)
        {
            return db.Student.FirstOrDefault(s => s.Id == id);
        }

        public void Create(Data.Student item)
        {
            db.Student.Add(item);
        }

        public void Update(Data.Student item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Data.Student item)
        {
            db.Student.Remove(item);
        }

        public bool Exists(int id)
        {
            return db.Student.Any(g => g.Id == id);
        }

        public IQueryable<Data.Student> GetQuery()
        {
            return db.Student.AsQueryable();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
