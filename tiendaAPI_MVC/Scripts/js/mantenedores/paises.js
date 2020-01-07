var endPoint = 'http://localhost:4529/';

onload = function () {
    //Asiciando eeventos a los botones
    document.getElementById('btnNuevo').onclick = buttons.nuevo;
    document.getElementById('btnGrabar').onclick = buttons.grabar;
    document.getElementById('btnEliminar').onclick = buttons.eliminar;
    document.getElementById('btnCancelar').onclick = buttons.cancelar;
    document.getElementById('filtro').onchange = grilla.filtrar;

    grilla.cargar();    //Cargando el listado de registros en la grilla

}


//Objeto encargado de manipular la grilla
var grilla = {

    //Carga la grilla con los registros de los paises existentes en la base de datos
    cargar: function (pagina) {
        if (pagina == undefined) { pagina = 1 }

        fetch(endPoint + 'Pais/listar?pagina=' + pagina)
            .then(res => res.json())
            .then(function (data) {
                paises = JSON.parse(data);

                var lstPaises = paises.registros.map(p => 
                                    `<tr>
                                        <td>${p.Nombre}</td>
                                        <td>${new Date(p.Created_at).format("d/m/Y")}</td>
                                        <td>${new Date(p.Updated_at).format("d/m/Y")}</td>
                                        <td>${p.Deleted_at != null ? new Date(p.Deleted_at).format("d/m/Y") : ""}</td>
                                        <td class="col-acciones">
                                            <button type="button" class="btn btn-success btn-sm" onclick="editar(${p.Id})"><i class="fa fa-edit" aria-hidden="true"></i></button>
                                            <button type="button" class="btn btn-danger btn-sm" onclick="eliminar(${p.Id})"><i class="fa fa-trash" aria-hidden="true"></i></button>
                                        </td>
                                    </tr>`)

                document.getElementById('tbPaises').innerHTML = lstPaises.join("");

                paginacion.paginar(paises.pagina, paises.totalPaginas, paises.pagVisibles, paises.totalRegistros);
            })
            .catch(function (error) {
                document.getElementById('tbPaises').innerHTML = `<tr>
                                                                     <td colspan="5">${error}</td>
                                                                </tr>`
                console.log(error);
            });
    },

    filtrar: function (pagina) {
        if (pagina == undefined) {pagina = 1}

        //Solicita una búsqueda en la base de datos para luego retornar sólo los registros que coinciden con el filtro aplicado y cargarlos en la grilla
        var filtro = document.getElementById("filtro").value;
        if (filtro == "") {
            grilla.cargar();
        } else {
            aplicarFiltro(filtro, pagina);
        }

        function aplicarFiltro(filtro, pagina) {           
            fetch(endPoint + "Pais/filtrar?filtro=" + filtro + "&pagina=" + pagina)
                .then(res => res.json())
                .then(data => {

                    var result = ""
                    if (typeof data != "string") {

                        lstPaises = data.registros.map(p => `<tr>
                                                <td>${p.Nombre}</td>
                                                <td>${new Date(Date(p.Created_at)).toString("d/m/y")}</td>
                                                <td>${new Date(Date(p.Updated_at)).toString("d/m/y")}</td>
                                                <td>${ p.Deleted_at != null ? new Date(Date(p.Deleted_at)).toString("d/m/y") : ""}</td >
                                                <td>
                                                    <button type="button" class="btn btn-success btn-sm" onclick="editar(${p.Id})"><i class="fa fa-edit" aria-hidden="true"></i></button>
                                                    <button type="button" class="btn btn-danger btn-sm" onclick="eliminar(${p.Id})"><i class="fa fa-trash" aria-hidden="true"></i></button>
                                                </td>
                                            </tr>`);


                        result = lstPaises.join("");
                    } else {
                        result = `<tr><td colspan="5">${data}</td></tr>`;
                    }
                    document.getElementById('tbPaises').innerHTML = result;
                    paginacion.paginar(data.pagina, data.totalPaginas, data.pagVisibles, data.totalRegistros);
                })
                .catch(error => {
                    console.log(error)
                    var messageRow = `<tr>
                                    <td colspan="5">${error.message}</td>
                                </tr>`;
                    document.getElementById('tbPaises').innerHTML = messageRow;
                })
        }
    },

    /*
    paginacion: function (pagActual, totPaginas, pagVisibles, totRegistros) {
        var paginacion = document.getElementById('paginacion');
        var filtro = document.getElementById('filtro').value;

        paginacion.innerHTML = actualizarPaginacion(filtro ? "filtrar" : "cargar", pagActual, totPaginas, pagVisibles, totRegistros);
        


        function actualizarPaginacion(funcion, pagActual, totPaginas, pagVisibles, totRegistros) {
            var BackButtonEvent = pagActual > 1 ? `onclick="grilla.${funcion}(${(pagActual--)})"` : "";
            var NextButtonEvent = pagActual < totPaginas ? `onclick="grilla.${ funcion }(${ pagActual++ })"` : "";            
            
            var desde = pagDesde(pagActual, pagVisibles);
            var hasta = pagHasta(pagActual, pagVisibles, totPaginas);

            var strPaginas = `<li class="page - item"><a class="page - link" ${BackButtonEvent} > Anterior</a ></li >`

            for (x = desde; x <= hasta; x++) {
                strPaginas += `<li class="page-item"><a class="page-link" onclick="grilla.${funcion}(${x})">${x}</a></li >`;
            }

            strPaginas += `<li class="page-item"><a class="page-link" ${NextButtonEvent} > Siguiente</a ></li >`;

            return strPaginas;
        }

        function pagDesde(pagActual, pagVisibles) {
            var res = pagActual - pagVisibles;
            return res < 1 ? 1 : res;
        }

        function pagHasta(pagActual, pagVisibles, totPag) {
            var res = pagActual + pagVisibles;
            return res >= totPag ? totPag : res;
        }
    }
    */
};



