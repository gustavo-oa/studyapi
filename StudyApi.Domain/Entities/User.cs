namespace StudyApi.Domain.Entities;

public class User
{
    public Guid Id { get; set; }               // ID único do usuário
    public string Nome { get; set; } = null!;  // Nome do usuário
    public string Email { get; set; } = null!; // Email (deve ser único)
    public string SenhaHash { get; set; } = null!; // Senha criptografada
    public bool IsActive { get; set; } = true; // Se o usuário está ativo
    public DateTime CreateDate { get; set; } = DateTime.UtcNow; // Data de criação
    public DateTime UpdateDate { get; set; }   // necessário para o CreateUserHandler
}