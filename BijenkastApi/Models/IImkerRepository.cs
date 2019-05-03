namespace BijenkastApi.Models
{
    public interface IImkerRepository
    {
        Imker GetBy(int id);

        Imker GetBy(string email);

        void Add(Imker imker);

        void Update(Imker imker);

        void SaveChanges();
    }
}