public class Pedido
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UsuarioId { get; set; }
    public Cliente Usuario { get; set; }

    public decimal PrecoTotal { get; set; }
    public string? EnderecoEntrega { get; set; }
    
    public List<ItemPedido> Itens { get; set; } = new();

}