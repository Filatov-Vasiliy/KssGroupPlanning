namespace KssGroupPlanning.Interfaces.Infrastruction
{
    public interface IPasswordHasher
    {
        string Generate(string password);

        bool Verify(string password, string hashedPassword);
    }
}
