using KssGroupPlanning.Interfaces.EntityInterfaces;
using KssGroupPlanning.Interfaces.Infrastruction;
using KssGroupPlanning.Entities;
using KssGroupPlanning.Repositories;

namespace KssGroupPlanning.Services.EntityServices;

public class UserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtProvider _jwtProvider;

    public UserService(IUsersRepository usersRepository,IPasswordHasher passwordHasher, IJwtProvider jwtProvider) 
    {
        _passwordHasher = passwordHasher;
        _usersRepository = usersRepository;
        _jwtProvider = jwtProvider;

    }
    public async Task Register(string userName, string email, string password)
    {
        var hashedPassword = _passwordHasher.Generate(password);

        var user = new UserEntity() { Id = Guid.NewGuid(), UserName = userName, PasswordHash = hashedPassword, Email = email };

        await _usersRepository.Add(user);
    }

    public async Task<string> Login(string Email, string password)
    {
        var user = await _usersRepository.GetByEmail(Email);
        //обработка юзера
        var result = _passwordHasher.Verify(password, user.PasswordHash);
        if (result == false)
        {
            throw new Exception("Failed to login");
        }

        var token = _jwtProvider.GenerateToken(user);

        return token;
    }
}
