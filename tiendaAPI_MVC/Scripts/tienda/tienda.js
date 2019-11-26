onload = function () {
    document.getElementById("btnCerrarAlertaModal").onclick = modal.cerrar; 
    document.getElementById("cerrarAlertaModal").onclick = modal.cerrar; 
}

function agregarProducto(id, cantidad)
{
    fetch(`carrito/agregarproducto?id=${id}&cantidad=${cantidad}`)
        .then(resp => resp.json())
        .then(function (data) {
            var cantidad = 0;           
            Object.entries(data).forEach(function (elem, index) {
                cantidad += eval(elem[1].cantidad)
            })
            document.getElementById("lbl-carrito").innerHTML = cantidad;
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