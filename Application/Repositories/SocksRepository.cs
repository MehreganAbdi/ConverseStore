using Application.Interfaces;
using Context.Data;
using Context.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class SocksRepository : ISocksRepository
    {
        private readonly StoreDbContext _context;
        public SocksRepository(StoreDbContext context)
        {
            _context = context;
        }
        public bool Add(Socks socks)
        {
            _context.Socks.Add(socks);
            return Save();
        }

        public async Task<bool> AddAsync(Socks socks)
        {
            await _context.Socks.AddAsync(socks);
            return await SaveAsync();
        }

        public IEnumerable<Socks> GetAll()
        {
            return _context.Socks.ToList();
        }

        public async Task<IEnumerable<Socks>> GetAllAsync()
        {
            return await _context.Socks.ToListAsync();
        }

        public Socks GetById(string Id)
        {
            return _context.Socks.FirstOrDefault(s => s.Id == Id);
        }

        public async Task<Socks> GetByIdAsync(string Id)
        {
            return await _context.Socks.FirstOrDefaultAsync(s=>s.Id==Id);
        }

        public async Task<Socks> GetByIdAsyncAsNoTracking(string Id)
        {
            return await _context.Socks.AsNoTracking().FirstOrDefaultAsync(s => s.Id == Id);
        }


        public bool Remove(Socks socks)
        {
            _context.Remove(socks);
            return Save();
        }

        public async Task<bool> RemoveAsync(Socks socks)
        {
            _context.Remove(socks);
            return await SaveAsync();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public bool Update(Socks socks)
        {
            _context.Socks.Update(socks);
            return Save();
        }
    }
}
