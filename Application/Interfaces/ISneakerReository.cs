using Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISneakerReository
    {
        Task<IEnumerable<Sneaker>> GetAllAsync();
        IEnumerable<Sneaker> GetAll();
        Task<Sneaker> GetByIdAsync(string Id);
        Sneaker GetById(string Id);
        Task<Sneaker> GetByIdAsyncAsNoTracking(string Id);
        Task<bool> AddAsync(Sneaker sneaker);
        bool Add(Sneaker sneaker);
        Task<bool> RemoveAsync(Sneaker sneaker);
        bool Remove(Sneaker sneaker);
        bool Update(Sneaker sneaker);
        Task<bool> SaveAsync();
        bool Save();
    }
}