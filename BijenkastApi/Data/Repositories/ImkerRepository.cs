using BijenkastApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BijenkastApi.Data.Repositories
{
    public class ImkerRepository : IImkerRepository
    {
        private readonly BijenkastContext _context;
        private readonly DbSet<Imker> _Imkers;

        public ImkerRepository(BijenkastContext dbContext)
        {
            _context = dbContext;
            _Imkers = dbContext.Imkers;
        }

        public void Add(Imker imker)
        {
            _Imkers.Add(imker);
        }

        public void Delete(Imker imker)
        {
            _context.Remove(imker);
        }

        public IEnumerable<Imker> GetAll()
        {
            return _Imkers.ToList();
        }

        public Imker GetBy(int id)
        {
            return _Imkers.SingleOrDefault(r => r.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool TryGetImker(int id, out Imker imker)
        {
            imker = _Imkers.FirstOrDefault(t => t.Id == id);
            return imker != null;
        }

        public void Update(Imker imker)
        {
            _context.Update(imker);
        }
    }
}