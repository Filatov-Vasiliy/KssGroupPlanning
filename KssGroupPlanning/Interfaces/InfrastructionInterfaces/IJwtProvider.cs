using KssGroupPlanning.Entities;

namespace KssGroupPlanning.Interfaces.Infrastruction;

public interface IJwtProvider
{
    string GenerateToken(UserEntity user);

}
