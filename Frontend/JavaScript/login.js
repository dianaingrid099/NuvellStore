document.addEventListener("DOMContentLoaded", function () {
  const form = document.getElementById("formulario-login");

  checkUsuarioLogado();

  if (form !== null) {
    form.addEventListener("submit", function (event) {
      console.log("login");
      event.preventDefault();

      const email = document.getElementById("email").value.trim();
      const senha = document.getElementById("senha").value;

      if (!email || !senha) {
        alert("Preencha todos os campos.");
        return;
      }

      validarLoginAPI(email, senha);
    });
  }

  const botaoSair = document.getElementById("botao-sair");

  botaoSair.addEventListener("click", () => {
    console.log("logout");
    localStorage.removeItem("usuarioLogado");
    window.location.href = "index.html";
  });
});

botaoCheckout.addEventListener("click", () => checkLoginAoFinalizarCompra());

function checkLoginAoFinalizarCompra() {
  console.log("CheckFinalizarCompra");
  const usuarioLogado = localStorage.getItem("usuarioLogado");

  if (!usuarioLogado) {
    alert("Aviso! Entre em uma conta para finalizar a compra!");
    window.location.href = "login.html";
  } else {
    window.location.href = "compra.html";
  }
}

function checkUsuarioLogado() {
  const usuarioLogado = localStorage.getItem("usuarioLogado");

  const botaoEntrar = document.getElementById("botao-entrar");
  const botaoSair = document.getElementById("botao-sair");

  if (usuarioLogado) {
    botaoEntrar.style.display = "none";
    botaoSair.style.display = "inline-block";
  } else {
    botaoEntrar.style.display = "inline-block";
    botaoSair.style.display = "none";
  }
}

function validarLoginAPI(email, senha) {
  console.log("caiu no validar loginapi");
  fetch("http://localhost:5050/login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      email: email,
      senha: senha,
    }),
  })
    .then((response) => {
      if (response.ok) {
        localStorage.setItem("usuarioLogado", true);
        return response.json();
      } else {
        throw new Error("Login inválido");
      }
    })
    .then((data) => {
      alert("Login bem-sucedido!");
      window.location.href = "index.html";
    })
    .catch((error) => {
      console.error("Erro no login:", error);
      alert("Email ou senha incorretos.");
    });
}
