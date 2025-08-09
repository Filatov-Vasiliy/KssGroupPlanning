using System.ComponentModel.DataAnnotations;

namespace KssGroupPlanning.Contracts.Users;

public record LoginUserRequest(
    [Required] string Password,
    [Required] string Email);
