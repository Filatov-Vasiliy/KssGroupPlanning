using KssGroupPlanning.Entities;


namespace KssGroupPlanning.Interfaces.EntityInterfaces
{
    public interface IUsersRepository
    {
        Task Add(UserEntity user);

        Task<UserEntity> GetByEmail(string email);
    }
}
