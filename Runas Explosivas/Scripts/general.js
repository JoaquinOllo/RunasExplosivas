/* FUNCIONES DE MANIPULACIÓN DE BARRA LATERAL */


/**
 * / Función de retorno booleano para revisar si la barra lateral está o no expandida. Sin parámetros.
 */
function isBarraExpandida(){
	if ($("#barra-lateral").width() > 100) {
        return true;
	} else {
        return false;
	}
}

/* Método que extiende Jquery para comprobar si un selector no devuelve nada */
$.fn.exists = function () {
    return this.length !== 0;
}

/**
 * / Función que maneja expansión y contracción de barra lateral, mostrando contenido de manera optativa, y eliminándolo al contraerla
 * @param {any} text Únicos parámetros reconocidos son "expandir" o "contraer".
 * @param {any} mostrarDefault Parámetro booleano para indicar si se muestra o no el contenido default de la barra lateral.
 */
function manipularBarra(text, mostrarDefault=true){
	$("#barra-lateral").toggleClass("col-xs-1").toggleClass("col-xs-7", "active");
    $("#boton-expandir-barra").toggleClass("glyphicon-plus").toggleClass("glyphicon-minus");
    if (text === "expandir" && mostrarDefault) {
        var MainSection = $(".boton-lateral[main-section]").data("target");
        $(MainSection).fadeIn();
        $(".boton-lateral[main-section]").toggleClass("active");
	} else if (text === "contraer") {
        $("#barra-lateral-contenido").children().fadeOut();
        $(".boton-lateral").removeClass("active");
	}
}

//function manipularBarra(text, mostrarDefault = true) {
//    if (text === "expandir") {
//        $("#barra-lateral").removeClass("col-xs-1").addClass("col-xs-7", "active");
//        $("#boton-expandir-barra").removeClass("glyphicon-plus").addClass("glyphicon-minus");
//        if (mostrarDefault) { $("#barra-lateral-widget-default").fadeIn(); }
//    } else if (text === "contraer") {
//        $("#barra-lateral-contenido").children().fadeOut();
//        $("#barra-lateral").removeClass("col-xs-7", "active").addClass("col-xs-1");
//        $("#boton-expandir-barra").removeClass("glyphicon-minus").addClass("glyphicon-plus");
//    }
//} VERSIÓN VIEJA DE LA FUNCIÓN, CONSERVAR POR SI ACASO


/**
 * /Función que permite a los botones de la barra lateral mostrar las distintas secciones, u ocultarlas.
 * @param {any} botonID String que debe ser el ID del botón clickeado, antecedido por '#'
 */
function botonera(botonID){
    var target = $(botonID).data("target");
    var child = $(botonID).data("child");

	if ($(botonID).hasClass("active") === false) {
        $(botonID).addClass("active").siblings().removeClass("active");
		if (isBarraExpandida()) {
			$("#barra-lateral-contenido").children().fadeOut(200);
            $(target).delay(200).fadeIn(300);
		} else {
			manipularBarra("expandir", false);
			$(target).fadeIn(300);
        }
	} else {
		manipularBarra("contraer");		
		$("#barra-lateral-contenido").children().fadeOut(200);
        $(botonID).parent().children().removeClass("active");
    }
}

/**
 * /Función en suspenso que activa el event listener de los botones
 * @param {String} boton string que es el ID del botón, con '#' antecediendo
 */
function activarBoton(boton) {
    $(boton).on("click", function () {
        botonera(boton);
    });
}

function toggleRespuestaA(boton) {
    var ComentariosAResaltar = $(boton).closest(".comentario");
    $(boton).find("span").toggleClass("glyphicon-eye-close").toggleClass("glyphicon-eye-open").parent().toggleClass("locked");
    var SiguienteComentario = $(boton).data("rtaid");
    while (true) {
        var Comentario = $("#lista-comentarios").find('[data-id="' + SiguienteComentario + '"]');
        SiguienteComentario = Comentario.find(".boton-derivacion").data("rtaid");
        ComentariosAResaltar = $(ComentariosAResaltar).add(Comentario);
        if (SiguienteComentario == null) {
            break;
        };
    };
    ComentariosAResaltar.toggleClass("comentario-resaltado");
};

/* DOCUMENT READY PROGRAMAR DEBAJO */

