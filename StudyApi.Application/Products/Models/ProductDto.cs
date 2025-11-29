namespace StudyApi.Application.Products.Models;

public record ProductDto(
    Guid Id,
    string Nome,
    decimal Price,
    DateTime CreateDate,
    DateTime UpdateDate,
    bool IsEnabled
);

