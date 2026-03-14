using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.Infrastructure.DataAcess.Repositories
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        private readonly MyRecipeBookDbContext _dbContext;

        public UserRepository(MyRecipeBookDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(User user) => await _dbContext.Users.AddAsync(user);

        public async Task<bool> ExistActiveUserWithEmail(string email) => await _dbContext.Users.AnyAsync(x => x.Email.Equals(email) && x.Active);

    }
}