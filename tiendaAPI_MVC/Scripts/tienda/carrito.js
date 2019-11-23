onload = function () {
    this.document.getElementById("btnActualizarCarrito").onclick = carrito.actualizar;
}

var carrito = {
    actualizar: function () {
        fetch(`/carrito/getCarrito`)
            .then(resp => resp.json())
            .then(function (data) {

            })
            .catch(exception e){

        }
    }
}