
var idGlobal;

$(document).ready(function () {
    listarCentroOperacions();


});


function listarCentroOperacions() {
    const table = document.querySelector("tbody");

    fetch("ListaCentroOperacion").
        then(response => response.json()).
        then(respuesta => {
            console.log(respuesta)
            let resultado = "";
            for (let i = 0; i < respuesta.length; i++) {


                resultado += '<tr class="small-font text-center" taskId=' + respuesta[i].Id + '>' + 
                    '<td>' + respuesta[i].Codigo + '</td>' +
                    '<td class="small-font text-center">' + respuesta[i].Nombre + '</td>' +
                    '<td class="small-font text-center">' + respuesta[i].Descripcion + '</td>' +
                    '<td class="small-font text-center">' + respuesta[i].Direccion + '</td>' +
                    '<td class="small-font text-center">' + respuesta[i].Telefono + '</td>' +
                    '<td class="small-font text-center">' + respuesta[i].Correo + '</td>' +
                    '<td>' + "<button onclick='editarCentroOperacion(" + respuesta[i].Codigo + ")'  type='button' data-toggle='modal'  data-target='#ModalActualizar'" +
                    "class='btn btn-small-table'  style='padding: 1px;' id='idEditar'><i class='fa fa-spinner'></button>" + '</td>' +
                    '<td>' + "<button onclick='eliminaCentroOperacion(" + respuesta[i].Codigo + ")' type='button'" +
                    "class='btn btn-small-table' id='idEliminar' style='padding: 1px;'><i class='fa fa-trash'></button>" + "</td>" +
                    '</tr>'

            }

            table.innerHTML = resultado;
            $('#tablaCentroOperacion').DataTable({
                language: {
                    "decimal": "",
                    "emptyTable": "No hay información",
                    "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
                    "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
                    "infoFiltered": "(Filtrado de _MAX_ total entradas)",
                    "infoPostFix": "",
                    "thousands": ",",
                    "lengthMenu": "Mostrar _MENU_ Entradas",
                    "loadingRecords": "Cargando...",
                    "processing": "Procesando...",
                    "search": "Buscar:",
                    "zeroRecords": "Sin resultados encontrados",
                    "paginate": {
                        "first": "Primero",
                        "last": "Ultimo",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    }
                },
            });

        });


}


