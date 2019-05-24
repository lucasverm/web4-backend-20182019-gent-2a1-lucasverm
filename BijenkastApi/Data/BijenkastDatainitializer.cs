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
                Bijenkast b = new Bijenkast("Productievolk", "dadant", "#e9f900", 1, 1, 10, "buckfast", 5, 5, 2018, true, false, true, 6, 3, 2019, new List<Inspectie>());
                Inspectie inspectie = new Inspectie(24, 4, 2019, "Dit is een eerste inspectie. Het bijenvolk ziet er goed uit!");
                inspectie.eitjes = true;
                inspectie.larven = true;
                inspectie.poppen = true;
                inspectie.moeraanwezig = true;
                inspectie.ramenmetbijen = 6;
                _dbContext.Inspecties.Add(inspectie);
                b.inspecties.Add(inspectie);
                Imker imker = new Imker { email = "web4@hogent.be", voornaam = "Lucas", achternaam = "Vermeulen", facebookimker = false };
                await CreateUser(imker.email, "GelukkigGeenNetbeans123@!");
                imker.bijenkasten.Add(b);
                _dbContext.Bijenkasten.Add(b);

                b = new Bijenkast("Aflegger", "simplex", "#7affef", 1, 0, 6, "buckfast", 20, 5, 2019, true, false, true, 3, 4, 2019, new List<Inspectie>());
                inspectie = new Inspectie(24, 4, 2019, "Opletten! Het volk zou te weinig voer kunnen hebben!");
                inspectie.eitjes = true;
                inspectie.larven = true;
                inspectie.poppen = false;
                inspectie.moeraanwezig = true;
                inspectie.ramenmetbijen = 2;
                _dbContext.Inspecties.Add(inspectie);
                b.inspecties.Add(inspectie);
                imker.bijenkasten.Add(b);
                _dbContext.Bijenkasten.Add(b);

                imker.bijenkasten.Add(b);
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