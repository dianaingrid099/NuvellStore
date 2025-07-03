using System.Text.Json.Serialization;

public class ItemPedido {
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PedidoId { get; set; }              
    [JsonIgnore]
    public Pedido Pedido { get; set; }

    public Guid ProdutoId { get; set; }              
    public Produto Produto { get; set; }

    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}