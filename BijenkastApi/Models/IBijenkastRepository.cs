using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BijenkastApi.Models
{
    public interface IBijenkastRepository
    {
        Bijenkast GetBy(int id);

        bool TryGetBijenkast(int id, out Bijenkast bijenkast);

        IEnumerable<Bijenkast> GetAll();

        void Add(Bijenkast bijenkast);

        void Delete(Bijenkast bijenkast);

        void Update(Bijenkast bijenkast);

        void SaveChanges();
    }
}