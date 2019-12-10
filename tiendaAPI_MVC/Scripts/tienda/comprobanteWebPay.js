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
        })
            .then(function (data) {
                console.log(data)
            })
        .catch(function (error) {
            console.log(error)
        });

    }
}