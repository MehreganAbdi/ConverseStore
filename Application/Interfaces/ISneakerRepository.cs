﻿using Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISneakerRepository
    {
        Task<IEnumerable<Sneaker>> GetAllAsync();
        IEnumerable<Sneaker> GetAll();
        Task<Sneaker> GetByIdAsync(int Id);
        Sneaker GetById(int Id);
        Task<Sneaker> GetByIdAsyncAsNoTracking(int Id);
        Task<bool> AddAsync(Sneaker sneaker);
        bool Add(Sneaker sneaker);
        Task<bool> RemoveAsync(Sneaker sneaker);
        bool Remove(Sneaker sneaker);
        bool Update(Sneaker sneaker);
        Task<bool> SaveAsync();
        bool Save();
    }
}