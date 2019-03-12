using System.Collections.Generic;

namespace BijenkastApi.Models
{
    internal interface IImkerRepository
    {
        Imker GetBy(int id);

        bool TryGetImker(int id, out Imker imker);

        IEnumerable<Imker> GetAll();

        void Add(Imker imker);

        void Delete(Imker imker);

        void Update(Imker imker);

        void SaveChanges();
    }
}