using Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISocksRepository
    {
        Task<IEnumerable<Socks>> GetAllAsync();
        IEnumerable<Socks> GetAll();
        Task<Socks> GetByIdAsync(int Id);
        Socks GetById(int Id);
        Task<Socks> GetByIdAsyncAsNoTracking(int Id);
        Task<bool> AddAsync(Socks socks);
        bool Add(Socks socks);
        Task<bool> RemoveAsync(Socks socks);
        bool Remove(Socks socks);
        bool Update(Socks socks);
        Task<bool> SaveAsync();
        bool Save();
    }
}
