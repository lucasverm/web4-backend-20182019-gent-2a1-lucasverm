using System.Collections.Generic;

namespace BijenkastApi.Models
{
    public interface IBijenkastRepository
    {
        Bijenkast GetBy(int id);

        bool TryGetBijenkast(int id, out Bijenkast bijenkast);

        IEnumerable<Bijenkast> GetAll(int imkerId);

        void Add(Bijenkast bijenkast);

        void Delete(Bijenkast bijenkast);

        void Update(Bijenkast bijenkast);

        void SaveChanges();
    }
}