using Application.Interfaces;
using Context.Data;
using Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class BackPackRepository : IBackPackRepository
    {
        private readonly StoreDbContext _context;
        public BackPackRepository(StoreDbContext _context)
        {
            this._context = _context;
        }
        public bool Add(BackPack backPack)
        {
            _context.BackPacks.Add(backPack);
            return Save();
        }

        public async Task<bool> AddAsync(BackPack backPack)
        {
            await _context.BackPacks.AddAsync(backPack);
            return await SaveAsync();
        }

        public IEnumerable<BackPack> GetAll()
        {
            return _context.BackPacks.ToList();
        }

        public async Task<IEnumerable<BackPack>> GetAllAsync()
        {
            return await _context.BackPacks.ToListAsync();
        }

        public BackPack GetById(int Id)
        {
            return _context.BackPacks.FirstOrDefault(b => b.Id == Id);
        }

        public async Task<BackPack> GetByIdAsync(int Id)
        {
            return await _context.BackPacks.FirstOrDefaultAsync(b => b.Id == Id);
        }

        public async Task<BackPack> GetByIdAsyncAsNoTracking(int Id)
        {
            return await _context.BackPacks.AsNoTracking().FirstOrDefaultAsync(b => b.Id == Id);
        }

        public bool Remove(BackPack backPack)
        {
            _context.BackPacks.Remove(backPack);
            return Save();
        }

        public async Task<bool> RemoveAsync(BackPack backPack)
        {
            _context.Remove(backPack);
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

        public bool Update(BackPack backPack)
        {
            _context.BackPacks.Update(backPack);
            return Save();
        }

        public async Task<bool> UpdateAsync(BackPack backPack)
        {
            _context.BackPacks.Update(backPack);
            return await SaveAsync(); 
        }
    }
}
