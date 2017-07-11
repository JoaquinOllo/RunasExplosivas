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

$(document).ready(function(){
    
/* BUCLE PARA AGREGAR ARTICULOS AL DOM */

   // for (var i = 0; i < articulos.length; i++) {
   // 	if (i === 0) {
   //         $("#destacado").prepend(
   //             '<h3><a href="#">' + articulos[i].titulo + ' </a>' + allGlyphHTML(articulos[i].tags) + '</h3>' +
   //             '<div class="col-md-10">' +
   //             '<p>' + articulos[i].getPreviewText(300) + '</p>' +
   //             '</div>').data("id", articulos[i].ID);
   //     } else {
   //         $("#display-articulos-secundarios").append(
   //             '<div class="col-md-6 articulo" data-id="' + articulos[i].ID + '">' +
			//    '<h4><a href="#">' + articulos[i].titulo + ' </a>' + allGlyphHTML(articulos[i].tags) + '</h4>' +
			//    '<p class="hidden-xs">'+ articulos[i].getPreviewText(200) +'</p>'
			//);
   //     }
    	/*if (isInArray("podcast", articulos[i].tags)) {
    		$("#destacado").append(
					'<div class="col-xs-2">' +
						'<div class="btn-group">' + 
						  '<button type="button" class="btn btn-lg btn-info dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
						    '<span class="glyphicon glyphicon-headphones"></span> <span class="caret"></span>' +
						  '</button>' +
						  '<ul class="dropdown-menu">' +
						    '<li><a href="'+ articulos[i].link +'">Youtube</a></li>' +
						    '<li><a href="#">Descargá el podcast!</a></li>' +
						    '<li><a href="#">Escuchalo después!</a></li>' +
						  '</ul>' +
						'</div>' +
					'</div>	')
    	};*/

    ////}

/* EVENT LISTENER PARA BOTONES DE LOS ARTÍCULOS AL HACER CLIC */

    //$("#display-articulos .articulo a").on("click", function () {
    //    $("#display-articulos-secundarios").fadeOut();
    //    $("#destacado").fadeOut(100);
    //    var thisArticuloID = $(this).parents(".articulo").data("id");
    //    var thisArticulo = buscarArticuloPorId(thisArticuloID, articulos);
    //    $("#display-articulos").append(

    //    );
    //});

/* LLAMADO A FUNCIÓN DE EXPANDIR BARRA AL PRESIONAR BOTON + O - */

    $("#boton-expandir-barra").click(function(){
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
    });

    $(window).on("scroll", function () {
        var posicionDeTienda = $("#display-tienda").offset().top - 15;
        if ((window.pageYOffset > 350) && $("#boton-scrolldown").css("opacity") !== "0.1" && !$("#boton-scrolldown").is(":animated")) {
            $("#boton-scrolldown").stop().animate({ opacity: 0.1 }, 500);
        } else if (window.pageYOffset === 0 && $("#boton-scrolldown").css("opacity") !== "1") {
            $("#boton-scrolldown").stop().animate({ opacity: 1 }, 100);
        }
    });


/* EVENT LISTENER PARA CLICK DE LOS BOTONES INFERIORES */

	$("#boton-compras").on("click", function(){
    	botonera("#boton-compras");
    });

    $("#boton-login").on("click", function(){
    	botonera("#boton-login");
    });

 	$("#boton-busqueda").on("click", function(){
    	botonera("#boton-busqueda");
    });

/* ACTIVACIÓN DE TOOLTIP DE BOOTSTRAP */ 

    $(function () {
        $('[data-toggle="tooltip"]').tooltip();
	});
});