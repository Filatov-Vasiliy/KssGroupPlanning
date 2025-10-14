using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KssGroupPlanning.Interfaces;
using KssGroupPlanning.Interfaces.Repository;
using KssGroupPlanning.Models;
using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Repositories;

public class NewUsersRepository(ProjectDbContext context) : INewUsersRepository
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
