onload = function () {
    document.getElementById("btnAgregarProducto").onclick = agregarProductoAlCarrito
    document.getElementById("btnCerrarAlertaModal").onclick = modal.cerrar;
    document.getElementById("cerrarAlertaModal").onclick = modal.cerrar; //Botón cerrar del cuadro de diálogo
    document.getElementById("btnCerrarModal").onclick = cerrarPreView;   //Botón cerrar del popup de vista previa del producto
}

//Busca los datos del producto y muestra sus datos en un cuadro popup
function preView(idImg, idProd) {
    fetch("Producto/buscar/" + idProd)
        .then(resp => resp.json())
        .then(function (data) {
            document.getElementById("btnAgregarProducto").dataset.idProducto = idProd;
            document.getElementById("preview-item").style.display = "block";
            document.getElementById("img-preview").src = document.getElementById("img-" + idImg).src;
            document.getElementById("nombre-item").innerHTML = data.Nombre;
            document.getElementById("descripcion-item").innerHTML = data.Descripcion;
            document.getElementById("precio-anterior-item").innerHTML = currencyFormat(data.precioAnterior);
            document.getElementById("precio-actual-item").innerHTML = currencyFormat(data.Precio);
            document.getElementById("marca-item").innerHTML = data.Marca.Nombre;
            document.getElementById("color-item").innerHTML = "Color: "+data.color;
        })
        .catch(function (error) {
            console.log(error);
            alert(error);
        })
}

//Da formato de moneda chilena a un numero
function currencyFormat(num) {
    return '$' + num.toFixed(0).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.')
}

//Agrega un pructo al carrito de compras del usuario
function agregarProductoAlCarrito() {
    var idProd = document.getElementById("btnAgregarProducto").dataset.idProducto;
    agregarProducto(idProd, 1);
    cerrarPreView();
}

//Cierra el popup de vista previa del producto
function cerrarPreView() {
    document.getElementById("preview-item").style.display = "none";
}