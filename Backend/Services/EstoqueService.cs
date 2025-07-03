using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class EstoqueService
{

    private readonly AppDbContext _db;

    public EstoqueService(AppDbContext context)
    {
        _db = context;
    }

    [HttpGet]
    public async Task<Produto?> GetProdutoById(Guid id){
         var produto = await _db.Produtos.FirstOrDefaultAsync(x => x.Id == id);
        if (produto == null) return null;
        return produto;
    }
    [HttpGet]
    public async Task<List<Produto>> GetProdutos()
    {

        var produtos = await _db.Produtos.ToListAsync();
        if (produtos != null && produtos.Count > 0)
        {
            return produtos;
        }
        else
        {
            return new List<Produto>();
        }

    }
    [HttpPost]

    public async Task AdicionarProduto(Produto produto)
    {

        var produtoExistente = await _db.Produtos.FirstOrDefaultAsync(x => x.Nome == produto.Nome);

        if (produtoExistente != null)
        {
            produtoExistente.Estoque += produto.Estoque;
            _db.Produtos.Update(produtoExistente);
        }
        else
        {
            await _db.Produtos.AddAsync(produto);
        }

        await _db.SaveChangesAsync();

        return;
    }

    [HttpDelete]

    public async Task RemoverProdutoAsync(string id)
    {
        var produtoRemovido = await _db.Produtos.FirstOrDefaultAsync(produto => produto.Id.ToString() == id);

        if (produtoRemovido != null)
        {
            _db.Produtos.Remove(produtoRemovido);
            await _db.SaveChangesAsync();
            return;
        }
    }

    [HttpPut]
    public async Task AtualizarProdutoAsync(string id, Produto produtoAtualizado)
    {
        var produtoExistente = _db.Produtos.FirstOrDefault(p => p.Id.ToString() == id);
        if (produtoExistente != null)
        {
            produtoExistente.Nome = produtoAtualizado.Nome ?? produtoExistente.Nome;
            produtoExistente.Descricao = produtoAtualizado.Descricao ?? produtoExistente.Descricao;
            produtoExistente.Preco = produtoAtualizado.Preco != 0 ? produtoAtualizado.Preco : produtoExistente.Preco;
            produtoExistente.Estoque = produtoAtualizado.Estoque != 0 ? produtoAtualizado.Estoque : produtoExistente.Estoque;

            _db.Update(produtoExistente);
            await _db.SaveChangesAsync();
            return;
        }
    }

    //DAQUI PRA BAIXO É USANDO JSON AO INVÉS DO DB
    public readonly string _pathEstoque = "BaseDeDados/Estoque.json";
    
    public List<Produto> getProdutosJSON(){
        var produtosJson = File.ReadAllText(_pathEstoque);
        var produtos = new List<Produto>();
        
        if (produtosJson != null && produtosJson != ""){

            produtos = JsonSerializer.Deserialize<List<Produto>>(produtosJson);

            if(produtos != null && produtos.Count > 0){
                return produtos;
            }else{
                return new List<Produto>();
            }
        }
        else{
            return new List<Produto>();
        }
        
    }
    [HttpPost]

    async public Task AdicionarProdutoJSON(Produto produto)
    {
        var estoque = getProdutosJSON();

        var produtoExistente = estoque.FirstOrDefault(p => p.Nome == produto.Nome);
        var estoqueAtualizado = "";

        if (produtoExistente != null)
        {
        
            foreach (var item in estoque)
            {
                if (item.Nome == produtoExistente.Nome)
                {
                    item.Estoque += produto.Estoque;
                }
            }
            estoqueAtualizado = JsonSerializer.Serialize(estoque, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_pathEstoque, estoqueAtualizado);
            return;
        }

        estoque.Add(produto);

        estoqueAtualizado = JsonSerializer.Serialize(estoque, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_pathEstoque, estoqueAtualizado);
        return;
    }

    [HttpDelete]

    public async Task RemoverProdutoAsyncJSON(string id)
    {
        var estoque = getProdutosJSON();

        var produtoExistente = estoque.FirstOrDefault(p => p.Id.ToString() == id);
        if (produtoExistente != null)
        {
            estoque.Remove(produtoExistente);

            var estoqueAtualizado = JsonSerializer.Serialize(estoque, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_pathEstoque, estoqueAtualizado);
        }
    }

    [HttpPut]
    
    public async Task AtualizarProdutoAsyncJSON(string id, Produto produtoAtualizado)
    {
        var estoque = getProdutosJSON();
        var produtoExistente = estoque.FirstOrDefault(p => p.Id.ToString() == id);
        if (produtoExistente != null)
        {
            produtoExistente.Nome = produtoAtualizado.Nome ?? produtoExistente.Nome;
            produtoExistente.Descricao = produtoAtualizado.Descricao ?? produtoExistente.Descricao;
            produtoExistente.Preco = produtoAtualizado.Preco != 0 ? produtoAtualizado.Preco : produtoExistente.Preco;
            produtoExistente.Estoque = produtoAtualizado.Estoque != 0 ? produtoAtualizado.Estoque : produtoExistente.Estoque;

            var estoqueAtualizadoJson = JsonSerializer.Serialize(estoque, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_pathEstoque, estoqueAtualizadoJson);
        }
    }
}