//Objeto encargado encargado de la manipulación de los datos del registros
var registro = {
    nuevo: function () {
        buttons.nuevo();
    },


    //Busca un registro cuyo id coincidacon el recibido en el parametro id, luego muestra el formulario
    //de edición con los datos del registro encontrado, en caso de no encontrar el registro recibe un 
    //mensaje de error
    buscar: function (id) {
        fetch(endPoint + 'Pais/Show/' + id)
            .then(res => res.json())
            .then(function (data) {
                document.getElementById('Id').value = data.Id;
                document.getElementById('Nombre').value = data.Nombre;
                document.getElementById('Created_at').value = new Date(Date(data.Created_at)).format("d/m/Y");
            })
            .catch(function (error) {
                console.log(error);
            });
    },


    //Graba los datos de un registro
    grabar: async function () {

        form = document.querySelector("form");
        const datos = new FormData(form);
        id = document.getElementById('Id').value;
        resp = id == "" ? await insertar(datos) : await actualizar(id, datos);
        grilla.cargar();    //Carga la grilla con los datos
        buttons.cancelar(); //Oculta el formulario y muestra la grilla
        

        function insertar(datos) {            
            grabar(endPoint + 'Pais/Create/', datos, 'POST');
        }

        function actualizar(id, datos) {
            grabar(endPoint + 'Pais/Update/' + id, datos , 'PUT');
        }

        function grabar(url, datos, metodo) {
            fetch(url, {
                method: metodo,
                body: datos
            })
                .then(res => res.json())
                .then(function (data) {
                    console.log(data);
                })
                .catch(function (error) {
                    console.log(error);
                })
        }
        
    },

    //Elimina un registro existente
    eliminar: function (id) {        
        
        if (id == undefined) {
            id = document.getElementById('Id').value;
        }
        if (confirm("¿Desea eliminar el registro?")) {
            
            fetch(endPoint + "Pais/Delete/" + id, {method: "DELETE"})
                .then(resp => resp.json())
                .then(function (data) {
                    alert(data);
                })
                .catch(error => {
                        console.log(error)
                        alert(error)
                    }
                )
        }
    }

}


//Objeto encargado de contener los eventos de los botones
var buttons = {
    nuevo: function () {
        document.getElementById('div-form').style.display = 'block';
        document.getElementById('div-grid').style.display = 'none';
        document.getElementById('Id').value = '';
        document.getElementById('btnEliminar').style.visibility = 'hidden';
        document.querySelector('form').reset();
    },

    grabar: function () {
        if (confirm("¿Desea grabar el registro?")) {
            registro.grabar();
        }
    },

    eliminar: function () {
        registro.eliminar();
    },

    cancelar: function () {
        document.getElementById('div-form').style.display = 'none';
        document.getElementById('div-grid').style.display = 'block';
    }
}

function editar(id) {    
    registro.nuevo();
    document.getElementById('btnEliminar').style.visibility = 'visible';
    registro.buscar(id);
}

function eliminar(id) {    
    registro.eliminar(id);
}