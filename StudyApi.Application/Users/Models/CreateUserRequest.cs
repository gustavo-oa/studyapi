namespace StudyApi.Application.Users.Models
{
    public record CreateUserRequest(string Name, string Email, string Password);
}