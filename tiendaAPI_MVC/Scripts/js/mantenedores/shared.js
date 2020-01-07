
var paginacion = {
    paginar: function (pagActual, totPaginas, pagVisibles, totRegistros) {
        var paginacion = document.getElementById('paginacion');
        var filtro = document.getElementById('filtro').value;

        paginacion.innerHTML = actualizarPaginacion(filtro ? "filtrar" : "cargar", pagActual, totPaginas, pagVisibles, totRegistros);



        function actualizarPaginacion(funcion, pagActual, totPaginas, pagVisibles, totRegistros) {
            var BackButtonEvent = pagActual > 1 ? `onclick="${funcion}(${(pagActual-1)})"` : "";
            var NextButtonEvent = pagActual < totPaginas ? `onclick="${funcion}(${pagActual+1})"` : "";
            
            var desde = pagDesde(pagActual, pagVisibles);
            var hasta = pagHasta(pagActual, pagVisibles, totPaginas);

            var strPaginas = `<li class="page - item"><button class="btn btn-button btn-sm" ${BackButtonEvent} > Anterior</button></li>`

            for (x = desde; x <= hasta; x++) {
                strPaginas += `<li class="page-item"><button class="btn btn-button btn-sm" onclick="${funcion}(${x})">${x}</button></li>`;
            }

            strPaginas += `<li class="page-item"><button class="btn btn-button btn-sm" ${NextButtonEvent} > Siguiente</button></li >`;

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
}

function filtrar(pagina) {
    grilla.filtrar(pagina);
}

function cargar(pagina) {
    grilla.cargar(pagina);
}