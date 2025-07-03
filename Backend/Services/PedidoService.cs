using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class PedidoService
{
    private readonly AppDbContext _db;

    public PedidoService(AppDbContext context)
    {
        _db = context;
    }

    [HttpGet]
    public async Task<List<Pedido>> GetPedidosByUserId(Guid userId)
    {
               return await _db.Pedidos
            .Include(p => p.Itens)
            .ThenInclude(ip => ip.Produto)
            .Where(x => x.UsuarioId == userId)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<string> FinalizarCompra(List<ItemPedido> itensPedido, Guid userId)
    {
        var user = await _db.Usuarios.FirstOrDefaultAsync(p => p.Id == userId);
        if (user == null) return "Usuário não encontrado";

        var precoTotalPedido = itensPedido.Sum(p => p.PrecoUnitario * p.Quantidade);
        var enderecoPedido = user.Endereco;

        var pedido = new Pedido
        {
            UsuarioId = userId,
            Usuario = user,
            PrecoTotal = precoTotalPedido,
            EnderecoEntrega = enderecoPedido,
            Itens = new List<ItemPedido>()
        };

        foreach (var item in itensPedido)
        {
            item.Id = Guid.NewGuid(); 
            item.PedidoId = pedido.Id;

            pedido.Itens.Add(item);
        }

        // _db.ItensPedido.AddRange(pedido.Itens);
        _db.Pedidos.Add(pedido);
        await _db.SaveChangesAsync();           

        return "Compra registrada!";
    }

}