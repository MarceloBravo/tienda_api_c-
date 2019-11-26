fetch(`/carrito/getCarrito`)
    .then(resp => resp.json())
    .then(function (data) {
        var cantidad = 0;
        Object.entries(data).forEach(function (elem, index) {
            cantidad += eval(elem[1].cantidad)
        })
        document.getElementById("lbl-carrito").innerHTML = cantidad;
    })
    .catch(function (error) {
        document.getElementById("lbl-carrito").innerHTML = "Error";
        console.log(error);
    });