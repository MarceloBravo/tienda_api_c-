onload = function () {
    document.getElementById("btn-login").onclick = botones.login;
}

var botones = {
    login: function () {
        nick = document.getElementById("nickname").value;
        pwd = document.getElementById("password").value;

        fetch("/login/autenticar", { method: 'POST', body: JSON.stringify({nickname:nick, password: pwd}) })
            .then(resp => resp.json())
            .then(function (data) {
                console.log(data)
            })
            .catch(function (error) {
                document.getElementById("alert-message").style.display = "block";
                document.getElementById("msg-error").innerHTML = error;
            });
    }
}