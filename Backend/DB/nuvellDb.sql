
Create table Usuarios (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Nome NVARCHAR(100),
    Email NVARCHAR(100),
    Senha NVARCHAR(255),
    Endereco NVARCHAR(300)
);

CREATE TABLE Produtos (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Nome NVARCHAR(100),
    Descricao NVARCHAR(300),
    Preco DECIMAL(10,2),
    Estoque INT
);

CREATE TABLE Pedidos(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    UsuarioId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Usuarios(Id),
    DataPedido DATETIME DEFAULT GETDATE(),
    EnderecoEntrega NVARCHAR(300),
    PrecoTotal DECIMAL(10,2)
);

CREATE TABLE ItensPedido (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    PedidoId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Pedidos(Id),
    ProdutoId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Produtos(Id),
    Quantidade INT,
    PrecoUnitario DECIMAL(10,2)
);

select * from pedidos;
select * from ItensPedido;
select * from usuarios;
select * from produtos;
