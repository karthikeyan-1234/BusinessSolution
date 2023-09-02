using CommonLibrary;
using CommonLibrary.Models;

using Microsoft.EntityFrameworkCore;

namespace CommonLibrary.Repositories
{
    public class GenericRepo<T,D> : IGenericRepo<T,D> where T : BaseModel where D : DbContext
    {
        D db;
        DbSet<T> table;

        public GenericRepo(D db)
        {
            this.db = db;
            this.table = db.Set<T>();
        }

        public async Task<T> AddAsync(T item)
        {
           var obj = await table.AddAsync(item);
           return obj.Entity;
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await table.FindAsync(id);
        }

        public ICollection<T> Find(Func<T, bool> predicate)
        {
            return  table.Where(predicate).ToList();
        }
    }
}
