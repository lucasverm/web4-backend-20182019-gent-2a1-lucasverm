using BijenkastApi.Data;
using BijenkastApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BijenkastApi.Repositories
{
    public class BijenkastRepository : IBijenkastRepository
    {
        private readonly BijenkastContext _context;
        private readonly DbSet<Bijenkast> _Bijenkasten;

        public BijenkastRepository(BijenkastContext dbContext)
        {
            _context = dbContext;
            _Bijenkasten = dbContext.Bijenkasten;
        }

        public IEnumerable<Bijenkast> GetAll(int imkerId)
        {
            return _Bijenkasten.Where(i => i.imkerId == imkerId).Include(t => t.inspecties).ToList();
        }

        public Bijenkast GetBy(int id)
        {
            return _Bijenkasten.Include(t => t.inspecties).SingleOrDefault(r => r.id == id);
        }

        public void Add(Bijenkast bijenkast)
        {
            _Bijenkasten.Add(bijenkast);
        }

        public void Update(Bijenkast bijenkast)
        {
            _context.Update(bijenkast);
        }

        public void Delete(Bijenkast bijenkast)
        {
            _Bijenkasten.Remove(bijenkast);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}