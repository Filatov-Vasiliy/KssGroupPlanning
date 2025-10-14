using KssGroupPlanning.Entities;
using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface INewUsersRepository
    {
        Task Add(UserEntity user);

        Task<UserEntity> GetByEmail(string email);
    }
}
