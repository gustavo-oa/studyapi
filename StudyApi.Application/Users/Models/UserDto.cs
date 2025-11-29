namespace StudyApi.Application.Users.Models;

public record UserDto(Guid Id, string Nome, string Email, DateTime CreateDate, DateTime UpdateDate);