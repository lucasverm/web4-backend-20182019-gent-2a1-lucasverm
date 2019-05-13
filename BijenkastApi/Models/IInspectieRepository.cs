using System.Collections.Generic;

namespace BijenkastApi.Models
{
    public interface IInspectieRepository
    {
        Inspectie GetBy(int id);

        IEnumerable<Inspectie> GetAll();

        void Add(Inspectie inspectie);

        void Delete(Inspectie inspectie);

        void Update(Inspectie inspectie);

        void SaveChanges();

    }
}