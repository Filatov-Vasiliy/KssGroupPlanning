using KssGroupPlanning.Models;

namespace KssGroupPlanning.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(User user);

}
