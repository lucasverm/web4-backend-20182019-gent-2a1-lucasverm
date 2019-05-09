using System.Collections.Generic;

namespace BijenkastApi.Models
{
    public interface IBijenkastRepository
    {
        Bijenkast GetBy(int id);

        IEnumerable<Bijenkast> GetAll(int imkerId);

        void Add(Bijenkast bijenkast);

        void Delete(Bijenkast bijenkast);

        void Update(Bijenkast bijenkast);

        void SaveChanges();

        void DeleteInspecties(Bijenkast bijenkast);
    }
}