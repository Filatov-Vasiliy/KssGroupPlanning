using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces.Repository
{
    public interface IUsersRepository
    {
        Task Add(User user);

        Task<User> GetByEmail(string email);
    }
}
