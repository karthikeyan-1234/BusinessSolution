﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public interface IGenericRepo<T> where T : class
    {
        Task<T> AddAsync(T item);
        Task SaveChangesAsync();
        Task<ICollection<T>> GetAllAsync();
    }
}
