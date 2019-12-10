$("#sendEmail").click(function () {
    email.enviar();
});
//body: new URLSearchParams(`comprobante=${documento}`)
var email = {
    enviar: function () {
        var doc = document.getElementById("comprobanteVenta").innerHTML;
        documento = { comprobante: doc.split("\"").join("'") };
        fetch("/SuccessWebPay/EnviarCorreoVenta", {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(documento)
        }).then(resp => resp.json())
            .then(function (data) {                
                alert(data.mensaje);
                window.location = "/";
            })
            .catch(function (error) {
                alert(error);
                console.log(error)
        });

    }
}