$(document).ready(function () {

    if (isBarraExpandida()) {
        $(".boton-lateral[main-section]").addClass("active");
    }

    /* LLAMADO A FUNCIÓN DE EXPANDIR BARRA AL PRESIONAR BOTON + O - */

    $("#boton-expandir-barra").click(function () {
        if (!isBarraExpandida()) {
            manipularBarra("expandir");
        } else {
            manipularBarra("contraer");
        }
    });

    /* EVENT LISTENER PARA CLICK DE LOS BOTONES INFERIORES */

    $("#boton-compras").on("click", function () {
        botonera("#boton-compras");
    });

    $("#boton-usuario").on("click", function () {
        botonera("#boton-usuario");
    });

    $("#boton-busqueda").on("click", function () {
        botonera("#boton-busqueda");
    });

    /* BOTON PARA MOSTRAR U OCULTAR COMENTARIOS */

    $("#boton-comentarios").on("click", function () {
        $("#lista-comentarios").slideToggle("fast");
    });

    /* BOTÓN PARA MOSTRAR A QUIÉN ESTÁ RESPONDIENDO CADA ARTÍCULO */

    $(".boton-derivacion").on("click", function (event) {
        if (!$(this).hasClass("locked")) {
            var DerivacionActiva = $(".boton-derivacion.locked");
            toggleRespuestaA(DerivacionActiva);
        };
        toggleRespuestaA(this);
        event.preventDefault();
    });

/* BOTON PARA AGREGAR COMENTARIOS A ARTÍCULOS */

    $("#lista-comentarios").on("click", ".boton-agregar-comentario", function (event) {
        event.preventDefault();
        if ($(this).hasClass("locked")) {
            $("textarea[name='textoComentario']").fadeOut().delay(500).remove();
            $(".boton-agregar-comentario[data-id='-1']").attr("type", "button").text("Comentar artículo");
            $(".boton-agregar-comentario.locked").removeClass("locked").closest(".comentario")
                .removeClass("comentario-seleccionado");
        } else {
            if ($(this).attr("type") != "submit" && $("textarea[name='textoComentario']").length == 0) {
                $(".boton-agregar-comentario").last().before(
                    '<textarea class="form-control" name="textoComentario" placeholder="Dejanos tu comentario" rows="5" cols="6"></textarea>')
                    .attr("type", "submit").text("Confirmar comentario");
            };
            if ($(this).is("a")) {
                $(".boton-agregar-comentario.locked").removeClass("locked").closest(".comentario")
                    .removeClass("comentario-seleccionado");
                $(this).addClass("locked").closest(".comentario").addClass("comentario-seleccionado");
                var respuestaAID = $(this).closest("li").data("id");
                $("input[name='respuestaA']").val(respuestaAID);
            };
        };

    });

/* ACTIVACIÓN DE TOOLTIP DE BOOTSTRAP */ 

    $(function () {
        $('[data-toggle="tooltip"]').tooltip();
	});

    // ENVÍO DE FORMULARIO DE BÚSQUEDA DE AJAX

    $('#form-busqueda').submit(function () {
        var UrlAjax = $("#boton-compras").exists() ? '/Editorial/BuscarProducto'  : '/Home/BuscarArticulo';
        $.ajax({
            url: UrlAjax,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: {
                ValorABuscar: $("#form-busqueda [name=ValorABuscar]").val(),
                TipoDeBusqueda: $("#form-busqueda [name=TipoDeBusqueda]:checked").val()
            },
            success: function (result) {
                $("#busqueda-realizada ul").add("#busqueda-realizada p").empty();
                if (result.length > 0) {
                    for (var i = 0; i < result.length; i++){
                        $("#busqueda-realizada ul").append(
                            '<li><a href="/Home/Articulo?blogPostId=' + result[i]["ID"] + '">' +
                            result[i]["Titulo"] + 
                            '</a></li>'
                        );
                    };
                } else {
                    $("#busqueda-realizada h4").after(
                        "<p>Ningún artículo coincide con los términos de la búsqueda. ¿Querés probar con otra palabra clave?</p>"
                    );
                };
                $("#busqueda").fadeOut("fast", function () {
                    $("#busqueda-realizada").fadeIn();
                });
            }
        });
        return false;
    });

    // BOTÓN DE NUEVA BÚSQUEDA 

    $("#busqueda-realizada").on("click", "button", function () {
        $("#busqueda-realizada").fadeOut("fast", function () {
            $("#busqueda").fadeIn().find("input[name='ValorABuscar']").val("");
        });
    });

});    