namespace StudyApi.Domain.Entities;

public class OrderNumber
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public bool IsEnabled { get; set; } = true;
}

