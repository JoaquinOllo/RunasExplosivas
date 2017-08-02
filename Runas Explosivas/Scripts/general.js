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

/**
 * / Función que maneja expansión y contracción de barra lateral, mostrando contenido de manera optativa, y eliminándolo al contraerla
 * @param {any} text Únicos parámetros reconocidos son "expandir" o "contraer".
 * @param {any} mostrarDefault Parámetro booleano para indicar si se muestra o no el contenido default de la barra lateral.
 */
function manipularBarra(text, mostrarDefault=true){
	$("#barra-lateral").toggleClass("col-xs-1").toggleClass("col-xs-7", "active");
    $("#boton-expandir-barra").toggleClass("glyphicon-plus").toggleClass("glyphicon-minus");
    if (text === "expandir" && mostrarDefault) {
        $("#barra-lateral-widget-default").fadeIn();
	} else if (text === "contraer") {
        $("#barra-lateral-contenido").children().fadeOut();
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

/**
 * Función que devuelve posición del objeto respecto del ViewPort
 * @param {any} $e 
 */
function getViewportOffset($e) {
    var $window = $(window),
        scrollLeft = $window.scrollLeft(),
        scrollTop = $window.scrollTop(),
        offset = $e.offset()
        //rect1 = { x1: scrollLeft, y1: scrollTop, x2: scrollLeft + $window.width(), y2: scrollTop + $window.height() },
        //rect2 = { x1: offset.left, y1: offset.top, x2: offset.left + $e.width(), y2: offset.top + $e.height() };
    return {
        left: offset.left - scrollLeft,
        top: offset.top - scrollTop
        //insideViewport: rect1.x1 < rect2.x2 && rect1.x2 > rect2.x1 && rect1.y1 < rect2.y2 && rect1.y2 > rect2.y1
    };
};

/* DOCUMENT READY PROGRAMAR DEBAJO */

$(document).ready(function () {

    /* LLAMADO A FUNCIÓN DE EXPANDIR BARRA AL PRESIONAR BOTON + O - */

    $("#boton-expandir-barra").click(function () {
        if (!isBarraExpandida()) {
            manipularBarra("expandir");
        } else {
            manipularBarra("contraer");
        }
    });

    /* LLAMADO A EVENT LISTENER PARA SCROLLDOWN DE BOTON-SCROLLDOWN DE EDITORIAL */


    $("#boton-scrolldown").on("click", function () {
        var posicionDeTienda = $("#display-tienda").offset().top;
        $("html, body").animate({ scrollTop: (posicionDeTienda - 15) }, 450);
        $("#boton-scrolldown").fadeOut("slow");
    });

    $(window).on("scroll", function () {
        if ($(window).width > 768) {
            var posicionDeTienda = $("#display-tienda").offset().top - 15;
            if (window.pageYOffset === 0 && $("#boton-scrolldown").css("display") === "none") {
                $("#boton-scrolldown").fadeIn("slow");
            }
        }
    });

    /* FUNCIÓN PARA CAMBIO DE OPACIDAD EN PRODUCTOS DE EDITORIAL AL HACER HOVER */

    $(".producto").hover(function () {
        $(this).children(".descripcion-producto").toggleClass("fondo-gris").fadeTo(200, 0.95);
        $(this).children(".btn-agregar-carrito").toggleClass("opaco");
        $(this).find(".categorias-producto").fadeTo(300, 0.95);
    }, function () {
        $(this).children(".descripcion-producto").toggleClass("fondo-gris").fadeTo(200, 0.3);
        $(this).children(".btn-agregar-carrito").toggleClass("opaco");
        $(this).find(".categorias-producto").fadeTo(400, 0.3);
        });

    /* EVENT LISTENER PARA AGREGAR PRODUCTOS AL CARRITO */

    $("#signo-atencion-carrito").css({ "top": getViewportOffset($("#boton-compras"))["top"]-45, "left": getViewportOffset($("#boton-compras"))["left"]-5 });


    $(".btn-agregar-carrito").click(function () {
        $("#signo-atencion-carrito").show();
        for (var i = 0; i < 4; i++) {
            if (i == 3)
            {
                $("#signo-atencion-carrito").fadeOut();
            }
            $("#signo-atencion-carrito").animate({ "top": "-=3px" }, 300).animate({ "top": "+=3px" }, 300);
        }
    });



/* EVENT LISTENER PARA CLICK DE LOS BOTONES INFERIORES */

	$("#boton-compras").on("click", function(){
    	botonera("#boton-compras");
    });

    $("#boton-usuario").on("click", function(){
    	botonera("#boton-usuario");
    });

 	$("#boton-busqueda").on("click", function(){
    	botonera("#boton-busqueda");
    });

/* BOTON PARA MOSTRAR U OCULTAR COMENTARIOS */

      
      $("#boton-comentarios").tooltip("show").on("click", function () {
          $("#lista-comentarios").slideToggle("fast");
      });

/* ACTIVACIÓN DE TOOLTIP DE BOOTSTRAP */ 

    $(function () {
        $('[data-toggle="tooltip"]').tooltip();
	});

    // ENVÍO DE FORMULARIO DE BÚSQUEDA DE AJAX

    $('#form-busqueda').submit(function () {

        $.ajax({
            url: '/Home/BuscarArticulo',
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: {
                ValorABuscar: $("#form-busqueda [name=ValorABuscar]").val(),
                TipoDeBusqueda: $("#form-busqueda [name=TipoDeBusqueda]:checked").val()
            },
            success: function (result) {

                if (result != null) {
                    $("#busqueda-realizada").append(
                        "<ul>"
                    );
                    for (var i = 0; i < result.length; i++){
                        $("#busqueda-realizada").append(
                            '<li><a href="/Home/Articulo?blogPostId=' + result[i]["ID"] + '">' +
                            result[i]["Titulo"] + 
                            '</a></li>'
                        );
                    };
                    $("#busqueda-realizada").append(
                        "</ul>"
                    ).fadeIn();
                    $("#busqueda").fadeOut();
                } else { };
            }
        });
        return false;
    });

});    