using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KssGroupPlanning.Interfaces;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Repositories;

public class UsersRepository(ProjectDbContext context) : IUsersRepository
{
    private readonly ProjectDbContext _dbcontext = context;

    public async Task Add(User user)
    {
        var userEntity = new UserEntity()
        {
            Id = user.Id,
            UserName = user.UserName,
            PasswordHash = user.PasswordHash,
            Email = user.Email
        };
        await _dbcontext.User.AddAsync(userEntity);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<User> GetByEmail(string email) 
    {
        var userEntity = await _dbcontext.User.AsNoTracking().FirstOrDefaultAsync(u=>u.Email == email)??throw new Exception();
        return User.Create(userEntity.Id, userEntity.UserName, userEntity.PasswordHash, userEntity.Email);
    }
}
