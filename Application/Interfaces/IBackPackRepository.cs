using Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBackPackRepository
    {
        IEnumerable<BackPack> GetAll();
        Task<IEnumerable<BackPack>> GetAllAsync();
        BackPack GetById(int Id);
        Task<BackPack> GetByIdAsync(int Id);
        Task<BackPack> GetByIdAsyncAsNoTracking(int Id);
        Task<bool> AddAsync(BackPack backPack);
        bool Add(BackPack backPack);
        Task<bool> RemoveAsync(BackPack backPack);
        bool Remove(BackPack backPack);
        Task<bool> UpdateAsync(BackPack backPack);
        bool Update(BackPack backPack);
        Task<bool> SaveAsync();
        bool Save();
    }
}
