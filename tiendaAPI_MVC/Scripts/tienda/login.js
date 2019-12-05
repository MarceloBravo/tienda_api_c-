onload = function () {
    document.getElementById("btn-login").onclick = botones.login;
    document.getElementById("close-message-button").onclick = botones.cerrarMensaje;
}

var botones = {
    login: function () {
        nick = document.getElementById("nickname").value;
        pwd = document.getElementById("password").value;
        
        fetch("/login/autenticar", { method: 'POST', body: new URLSearchParams(`nickname=${nick}&password=${pwd}`) })
            .then(resp => resp.json())
            .then(function (response) {
                if (response) {
                    window.location = "/Carrito/EfectuarPagoCompra";
                } else {
                    document.getElementById("alert-message").style.display = "block";
                    document.getElementById("msg-error").innerHTML = response;
                }
            })
            .catch(function (error) {
                document.getElementById("alert-message").style.display = "block";
                document.getElementById("msg-error").innerHTML = error.message;
            });
    },

    cerrarMensaje: function () {
        document.getElementById("alert-message").style.display = "none";
    }
}