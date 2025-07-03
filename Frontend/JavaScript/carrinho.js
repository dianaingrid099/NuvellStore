const botaoAbrir = document.getElementById("abrir-carrinho");
const botaoFechar = document.getElementById("fechar-carrinho");
const carrinho = document.getElementById("carrinho");
const botaoCheckout = document.getElementById("finalizar-compra");

function irParaCheckout() {
  console.log("checkout");
}
function attQuantidade(produto) {
  document.getElementById(`quantidade-${produto.Id}`).innerText =
    produto.Quantidade;
}

function setLocalStorage(key, value) {
  localStorage.setItem(key, JSON.stringify(value));
}

function getLocalStorage(key) {
  return JSON.parse(localStorage.getItem(key));
}

botaoAbrir.addEventListener("click", (e) => {
  e.preventDefault();
  carrinho.style.right = "0";
});

botaoFechar.addEventListener("click", () => {
  carrinho.style.right = "-360px";
});

const produtosCarrinho = document.getElementById("produtos-carrinho");
const precoTotal = document.getElementById("preco-total");

function incrementarQuantProduto(produto) {
  let carrinho = getLocalStorage("carrinho") ?? [];

  const index = carrinho.findIndex((p) => p.Id === produto.Id);
  if (index !== -1) {
    carrinho[index].Quantidade++;
    setLocalStorage("carrinho", carrinho);
    attQuantidade(carrinho[index]);
    attPrecoCarrinho();
  }
}

function decrementarQuantProduto(produto) {
  let carrinho = getLocalStorage("carrinho") ?? [];

  const index = carrinho.findIndex((p) => p.Id === produto.Id);
  if (index !== -1) {
    if (carrinho[index].Quantidade === 1) {
      removerDoCarrinho(produto);
    } else {
      carrinho[index].Quantidade--;
      setLocalStorage("carrinho", carrinho);
      attQuantidade(carrinho[index]);
      attPrecoCarrinho();
    }
  }
}

function removerDoCarrinho(produto) {
  let carrinho = getLocalStorage("carrinho") ?? [];
  carrinho = carrinho.filter((p) => p.Id !== produto.Id);
  setLocalStorage("carrinho", carrinho);
  atualizarCarrinho();
}

function atualizarCarrinho() {
  const itens = JSON.parse(localStorage.getItem("carrinho")) || [];

  produtosCarrinho.innerHTML = "";
  let total = 0;
  itens.forEach((produto) => {
    console.log(produto);
    const div = document.createElement("div");
    div.classList.add("mb-2");
    div.innerHTML = `
     <div class="card p-3 d-flex flex-column align-items-center" style="width: 18rem;">

  <div class="align-self-start">
    <button
      id="remover-item-${produto.Id}"
      class="text-danger material-symbols-outlined bg-transparent border-0"
      title="Remover"
    >
      delete
    </button>
  </div>

  <img
    src="/assets/css/global/Pelucias/${produto.Nome}.png"
    alt="Carrinho: ${produto.Nome}"
    class="img-thumbnail my-2"
    style="height: 18rem;"
  />

  <div class="text-center">
    <p class="text-dark small mb-1">${produto.Nome}</p>
    <p class="text-success fw-bold fs-5 mb-2">$${produto.PrecoUnitario}</p>
  </div>

  <div class="d-flex justify-content-center align-items-center fs-5 mt-auto">
    <button
      class="text-warning rounded-circle border-0 bg-transparent material-symbols-outlined"
      id="botao-decrementar-${produto.Id}"
    >
      do_not_disturb_on
    </button>
    <p id="quantidade-${produto.Id}" class="mx-3 my-0">
      ${produto.Quantidade}
    </p>
    <button
      class="text-success border-0 bg-transparent material-symbols-outlined"
      id="botao-incrementar-${produto.Id}"
    >
      add_circle
    </button>
  </div>

</div>
    `;
    produtosCarrinho.appendChild(div);
    total += produto.PrecoUnitario * produto.Quantidade;
    let produtoAtual;

    let produtosNoCarrinho = getLocalStorage("carrinho") ?? [];

    produtosNoCarrinho.forEach((produtoEstoque) => {
      if (produtoEstoque.Id === produto.Id) {
        produtoAtual = produtoEstoque;
      }
    });

    //Chamando a função de alterar a quantidade ou remover do carrinho (botões):
    const botaoAumentarQuantidade = document.getElementById(
      `botao-incrementar-${produto.Id}`
    );
    const botaoDiminuirQuantidade = document.getElementById(
      `botao-decrementar-${produto.Id}`
    );
    const removerItem = document.getElementById(`remover-item-${produto.Id}`);

    botaoAumentarQuantidade.addEventListener("click", () =>
      incrementarQuantProduto(produto)
    );
    botaoDiminuirQuantidade.addEventListener("click", () =>
      decrementarQuantProduto(produtoAtual)
    );
    removerItem.addEventListener("click", () =>
      removerDoCarrinho(produtoAtual)
    );
  });

  precoTotal.innerText = `Total: R$${total.toFixed(2)}`;
}

function renderizarProdutosCarrinho() {
  const containerProdutosCarrinho =
    document.getElementById("produtos-carrinho");
  containerProdutosCarrinho.innerHTML = "";

  for (const produto in produtosNoCarrinho) {
    atualizarCarrinho(produto);
  }
}

function attPrecoCarrinho() {
  const precoCarrinho = document.getElementById("preco-total");
  let precoTotal = 0;
  const carrinho = getLocalStorage("carrinho") ?? [];

  for (const produto of carrinho) {
    precoTotal += produto.PrecoUnitario * produto.Quantidade;
  }

  precoCarrinho.innerText = `Total: R$${precoTotal.toFixed(2)}`;
}

atualizarCarrinho();
