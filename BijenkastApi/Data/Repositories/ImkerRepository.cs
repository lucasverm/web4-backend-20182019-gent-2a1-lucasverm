using Microsoft.EntityFrameworkCore;
using BijenkastApi.Models;
using System.Linq;

namespace BijenkastApi.Data.Repositories
{
    public class ImkerRepository : IImkerRepository
    {
        private readonly BijenkastContext _context;
        private readonly DbSet<Imker> _customers;

        public ImkerRepository(BijenkastContext dbContext)
        {
            _context = dbContext;
            _customers = dbContext.Imkers;
        }

        public Imker GetBy(string email)
        {
            return _customers.SingleOrDefault(c => c.Email == email);
        }

        public void Add(Imker imker)
        {
            _customers.Add(imker);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}