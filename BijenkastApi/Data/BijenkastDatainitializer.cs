using BijenkastApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
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
                Bijenkast b = new Bijenkast("kast in de tuin", "dadant", "#269d2f", 1, 1, 10, "buckfast", 26, 5, 1998, true, false, true, 6, 3, 2014, new List<Inspectie>());
                Inspectie inspectie = new Inspectie(26, 5, 1998, "Dit is een test Notitie 1");
                _dbContext.Inspecties.Add(inspectie);
                b.inspecties.Add(inspectie);
                Imker imker = new Imker { email = "a@a.a", voornaam = "Student", achternaam = "Hogent", facebookimker = false };
                await CreateUser(imker.email, "Aaaaaa123!");
                imker.bijenkasten.Add(b);
                _dbContext.Bijenkasten.Add(b);

                b = new Bijenkast("kast bij de buren", "simplex", "#269d2f", 2, 1, 10, "buckfast", 6, 8, 2017, true, false, true, 3, 4, 2019, new List<Inspectie>());
                inspectie = new Inspectie(26, 5, 1998, "Dit is een test Notitie 2");
                _dbContext.Inspecties.Add(inspectie);
                b.inspecties.Add(inspectie);
                imker.bijenkasten.Add(b);
                _dbContext.Bijenkasten.Add(b);

                imker.bijenkasten.Add(b);
                _dbContext.Imkers.Add(imker);

                b = new Bijenkast("kast bij de buren", "simplex", "#269d2f", 2, 1, 10, "buckfast", 6, 8, 2017, true, false, true, 3, 4, 2019, new List<Inspectie>());
                inspectie = new Inspectie(26, 5, 1998, "Dit is een test Notitie 3");
                _dbContext.Inspecties.Add(inspectie);
                b.inspecties.Add(inspectie);
                imker = new Imker { email = "user2@example.com", voornaam = "Student", achternaam = "Hogent", facebookimker = false };
                await CreateUser(imker.email, "LucasVermeulen123!");
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