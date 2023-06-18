using CommonLibrary;

using Microsoft.EntityFrameworkCore;

namespace PurchaseAPI.Repositories
{
    public class PurchaseRepo<T> : IGenericRepo<T> where T : class
    {
        IDbContext db;
        DbSet<T> table;

        public PurchaseRepo(IDbContext db)
        {
            this.db = db;
            this.table = db.Set<T>();
        }

        public async Task<T> AddAsync(T item)
        {
            var res = await table.AddAsync(item);
            return res.Entity;
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
