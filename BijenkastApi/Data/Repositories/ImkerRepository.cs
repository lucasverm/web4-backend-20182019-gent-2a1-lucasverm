using Microsoft.EntityFrameworkCore;
using BijenkastApi.Models;
using System.Linq;

namespace BijenkastApi.Data.Repositories
{
    public class ImkerRepository : IImkerRepository
    {
        private readonly BijenkastContext _context;
        private readonly DbSet<Imker> _imkers;

        public ImkerRepository(BijenkastContext dbContext)
        {
            _context = dbContext;
            _imkers = dbContext.Imkers;
        }

        public Imker GetBy(string email)
        {
            return _imkers.Where(c => c.email == email).SingleOrDefault();
        }

        public Imker GetBy(int id)
        {
            return _imkers.Where(c => c.ImkerId == id).SingleOrDefault();
        }

        public void Add(Imker imker)
        {
            _imkers.Add(imker);
        }

        public void Update(Imker imker)
        {
            _imkers.Update(imker);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}