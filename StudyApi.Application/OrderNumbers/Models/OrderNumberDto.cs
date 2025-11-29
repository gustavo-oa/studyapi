namespace StudyApi.Application.OrderNumbers.Models;

public record OrderNumberDto(
    Guid Id,
    string Nome,
    DateTime CreateDate,
    DateTime UpdateDate,
    bool IsEnabled
);

