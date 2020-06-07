using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student.DataLayer.Data;

namespace Student.DataLayer.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Person FirstOrDefault(Expression<Func<Person, bool>> predicate);
    }

    public class PersonRepository : IPersonRepository
    {
        private StudentDbContext db;

        public PersonRepository(StudentDbContext db)
        {
            this.db = db;
        }

        public Task<List<Person>> GetAllAsync()
        {
            return db.Person.ToListAsync();
        }

        public Person Get(int id)
        {
            return db.Person.FirstOrDefault(g => g.Id == id);
        }

        public void Create(Person item)
        {
            db.Person.Add(item);
        }

        public void Update(Person item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Person item)
        {
            db.Person.Remove(item);
        }

        public bool Exists(int id)
        {
            return db.Person.Any(g => g.Id == id);
        }

        public IQueryable<Person> GetQuery()
        {
            return db.Person.AsQueryable();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public Person FirstOrDefault(Expression<Func<Person, bool>> predicate)
        {
            return db.Person.FirstOrDefault(predicate);
        }
    }
}
