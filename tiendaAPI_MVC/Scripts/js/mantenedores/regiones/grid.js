var endPoint = 'http://localhost:4529/';

onload = function () {    
    document.getElementById('filtro').onchange = grilla.filtrar;
    document.getElementById('btnNuevo').onclick = registros.nuevo;

    grilla.cargar();
}

var grilla = {
    cargar: function (pagina) {
        if (pagina == undefined) pagina = 1;
        var tBody = document.getElementById('tbRegistros');

        fetch(endPoint + 'regiones/listar?pagina='+pagina)
            .then(resp => resp.json())
            .then(data => {
                regiones = JSON.parse(data);

                var regionesLst = regiones.registros.map(r => `<tr>
                                                                <td>${r.Nombre}</td>
                                                                <td>${r.Pais.Nombre}</td>
                                                                <td>${r.Created_at != null ? new Date(r.Created_at).format("d/m/yy") : "" }</td>
                                                                <td>${r.Updated_at != null ? new Date(r.Updated_at).format("d/m/yy") : "" }</td>
                                                                <td>${r.Deleted_at != null ? new Date(r.Deleted_at).format("d/m/yy") : "" }</td>
                                                                <td class="col-acciones">
                                                                    <button type="button" class="btn btn-success btn-sm" onclick="editar(${r.Id})"><i class="fa fa-edit"></i></button>
                                                                    <button type="button" class="btn btn-danger btn-sm" onclick="eliminar(${r.Id})"><i class="fa fa-trash"></i></button>
                                                                </td>
                                                            </tr>`);

                tBody.innerHTML = regionesLst.join("");
                paginacion.paginar(regiones.pagina, regiones.totalPaginas, regiones.pagVisibles, regiones.totalRegistros);
                
            })
            .catch(error => {
                console.log(error)
                tBody.innerHTML = `<tr><td colspan="6">${error}</td></tr>`;
            })
    },


    filtrar: function (pagina) {
        if (pagina == undefined) { pagina = 1 }

        //Solicita una búsqueda en la base de datos para luego retornar sólo los registros que coinciden con el filtro aplicado y cargarlos en la grilla
        var filtro = document.getElementById("filtro").value;
        if (filtro == "") {
            grilla.cargar(pagina);
        } else {
            aplicarFiltro(pagina, filtro);
        }


        function aplicarFiltro(pagina, filtro) {
            var tBody = document.getElementById('tbRegistros');
            var filtro = document.getElementById('filtro').value;

            fetch(endPoint + 'regiones/filtrar?filtro=' + filtro + '&pagina=' + pagina)
                .then(resp => resp.json())
                .then(data => {
                    regiones = JSON.parse(data);

                    var regionesLst = regiones.registros.map(r => `<tr>
                                                                <td>${r.Nombre}</td>
                                                                <td>${r.Pais.Nombre}</td>
                                                                <td>${r.Created_at != null ? new Date(r.Created_at).format("d/m/yy") : ""}</td>
                                                                <td>${r.Updated_at != null ? new Date(r.Updated_at).format("d/m/yy") : ""}</td>
                                                                <td>${r.Deleted_at != null ? new Date(r.Deleted_at).format("d/m/yy") : ""}</td>
                                                                <td class="col-acciones">
                                                                    <button type="button" class="btn btn-success btn-sm" onclick="editar(${r.Id})"><i class="fa fa-edit"></i></button>
                                                                    <button type="button" class="btn btn-danger btn-sm" onclick="eliminar(${r.Id})"><i class="fa fa-trash"></i></button>
                                                                </td>
                                                            </tr>`);

                    tBody.innerHTML = regionesLst.join("");
                    paginacion.paginar(regiones.pagina, regiones.totalPaginas, regiones.pagVisibles, regiones.totalRegistros);

                })
                .catch(error => {
                    console.log(error)
                    tBody.innerHTML = `<tr><td colspan="6">${error}</td></tr>`;
                })
        }
    }

    
}

var registros = {

    nuevo: function () {
        window.location = endPoint + "/regiones/Form";
    },

    editar: function (id) {
        window.location = endPoint + "regiones/editar/" + id;
    },

    eliminar: function (id) {
        if (confirm("¿Desea eliminar el registro?")) {

            fetch(endPoint + "regiones/Eliminar/" + id, { method: "delete" })
                .then(resp => resp.json())
                .then(data => {
                    grilla.cargar(1);
                    console.log(data)
                    alert(data);
                })
                .catch(error => {
                    alert(error)
                    console.log(error)
                })
        }
    }
}

function eliminar(id) { registros.eliminar(id) }

function editar(id) { registros.editar(id) }