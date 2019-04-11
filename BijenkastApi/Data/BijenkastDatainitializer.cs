using BijenkastApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
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
                Imker imker = new Imker { Email = "student@hogent.be", FirstName = "Student", LastName = "Hogent" };
                _dbContext.Imkers.Add(imker);
                await CreateUser(imker.Email, "TomDeBakker123!");
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