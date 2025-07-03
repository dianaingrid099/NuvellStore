public class Cliente
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string? Endereco { get; set; }

}