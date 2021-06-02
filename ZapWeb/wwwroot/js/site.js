﻿/* Cliente de Conexão e Reconexão com SignalR - Hub */

var connection = new signalR.HubConnectionBuilder().withUrl("/ZapWebHub").build();

function ConnectionStart() {
    connection.start().then(function () {
        HabilitarCadastro();
        console.info("Connected!");
    }).catch(function () {
        console.error(err.toSatring());
        setTimeout(ConnectionStart(), 5000);
    });
}

connection.onclose(async () => { await ConnectionStart(); });

function HabilitarCadastro() {
    var formCadastro = document.getElementById("form-cadastro");
    if (formCadastro != null) {
        var btnCadastrar = document.getElementById("btnCadastrar");

        btnCadastrar.addEventListener("click", function () {
            var nome = document.getElementById("nome").value;
            var email = document.getElementById("email").value;
            var senha = document.getElementById("senha").value;

            var usuario = { Nome: nome, Email: email, Senha: senha };

            connection.invoke("Cadastrar", usuario);
        });
    }

    connection.on("ReceberCadastro", function (sucesso, usuario, msg) {
        var mensagem = document.getElementById("mensagem");
        if (sucesso) {
            console.info(usuario);
            document.getElementById("nome").value = "";
            document.getElementById("email").value = "";
            document.getElementById("senha").value = "";
        }
        mensagem.innerText = msg;
    });
}

ConnectionStart();