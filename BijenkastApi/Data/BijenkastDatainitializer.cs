using BijenkastApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BijenkastApi.Data
{
    public class BijenkastDatainitializer
    {
        private readonly BijenkastContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public BijenkastDatainitializer(BijenkastContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                Bijenkast b = new Bijenkast("kast in de tuin", "dadant", 1, 1, 10, "buckfast", 26, 5, 1998, true, false, true, 6, 3, 2014);
                Imker imker = new Imker { Email = "a@a.a", FirstName = "Student", LastName = "Hogent" };
                await CreateUser(imker.Email, "Aaaaaa123!");
                imker.bijenkasten.Add(b);
                _dbContext.Bijenkasten.Add(b);

                b = new Bijenkast("kast bij de buren", "simplex", 2, 1, 10, "buckfast", 6, 8, 2017, true, false, true, 3, 4, 2019);
                imker.bijenkasten.Add(b);
                _dbContext.Bijenkasten.Add(b);

                imker.bijenkasten.Add(b);
                _dbContext.Imkers.Add(imker);

                b = new Bijenkast("kast bij de buren", "simplex", 2, 1, 10, "buckfast", 6, 8, 2017, true, false, true, 3, 4, 2019); imker = new Imker { Email = "user2@example.com", FirstName = "Student", LastName = "Hogent" };
                await CreateUser(imker.Email, "LucasVermeulen123!");
                imker.bijenkasten.Add(b);
                _dbContext.Bijenkasten.Add(b);
                _dbContext.Imkers.Add(imker);

                _dbContext.SaveChanges();
            }
        }

        private async Task CreateUser(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user, password);
        }
    }
}