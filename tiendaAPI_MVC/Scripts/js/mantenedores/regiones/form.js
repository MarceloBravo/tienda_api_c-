var endPoint = 'http://localhost:4529/';

onload = function () {
    document.getElementById('btnGrabar').onclick = botones.grabar;
    document.getElementById('btnEliminar').onclick = botones.eliminar;
    document.getElementById('btnCancelar').onclick = botones.cancelar;
}


var botones = {

    grabar: function () {
        if (confirm("¿Desea grabar el registro?")) {
            var form = document.querySelector("form");
            var id = document.getElementById("Id").value;
            const datos = new FormData(form);
            var result = (id == "" || id == "0") ? grabar("regiones/Insertar", "POST", datos) : grabar("regiones/Actualizar/" + id, "PUT", datos);
            
        }

        function grabar(url, metodo, datos) {
            fetch(endPoint + url,
                {
                    method: metodo,
                    body: datos
                })
                .then(resp => resp.json())
                .then(data => {
                    console.log(data);
                    alert(data);
                    window.location = "/regiones";
                })
                .catch(error => {
                    console.log(error);
                    alert(error);
                    return false;
                })
        }
    },

    eliminar: function () {
        if (confirm("¿Desea eliminar el registro?")) {
            
            id = document.getElementById("Id").value;
            fetch(endPoint + "regiones/eliminar/" + id, { method: "DELETE" })
                .then(resp => resp.json())
                .then(data => {
                    alert(data);
                    window.location = "/regiones";
                })
                .catch(error => {
                    alert(error);
                })
        }
    },

    cancelar: function () {
        window.location = endPoint + "regiones";
    }
}

