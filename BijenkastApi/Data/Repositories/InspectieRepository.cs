using BijenkastApi.Data;
using BijenkastApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BijenkastApi.Repositories
{
    public class InspectieRepository : IInspectieRepository
    {
        private readonly BijenkastContext _context;
        private readonly DbSet<Inspectie> _inspecties;

        public InspectieRepository(BijenkastContext dbContext)
        {
            _context = dbContext;
            _inspecties = dbContext.Inspecties;
        }

        public IEnumerable<Inspectie> GetAll()
        {
            return _inspecties.ToList();
        }

        public Inspectie GetBy(int id)
        {
            return _inspecties.SingleOrDefault(r => r.id == id);
        }

        public void Add(Inspectie inspectie)
        {
            _inspecties.Add(inspectie);
        }

        public void Update(Inspectie inspectie)
        {
            _inspecties.Update(inspectie);
        }

        public void Delete(Inspectie inspectie)
        {
            _inspecties.Remove(inspectie);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}