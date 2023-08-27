using CommonLibrary.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public interface IGenericRepo<T,D> where T : BaseModel where D : DbContext
    {
        Task<T> AddAsync(T item);
        Task SaveChangesAsync();
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetById(object id);
    }
}
