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
    public class SneakerRepository : ISneakerRepository
    {
        private readonly StoreDbContext _context;
        public SneakerRepository(StoreDbContext context)
        {
            _context = context;
        }

        public bool Add(Sneaker sneaker)
        {
            _context.Sneakers.Add(sneaker);
            return Save();
        }

        public async Task<bool> AddAsync(Sneaker sneaker)
        {
            await _context.Sneakers.AddAsync(sneaker);
            return await SaveAsync();
        }

        public IEnumerable<Sneaker> GetAll()
        {
            return _context.Sneakers.ToList();
        }

        public async Task<IEnumerable<Sneaker>> GetAllAsync()
        {
            return await _context.Sneakers.ToListAsync();
        }

        public Sneaker GetById(int Id)
        {
            return _context.Sneakers.FirstOrDefault(s => s.Id == Id);
        }

        public async Task<Sneaker> GetByIdAsync(int Id)
        {
            return await _context.Sneakers.FirstOrDefaultAsync(s => s.Id == Id);
        }

        public async Task<Sneaker> GetByIdAsyncAsNoTracking(int Id)
        {
            return await _context.Sneakers.AsNoTracking().FirstOrDefaultAsync(s => s.Id == Id);
        }


        public bool Remove(Sneaker sneaker)
        {
            _context.Sneakers.Remove(sneaker);
            return Save();
        }

        public async Task<bool> RemoveAsync(Sneaker sneaker)
        {
            _context.Sneakers.Remove(sneaker);
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

        public bool Update(Sneaker sneaker)
        {
            _context.Update(sneaker);
            return Save();
        }
    }
}
