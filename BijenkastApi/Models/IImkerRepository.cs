namespace BijenkastApi.Models
{
    public interface IImkerRepository
    {
        Imker GetBy(string email);

        void Add(Imker customer);

        void SaveChanges();
    }
}