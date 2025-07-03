using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

public class ClienteService
{
    private readonly AppDbContext _db;
    private readonly PasswordHasher<Cliente> _hasher = new();


    public ClienteService(AppDbContext context)
    {
        _db = context;
    }
 
    [HttpPost]
    public async Task<Cliente?> Login(string email, string senha)
    {

        var usuario = await _db.Usuarios.FirstOrDefaultAsync(x => x.Email == email);
        if (usuario == null) return null;

        var result = _hasher.VerifyHashedPassword(usuario, usuario.Senha, senha);
        if (result == PasswordVerificationResult.Success)
        {
            usuario.Senha = string.Empty;
            return usuario;
        }

        return null;

    }



    [HttpPost]
    public async Task<string> CadastrarCliente(Cliente cliente)
    {

        

        var usuarioExiste = await _db.Usuarios.FirstOrDefaultAsync(c => c.Email == cliente.Email) != null;
        if (usuarioExiste)
        {
            return "Email já cadastrado.";
        }

        cliente.Senha = _hasher.HashPassword(cliente, cliente.Senha);
        await _db.Usuarios.AddAsync(cliente);
        await _db.SaveChangesAsync();
        return "Usuario cadastrado com sucesso.";
    }


    //MÉTODOS UTILIZANDO JSON COMO FONTE DE DADOS:
    private readonly string _pathCliente = "BaseDeDados/Clientes.json";

    public List<Cliente> GetClientesJSON()
    {
        if (!File.Exists(_pathCliente))
        {
            File.WriteAllText(_pathCliente, "[]");
        }

        var clientesJson = File.ReadAllText(_pathCliente);
        return JsonSerializer.Deserialize<List<Cliente>>(clientesJson) ?? new List<Cliente>();
    }

    [HttpPost]
    public async Task<string> CadastrarClienteJSON(Cliente cliente)
    {
        var clientes = GetClientesJSON();

        if (clientes.Any(c => c.Email == cliente.Email))
        {
            return "Email já cadastrado.";
        }

        clientes.Add(cliente);

        var json = JsonSerializer.Serialize(clientes, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_pathCliente, json);

        return "Cliente cadastrado com sucesso.";
    }

    [HttpGet]

    public Cliente? AutenticarClienteJSON(string email, string senha)
    {
        var clientes = GetClientesJSON();
        return clientes.FirstOrDefault(c => c.Email == email && c.Senha == senha);
    }
}