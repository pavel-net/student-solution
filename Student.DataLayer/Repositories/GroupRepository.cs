using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student.DataLayer.Data;

namespace Student.DataLayer.Repositories
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        void AddStudentToGroup(int studentId, int groupId);
        void DeleteStudentFromGroup(int studentId, int groupId);
    }

    public class GroupRepository : IGroupRepository
    {
        private StudentDbContext db;

        public GroupRepository(StudentDbContext db)
        {
            this.db = db;
        }

        public Task<List<Group>> GetAllAsync()
        {
            return db.Group.ToListAsync();
        }

        public IQueryable<Group> GetQuery()
        {
            return db.Group.AsQueryable();
        }

        public Group Get(int id)
        {
            return db.Group.FirstOrDefault(g => g.Id == id);
        }

        public void Create(Group item)
        {
            db.Group.Add(item);
        }

        public void Update(Group item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Group item)
        {
            db.Group.Remove(item);
        }

        public bool Exists(int id)
        {
            return db.Group.Any(g => g.Id == id);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void AddStudentToGroup(int studentId, int groupId)
        {
            db.StudentGroup.Add(new StudentGroup() {IdGroup = groupId, IdStudent = studentId});
        }

        public void DeleteStudentFromGroup(int studentId, int groupId)
        {
            var studGroup = db.StudentGroup.FirstOrDefault(s => s.IdGroup == groupId && s.IdStudent == studentId);
            if (studGroup != null)
                db.StudentGroup.Remove(studGroup);
        }
    }
}
