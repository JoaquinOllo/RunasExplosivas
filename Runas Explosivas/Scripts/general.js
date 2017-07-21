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

/* FUNCION IN DE PYTHON */

/**
 * / Función In de Python, para rastrear en un array un objeto
 * @param {any} obj Objeto a buscar en el array
 * @param {Array} list Array en el que se busca el objeto
 * @returns {boolean} true si el objeto está en la lista, false si no es así
 */
function isInArray(obj, list) {
    for (var i = 0; i < list.length; i++) {
        if (list[i] === obj) {
            return true;
        }
    }
    return false;
}

/* CLASE Y CARGA MANUAL DE Y ARTÍCULOS DE TIENDA */

/**
 * / Clase usada por todos los artículos de la tienda
 * @param {Number} ID Identificador único numérico (int)
 * @param {String} titulo Título del artículo
 * @param {String} autor Autor del artículo. Vacío si el texto no tiene autor
 * @param {Tag} tags Lista de objetos de clase Tag
 * @param {string} tipoProducto Digital, Hardcover, Softcover, etc.
 * @param {Date} fecha Fecha de publicación
 * @param {string} texto Texto descriptivo
 * @param {string} imagen Imagen descriptiva del producto
 * @param {Float32Array} precio En caso de precio 0, producto de descarga gratuita
 * @param {Number} stock Cantidad de productos disponibles. No se usa si el producto es digital
 */
var ArticuloTienda = class ArticuloTienda {
    constructor(ID, titulo, autor="", tags, tipoProducto, fecha, texto, imagen, precio, stock) {
        this.ID = ID;
        this.titulo = titulo;
        this.autor = autor;
        this.tags = tags;
        this.tipoProducto = tipoProducto;
        this.fecha = fecha; /*REVISAR, SACAR DE PARÁMETRO DEL CONSTRUCTOR Y BUSCAR CÓMO OBTENER FECHA*/
        this.texto = texto;
        this.imagen = imagen;
        this.precio = precio;
        this.stock = stock;
    }
}

var producto1 = new ArticuloTienda();
var producto2 = new ArticuloTienda();
var producto3 = new ArticuloTienda();
var producto4 = new ArticuloTienda();
var producto5 = new ArticuloTienda();
var producto6 = new ArticuloTienda();
var producto7 = new ArticuloTienda();
var producto8 = new ArticuloTienda();
var producto9 = new ArticuloTienda();

var articulos = [producto1, producto2, producto3, producto4, producto5, producto6, producto7, producto8, producto9];


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

    $(".btn-agregar-carrito").on("click", function () {

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
});