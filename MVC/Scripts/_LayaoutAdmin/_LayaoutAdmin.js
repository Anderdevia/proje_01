

cargarMenu();
cargarCentroOperacionesMenu();
cargarMenuInforme();
function cargarMenu() {


    let baseUrl = window.location.origin;
    let endpoint = "/Home/ListarMenu";

    // Verifica que la URL no tenga doble 'Home'
    if (baseUrl.includes("/Home")) {
        endpoint = "/ListarMenu";
    }



    $.ajax({
        url: baseUrl + endpoint,
        type: "GET",
        dataType: "json",
        success: function (response) {
            let resultado = '';

            console.log(response);
            for (let i in response) {
                resultado += `<a class="collapse-item" onclick="menu('${response[i].Controlador}');">${response[i].Controlador}</a>`;
            }

            $("#menuadmin").html(resultado);
        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });



}

function cargarCentroOperacionesMenu() {

    let baseUrl = window.location.origin;
    let endpoint = "/Home/ListarCentroOperaciones";

    // Verifica que la URL no tenga doble 'Home'
    if (baseUrl.includes("/Home")) {
        endpoint = "/ListarCentroOperaciones";
    }


    $.ajax({
        url: baseUrl + endpoint,
        type: "GET",
        dataType: "json",
        success: function (response) {
            let resultado = '';

            console.log(response);
            for (let i in response) {
                resultado += `<button class="menu-centros-operaciones" onclick="menuOperaciones('${response[i].Controlador}','${response[i].Id}','${response[i].Ver}');">${response[i].Nombre}</button>`;
            }

            $("#menuCentrosOperaciones").html(resultado);
        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });


}


function cargarMenuInforme() {
    const centrosOperaciones = [
        { Id: 1, Nombre: "Clientes", Controlador: "FiltroFecha", Ver: true }
    ];

    let resultado = '';

    centrosOperaciones.forEach(centro => {
        resultado += `<button class="menu-centros-operaciones" 
                          onclick="menuFiltros('${centro.Controlador}', '${centro.Id}', '${centro.Ver}');">
                          ${centro.Nombre}
                      </button>`;
    });

    document.getElementById("menuInformes").innerHTML = resultado;
}



function menu(modulo) {


    var modulocontroller = "/" + modulo + "/" + modulo + "";

    window.location.href = modulocontroller;

}


function menuFiltros(modulo, id, id_usuario) {


    var modulocontroller = "/CentroOperaciones/" + modulo + "";

    window.location.href = modulocontroller;


}

function menuOperaciones(modulo,id,id_usuario) {

    $("#IdCentroOperacion").val(id);
    $("#Modulo").val(modulo);
    $("#IdUsuario").val(id_usuario);

    verificarAcceso(id, modulo, 1, id_usuario);

    let modal = document.getElementById("habeasDataModalModulo");
    modal.style.display = "block";

}




function verificarAcceso(id_operacion, modulo, opcion, id_usuario){

    let baseUrl = window.location.origin;
    let endpoint = "/Home/verificaAccesoCentro";

    if (baseUrl.includes("/Home")) {
        endpoint = "/verificaAccesoCentro";
    }

  
    

    $.ajax({
        url: baseUrl + endpoint,
        type: 'POST',
        cache: false,
        data: {
            id_centro_operacion: id_operacion,
            opcion: opcion,
            id_usuario: id_usuario
        },
        dataType: 'json'
    }).done(function (res) {
        if (res[0].Nombre.length > 0) {


            if (res[0].Nombre === "NO EXISTE") {

                let modal = document.getElementById("habeasDataModalModulo");
                modal.style.display = "block";
            }
            else if (res[0].Nombre === "EXISTE") {


                var modulocontroller = "/" + modulo + "/DetalleCentroOperacion?id=" + id_operacion;
                window.location.href = modulocontroller;
            }
            

        }
        else {


        }
    }).fail(function (res) {

    });

}

document.getElementById("aceptarHabeasModulo").addEventListener("click", function () {
    let modal = document.getElementById("habeasDataModalModulo");
    modal.style.display = "none";

    var idCentro = $("#IdCentroOperacion").val();
    var nombreModulo = $("#Modulo").val();
    var idUsuario = $("#IdUsuario").val();

    verificarAcceso(idCentro, nombreModulo, 2, idUsuario);
});


document.getElementById("rechazarHabeasModulo").addEventListener("click", function () {

  

    var idCentro = $("#IdCentroOperacion").val();
    var nombreModulo = $("#Modulo").val();
    var idUsuario = $("#IdUsuario").val();

    verificarAcceso(idCentro, nombreModulo, 3, idUsuario); 

    setTimeout(() => {
   

        let modal = document.getElementById("habeasDataModalModulo");
        modal.style.display = "none";

    }, 1000); 

});