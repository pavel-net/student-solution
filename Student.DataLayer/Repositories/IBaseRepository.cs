using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.DataLayer.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
        bool Exists(int id);
        IQueryable<T> GetQuery();
        void Save();
    }
}