//AGREGAR
const formulario = document.getElementById("formRegistra");
formulario.addEventListener('submit', function (e) {

    let formData = new FormData();
    let files = $('#imagenCentroOperacion')[0].files;

    for (var i = 0; i < files.length; i++) {
        formData.append('files', files[i]);

    }

    var id = document.getElementById("codigo").value;



    e.preventDefault();

    // Obtener los valores de los campos
    let nombre = document.getElementById("nombreCentroOperacion").value;
    let descripcion = document.getElementById("descripcionCentroOperacion").value;
    let direccion = document.getElementById("direccionCentroOperacion").value;
    let telefono = document.getElementById("telefonoCentroOperacion").value;
    let correo = document.getElementById("correoCentroOperacion").value;
    let imagen = document.getElementById("imagenCentroOperacion").value;

    // Verificar si los campos están vacíos y cambiar el borde a rojo si es necesario
    if (nombre === "") {
        document.getElementById("nombreCentroOperacion").style.borderColor = "red";
    } else {
        document.getElementById("nombreCentroOperacion").style.borderColor = "";
    }

    if (descripcion === "") {
        document.getElementById("descripcionCentroOperacion").style.borderColor = "red";
    } else {
        document.getElementById("descripcionCentroOperacion").style.borderColor = "";
    }


    if (direccion === "") {
        document.getElementById("direccionCentroOperacion").style.borderColor = "red";
    } else {
        document.getElementById("direccionCentroOperacion").style.borderColor = "";
    }

    if (telefono === "") {
        document.getElementById("telefonoCentroOperacion").style.borderColor = "red";
    } else {
        document.getElementById("telefonoCentroOperacion").style.borderColor = "";
    }

    if (correo === "") {
        document.getElementById("correoCentroOperacion").style.borderColor = "red";
    } else {
        document.getElementById("correoCentroOperacion").style.borderColor = "";
    }

    if (imagen === "") {
        document.getElementById("imagenCentroOperacion").style.borderColor = "red";
    } else {
        document.getElementById("imagenCentroOperacion").style.borderColor = "";
    }


    if (nombre === "" || descripcion === "" || imagen === "" || direccion === "" || telefono === "" || correo === "") {
       
        errorMessage.textContent = "Todos los campos son obligatorios. Por favor, completa los campos en Rojo.";
        errorMessage.style.display = "block";
       
        return; 
    } else {
        var cdert = files[0].name;
    }

    var datosCentroOperacion = {
        Nombre: nombre,
        Descripcion: descripcion,
        Direccion: direccion,
        Telefono: telefono,
        Correo: correo,
        NombreImagen: "~/img/" + files[0].name

    };

    $.ajax({
        url: "nuevoCentroOperacion",
        type: 'POST',
        cache: false,
        data: {
            centroOperaciones: datosCentroOperacion
        },
        dataType: 'json'
    }).done(function (res) {
        if (res.success) {
            var IdProveedorValida = 1;

            var datos = new FormData(formulario);
            fetch("imagenCentroOperacion", {
                method: 'POST',
                body: datos
            }).
                then(response => response.text()).
                then(respuesta => {
                    console.log("Agregado");

                    $('#ModalAgregar').modal('hide')


                    listarCentroOperacions();
                    $('#tablaCentroOperacion').DataTable().destroy();
                    cargarCentroOperacionesMenu();
                    resetearCentroOperacion();
                    if (id == 0) {
                        Swal.fire({
                            position: 'top',
                            icon: 'success',
                            title: 'Centro de Operación Registrado',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    } else {
                        Swal.fire({
                            position: 'top',
                            icon: 'success',
                            title: 'Centro de Operación Actualizado',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }
                });

        }
        else {
        }
    }).fail(function (res) {

    });

});



//Actualizar
const formularioA = document.getElementById("formActualiza");
formularioA.addEventListener('submit', function (e) {

    let formDataA = new FormData();
    let filesA = $('#imagenCentroOperacionA')[0].files;

    for (var i = 0; i < filesA.length; i++) {
        formDataA.append('files', filesA[i]);

    }

    var idA = document.getElementById("codigoA").value;



    e.preventDefault();

    // Obtener los valores de los campos
    let nombreA = document.getElementById("nombreCentroOperacionA").value;
    let descripcionA = document.getElementById("descripcionCentroOperacionA").value;
    let nombreRuta = document.getElementById("nombreRutaA").value;
    let direccionA = document.getElementById("direccionCentroOperacionA").value;
    let telefonoA = document.getElementById("telefonoCentroOperacionA").value;
    let correoA = document.getElementById("correoCentroOperacionA").value;
    let imagenA = document.getElementById("imagenCentroOperacionA").value;

    // Verificar si los campos están vacíos y cambiar el borde a rojo si es necesario
    if (nombreA === "") {
        document.getElementById("nombreCentroOperacionA").style.borderColor = "red";
    } else {
        document.getElementById("nombreCentroOperacionA").style.borderColor = "";
    }

    if (descripcionA === "") {
        document.getElementById("descripcionCentroOperacionA").style.borderColor = "red";
    } else {
        document.getElementById("descripcionCentroOperacionA").style.borderColor = "";
    }

    if (direccionA === "") {
        document.getElementById("direccionCentroOperacionA").style.borderColor = "red";
    } else {
        document.getElementById("direccionCentroOperacionA").style.borderColor = "";
    }

    if (telefonoA === "") {
        document.getElementById("telefonoCentroOperacionA").style.borderColor = "red";
    } else {
        document.getElementById("telefonoCentroOperacionA").style.borderColor = "";
    }

    if (correoA === "") {
        document.getElementById("correoCentroOperacionA").style.borderColor = "red";
    } else {
        document.getElementById("correoCentroOperacionA").style.borderColor = "";
    }



   
    if (nombreA === "" || descripcionA === "" || direccionA === "" || telefonoA === "" || correoA === "" ) {
        
        errorMessageA.textContent = "Todos los campos son obligatorios. Por favor, completa los campos en Rojo.";
        errorMessageA.style.display = "block"; 
      
        return; 
    }

    var rutaNombreIma = "";
    var IfActualizarImagen = 0;

    if (filesA.length > 0) {
        rutaNombreIma = "~/img/" + filesA[0].name;

        IfActualizarImagen = 1;
    } else if (nombreRuta.length > 0) {
        rutaNombreIma = nombreRuta;
    }


    var datosCentroOperacionA = {
        Codigo: idA,
        Nombre: nombreA,
        Descripcion: descripcionA,
        Direccion: direccionA,
        Telefono: telefonoA,
        Correo: correoA,
        NombreImagen:  rutaNombreIma 

    };
    $.ajax({
        url: "actualizaCentroOperacion",
        type: 'POST',
        cache: false,
        data: {
            centroOperaciones: datosCentroOperacionA,
            imagen: nombreRuta
        },
        dataType: 'json'
    }).done(function (res) {
        if (res.success) {

            if (filesA.length > 0) {



                var datos = new FormData(formularioA);
                fetch("imagenCentroOperacion", {
                    method: 'POST',
                    body: datos
                }).
                    then(response => response.text()).
                    then(respuesta => {
                        console.log("EditadoImagen");
                    });
            }

            $('#ModalActualizar').modal('hide')


            listarCentroOperacions();
            $('#tablaCentroOperacion').DataTable().destroy();
            cargarCentroOperacionesMenu();
            resetearCentroOperacion();
            if (idA == 0) {
                Swal.fire({
                    position: 'top',
                    icon: 'success',
                    title: 'Centro de Operación Registrado',
                    showConfirmButton: false,
                    timer: 1500
                })
            } else {
                Swal.fire({
                    position: 'top',
                    icon: 'success',
                    title: 'Centro de Operación Actualizado',
                    showConfirmButton: false,
                    timer: 1500
                })
            }
        }
        else {


        }
    }).fail(function (res) {

    });
});



function editarCentroOperacion(id) {


    fetch("Update?id=" + id).
        then(response => response.json()).
        then(respuesta => {
            let resultado = "";
            let resultado2 = "";
            let resultado3 = "";
            console.log(respuesta)
            if (respuesta.Codigo > 0) {

                document.getElementById("codigoA").value = respuesta.Codigo;
                document.getElementById("nombreCentroOperacionA").value = respuesta.Nombre;
                document.getElementById("descripcionCentroOperacionA").value = respuesta.Descripcion;
                document.getElementById("direccionCentroOperacionA").value = respuesta.Direccion;
                document.getElementById("telefonoCentroOperacionA").value = respuesta.Telefono;
                document.getElementById("correoCentroOperacionA").value = respuesta.Correo;
                document.getElementById("imagenCentroOperacionA").value = "";
                document.getElementById("nombreRutaA").value = respuesta.NombreImagen;
                resultado3 += `<div id="imgSeleccion" style=' width: 100%;'> <img id=""  style=' width: 100%; 'src="${respuesta.NombreImagen.slice(1)}" alt=""> </div>`;

                document.getElementById("foto").innerHTML = resultado3;
                //Combos
            }
        });
}


function eliminaCentroOperacion(id) {

    fetch("Delete?id=" + id).
        then(response => response.json()).
        then(data => {
            listarCentroOperacions();
            cargarCentroOperacionesMenu();

            $('#tablaCentroOperacion').DataTable().destroy();

            Swal.fire({
                position: 'top',
                icon: 'success',
                title: 'Centro de Operación Eliminado',
                showConfirmButton: false,
                timer: 1500
            });

        });
}






function resetearCentroOperacion() {
    document.getElementById("codigo").value = 0;
    document.getElementById("nombreCentroOperacion").value = "";
    document.getElementById("descripcionCentroOperacion").value = "";
    document.getElementById("imagenCentroOperacion").value = "";

}