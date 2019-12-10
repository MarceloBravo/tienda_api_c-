onload = function () {
    document.getElementById("btnCerrarAlertaModal").onclick = modal.cerrar;
    document.getElementById("cerrarAlertaModal").onclick = modal.cerrar;
}

function agregarProducto(id, cantidad)
{
    fetch(`carrito/agregarproducto?id=${id}&cantidad=${cantidad}`)
        .then(resp => resp.json())
        .then(function (data) {
            console.log(data)
            
            document.getElementById("cantidad-" + id).value = data[id].cantidad;
            document.getElementById("precio-" + id).innerHTML = "$" + (data[id].producto.Precio * data[id].cantidad).toLocaleString();
            var datos = Object.entries(data);
            var total = 0;
            datos.forEach(function (elem, id) {
                total += elem[1].cantidad * elem[1].producto.Precio;
            });
            document.getElementById("totalVenta").innerHTML = "$" + total.toLocaleString('de-DE');
        })
        .catch(function (error) {
            document.getElementById("exampleModal").style.display = "block";
            document.getElementById("modalMessage").innerHTML = error.message;
            document.getElementById("modalTitle").innerHTML = "Error";
            console.log(error);
        })
}

var modal = {
    cerrar: function () {
        document.getElementById("exampleModal").style.display = "none";
    }
}