onload = function () {
    document.getElementById("btnCerrarAlertaModal").onclick = modal.cerrar; 
    document.getElementById("cerrarAlertaModal").onclick = modal.cerrar; 
}

function agregarProducto(id, cantidad)
{
    fetch(`carrito/agregarproducto?id=${id}&cantidad=${cantidad}`)
        .then(resp => resp.json())
        .then(function (data) {
            document.getElementById("exampleModal").style.display = "block";
            document.getElementById("modalMessage").innerHTML = "El producto fue agregado a tu carrito de compras.";
            document.getElementById("modalTitle").innerHTML = "Producto agregado.";
        })
        .catch(function (error) {
            document.getElementById("exampleModal").style.display = "block";
            document.getElementById("modalMessage").innerHTML = error.message;
            document.getElementById("modalTitle").innerHTML = "Error";
            console.log(error);
        });
}


var modal = {
    cerrar: function () {
        document.getElementById("exampleModal").style.display = "none";
    }
}