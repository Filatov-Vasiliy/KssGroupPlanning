using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Interfaces.EntityInterfaces;
using KssGroupPlanning.Infrastuction.Db;

namespace KssGroupPlanning.Repositories;

public class UsersRepository(ProjectDbContext context) : IUsersRepository
{
    private readonly ProjectDbContext _dbcontext = context;

    public async Task Add(UserEntity user)
    {
        await _dbcontext.User.AddAsync(user);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<UserEntity> GetByEmail(string email) 
    {
        return await _dbcontext.User.AsNoTracking().FirstOrDefaultAsync(u=>u.Email == email)??throw new Exception();
    }
}
