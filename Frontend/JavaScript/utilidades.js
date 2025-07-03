//Salvando os itens do carrinho no armazenamento local do navegador:
  
export function setLocalStorage(key, value){
  localStorage.setItem(key, JSON.stringify(value))
}

export function getLocalStorage(key){
  return JSON.parse(localStorage.getItem(key));
}

export function deleteFromLocalStorage(key){
  localStorage.removeItem(key);
}

//Cards dos produtos na página de checkout:
export function desenharProdutoSimples(idProduto, idContainerHtml, quantidadeProduto){
  const produto = catalogo.find((p) => p.id === idProduto);
  
  const containerProdutosCarrinho = document.getElementById(idContainerHtml);

    const elementoArticle = document.createElement("article");
    const articleClasses = [
      "flex",
      "relative",
      "w-[427px]",
      "rounded-lg",
      "overflow-hidden",
      "my-2",
      "p-2",
    ];
  
    for (const articleClass of articleClasses) {
      elementoArticle.classList.add(articleClass);
    }
  
    const cardProdutoCarrinho = `        
      <div class="flex w-[100%] mx-2 p-1 rounded-md shadow-md shadow-slate-500 bg-slate-50">
      
      <img
        src="./assets/img/${produto.imagem}"
        alt="Carrinho: ${produto.nome}"
        class="h-[150px]"
      />

      <div class="ml-3 py-1 flex flex-col justify-between">
        <p class="text-slate-900 text-md">${produto.nome}</p>
        <p class="text-slate-800 text-sm">Tamanho: M</p>
        <p class="text-emerald-900 text-lg">$${produto.preco}</p>
      </div>
      <div class="flex absolute text-lg bottom-3 right-20 text-slate-950 items-end">

          <p class='relative' id="quantidade-${produto.id}" class="mx-3">x ${
      quantidadeProduto}</p>
    
      </div>
      </div>`;
  
    
    elementoArticle.innerHTML = cardProdutoCarrinho;
    containerProdutosCarrinho.appendChild(elementoArticle);
    
  }