function agregarProducto(id, cantidad)
{
    fetch(`carrito/agregarproducto?id=${id}&cantidad=${cantidad}`)
        .then(resp => resp.json())
        .then(function (data) {
            alert("El producto fuen agregado.");
        })
        .catch(function (error) {
            alert("Ocurrio un error al agregar el producto al carrito.");
            console.log(error);
        });
}