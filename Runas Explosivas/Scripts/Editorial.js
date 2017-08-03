/**
 * Función que devuelve posición del objeto respecto del ViewPort
 * @param {any} $e 
 */
function getViewportOffset($e) {
    var $window = $(window),
        scrollLeft = $window.scrollLeft(),
        scrollTop = $window.scrollTop(),
        offset = $e.offset()
    return {
        left: offset.left - scrollLeft,
        top: offset.top - scrollTop
    };
};

function vaciarYCompletarCarrito(result) {
    $("#lista-carrito").empty();
    for (var i = 0; i < result.length; i++) {
        $("#lista-carrito").append(
            '<li data-product="' + result[i]["ID"] + '">' + result[i]["Titulo"].substring(0, 8) + '...'
            + ' <b class="light-gray">x' + result[i]["Cantidad"] + ': $' + result[i]["Precio"] + '</b>'
            + '<button class="btn btn-xs btn-default boton-quitar-de-carrito" type="button">'
            + '<span class="glyphicon glyphicon-minus"></span></button></li>'
        )
    }
    $("#boton-checkout").add("#boton-vaciar-carrito").show();
}

$(document).ready(function () {
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
        $(this).children(".descripcion-producto").toggleClass("fondo-gris").fadeTo(0, 0.95);
        $(this).children(".btn-agregar-carrito").toggleClass("opaco");
        $(this).find(".categorias-producto").fadeTo(300, 0.95);
    }, function () {
        $(this).children(".descripcion-producto").toggleClass("fondo-gris").fadeTo(0, 0.3);
        $(this).children(".btn-agregar-carrito").toggleClass("opaco");
        $(this).find(".categorias-producto").fadeTo(400, 0.3);
    });

    /* EVENT LISTENER PARA AGREGAR PRODUCTOS AL CARRITO */

    $("#signo-atencion-carrito").css({ "top": getViewportOffset($("#boton-compras"))["top"] - 45, "left": getViewportOffset($("#boton-compras"))["left"] + 5 });

    if ($("#lista-carrito").has("li[data-product]").length == 0) {
        $("#boton-checkout").add("#boton-vaciar-carrito").hide();
    }

    $(".btn-agregar-carrito").click(function () {
        var botonPresionado = $(this);
        $.ajax({
            url: '/Editorial/AgregarACarrito',
            type: "GET",
            dataType: "json",
            context: botonPresionado,
            contentType: "application/json; charset=utf-8",
            data: {
                prodID: $(botonPresionado).data("product")
            },
            success: vaciarYCompletarCarrito (result)
            //{
            //    $("#lista-carrito").empty();
            //    for (var i = 0; i < result.length; i++) {
            //        $("#lista-carrito").append(
            //            '<li data-product="' + result[i]["ID"] + '">' + result[i]["Titulo"].substring(0, 8) + '...'
            //            + ' <b class="light-gray">x' + result[i]["Cantidad"] + ': $' + result[i]["Precio"] + '</b>'
            //            + '<button class="btn btn-xs btn-default boton-quitar-de-carrito" type="button">'
            //            + '<span class="glyphicon glyphicon-minus"></span></button></li>'
            //        )
            //    }
            //    $("#boton-checkout").add("#boton-vaciar-carrito").show();
            //}
        });
        $("#signo-atencion-carrito").show();
        for (var i = 0; i < 4; i++) {
            if (i == 3) {
                $("#signo-atencion-carrito").fadeOut();
            }
            $("#signo-atencion-carrito").animate({ "top": "-=3px" }, 300).animate({ "top": "+=3px" }, 300);
        }
    });

    /* EVENT LISTENER PARA VACIAR CARRITO */

    $("#boton-vaciar-carrito").click(function () {
        $.ajax({
            url: '/Editorial/VaciarCarrito',
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            complete: function () {
                $("#lista-carrito").empty().append('<li>Todavía no agregaste nada al carrito</li>');
                $("#boton-checkout").add("#boton-vaciar-carrito").hide();
            }
        });
    });

    /* EVENT LISTENER PARA QUITAR PRODUCTOS DEL CARRITO */

    $("#lista-carrito").on("click", ".boton-quitar-de-carrito" ,function () {
        var botonPresionado = $(this);
        $.ajax({
            url: '/Editorial/EliminarDeCarrito',
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: {
                prodID: $(botonPresionado).parent().data("product")
            },
            success: function (result) { vaciarYCompletarCarrito(result) }
        });
    });

    $("#boton-checkout").click(function () {
        if (!$("#boton-cierre-sesion").exists()) {
            event.preventDefault();
            $(this).data("toggle", "tooltip").attr("data-original-title", "Debes estar conectado para confirmar la compra").tooltip("show");
            $("#signo-atencion-carrito").css({ "top": getViewportOffset($("#boton-usuario"))["top"] - 45, "left": getViewportOffset($("#boton-usuario"))["left"] + 5 });
            $("#signo-atencion-carrito").show();
            for (var i = 0; i < 4; i++) {
                if (i == 3) {
                    $("#signo-atencion-carrito").fadeOut();
                }
                $("#signo-atencion-carrito").animate({ "top": "-=3px" }, 300).animate({ "top": "+=3px" }, 300);
            }
        }
    });
});