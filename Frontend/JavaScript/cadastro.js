document.addEventListener("DOMContentLoaded", function() {
    const botaoCriar = document.querySelector(".botao-criar-conta");

    botaoCriar.addEventListener("click", function(event) {
        event.preventDefault();

        // Captura dos campos
        const email = document.getElementById("email").value.trim();
        const nome = document.getElementById("nome").value.trim();
        const sobrenome = document.getElementById("sobrenome").value.trim();
        const senha = document.getElementById("Senha").value;
        const confirmarSenha = document.getElementById("Senha2").value;
        const cpf = document.getElementById("CPF").value.trim();
        const celular = document.getElementById("Celular").value.trim();
        const cep = document.getElementById("CEP").value.trim();
        const endereco = document.getElementById("Endereço").value.trim();

        // Validação simples
        if (!email || !nome || !sobrenome || !senha || !confirmarSenha || !cpf || !celular || !cep || !endereco) {
            alert("Preencha todos os campos obrigatórios!");
            return;
        }

        if (senha !== confirmarSenha) {
            alert("As senhas não coincidem!");
            return;
        }
        const nomeCompleto = `${nome} ${sobrenome}`;
        const novoUsuario = {
            email: email,
            nome: nomeCompleto,
            senha: senha,
            endereco: endereco
        };
        enviarParaAPI(novoUsuario);
    });


    // Função que você vai usar no futuro para a API
    function enviarParaAPI(usuario) {
        fetch("http://localhost:5050/cadastrarUsuario", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(usuario)
        })
        .then(response => {
            if (response.ok) {
                alert("Conta criada com sucesso!");
                window.location.href = "login.html";
            } else {
                alert("Erro ao criar conta. Verifique os dados.");
            }
        })
        .catch(error => {
            console.error("Erro na requisição:", error);
            alert("Erro ao conectar com o servidor.");
        });
    }
});
