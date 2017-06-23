var avisos = "";

/* FUNCIONES DE MANIPULACIÓN DE BARRA LATERAL */

function isBarraExpandida(){
	if ($("#barra-lateral").width() > 100) {
		return true;
	} else {
		return false;
	}
};

function manipularBarra(text, mostrarDefault=true){
	if (text == "expandir") {
		$("#barra-lateral").removeClass("col-xs-1").addClass("col-xs-7", "active");
		$("#boton-expandir-barra").removeClass("glyphicon-plus").addClass("glyphicon-minus");
		if (mostrarDefault) {$("#barra-lateral-widget-default").fadeIn()};
	} else if (text == "contraer") {
		$("#barra-lateral-contenido").children().fadeOut();
    	$("#barra-lateral").removeClass("col-xs-7", "active").addClass("col-xs-1");
    	$("#boton-expandir-barra").removeClass("glyphicon-minus").addClass("glyphicon-plus");
	};
};

function botonera(botonID){
    var target = $(botonID).data("target");
    var child = $(botonID).data("child");
	if ($(botonID).hasClass("active") == false) {

        $(botonID).addClass("active").siblings().removeClass("active");
		if (isBarraExpandida()) {
			$("#barra-lateral-contenido").children().fadeOut(200);
            $(target).delay(200).fadeIn(300);
		} else {
			manipularBarra("expandir", false);
			$(target).fadeIn(300);
        };
	} else {
		manipularBarra("contraer");		
		$("#barra-lateral-contenido").children().fadeOut(200);
        $(botonID).parent().children().removeClass("active");
    };
};

/**
 * / FUNCIÓN ENCARGADA DE ACTIVAR EL EVENT LISTENER PARA LOS BOTONES QUE INTERACTÚAN CON LA BARRA LATERAL
 * @param {any} boton PASAR ID DE BOTÓN CON # ANTECEDIENDO
 */

function activarBoton(boton) {
    $(boton).on("click", function () {
        botonera(boton);
        $(boton).removeClass("active");
    });
}

/* FUNCION IN DE PYTHON */

function isInArray(obj, list) {
    for (var i = 0; i < list.length; i++) {
        if (list[i] === obj) {
            return true;
        };
    };
    return false;
};

  /*getPreviewText (){
  	var textArray = this.texto.split(" ");
  	if (textArray.length > 45) {
  		
  	};
  };*/

/* CLASE Y CARGADO MANUAL DE TAGS */

var Tag = class Tag {
	constructor(nombre, glyphicon='', prioridad=false) {
		this.nombre = nombre;
		this.glyphicon = (glyphicon) ? "glyphicon glyphicon-" + glyphicon: glyphicon;
	    this.prioridad = prioridad;
	    this.glyphHTML = (glyphicon) ? '</a><span data-toggle="tooltip" title="' + this.nombre + '" class="tag-icon ' + this.glyphicon + '"></span>': "";
  	};
};

function allGlyphHTML (tagList) {
	var finalHTML = "";
	for (var i = 0; i < tagList.length; i++){
		finalHTML = finalHTML + tagList[i].glyphHTML;
	};
	return finalHTML;
};

var tagBlog = new Tag (
	"blog",
	"pencil",
	true
	);

var tagPodcast = new Tag (
	"podcast",
	"headphones",
	true
	);

var tagNoticia = new Tag (
	"noticia",
	"info-sign",
	true
	);

var tagResenha = new Tag (
	"reseña",
	"flag",
	true
	);

var tagCuentacuentos = new Tag (
	"cuentacuentos",
	"file",
	true
	);

var tagRolerosofia = new Tag (
	"rolerosofía",
	"eye-open",
	true
	);

var tagRolerodromo = new Tag (
	"roleródromo",
	"flash",
	true
	);

var tagMazoMuchasCosas = new Tag (
	"mazo de muchas cosas",
	"paperclip",
	true
	);

var listaTags = [tagBlog, tagPodcast, tagNoticia, tagResenha, tagCuentacuentos, tagRolerosofia, tagRolerodromo];

/* CLASE Y CARGA MANUAL DE Y ARTÍCULOS */

var Articulo = class Articulo {
  constructor(titulo, autor, tags, fecha, texto, imagen=false, link=false) {
    this.titulo = titulo;
    this.autor = autor;
    this.tags = tags; 
    this.fecha = fecha; /*REVISAR, SACAR DE PARÁMETRO DEL CONSTRUCTOR Y BUSCAR CÓMO OBTENER FECHA*/
    this.texto = texto;
    this.imagen = imagen;
    this.link = link;
  };
};

var articulo1 = new Articulo(
	"PE nº66: Aplicando Resolución de Conflictos a tu Mesa",
	"Juan Manuel Avila",
	[tagPodcast],
	"3/30/2017",
	"¡Otro Miércoles de #PodcastExplosivo! Hoy vamos a hablar sobre cómo aplicar lo que aprendimos de juegos con resoluciones de conflicto no binarias a otros juegos más tradicionales. Ideas new school para juegos con raíces en los 80'/90'.",
	);

var articulo2 = new Articulo(
	"PE nº65 - Las Grandes Tres, parte 3: ¿Qué hacen personajes y jugadores?",
	"Juan Manuel Avila",
	[tagPodcast],
	"3/22/2017",
	"¡Primer (nuevo) Miércoles del #PodcastExplosivo! Hoy vamos a terminar un tema vital para cualquier tipo de diseño rolero: la tercera y última parte de las Grandes Tres preguntas que todo diseñador debería hacerse en algún momento antes de publicar.",
	);

var articulo3 = new Articulo(
	"Reseña: Princes of the Apocalypse D&D",
	"Ezequiel",
	[tagBlog, tagResenha],
	"8/8/2016",
	"seawefaefdfadfaefaefaefsdfasd",
	);

var articulo4 = new Articulo(
	"Murder Hobos & Fantasy Rockstars",
	"Duamn Figueroa",
	[tagBlog, tagRolerosofia],
	"7/20/2016",
	"Ninguna party de aventureros es la Comunidad del Anillo. Momento, tal vez me estoy adelantando, volvamos unos pasos en mi tren de pensamiento. Dungeons and Dragons no es El Señor de los Anillos. Tampoco es Conan el Bárbaro, ni El Nombre del Viento. D&D, y la miríada de derivados que originó, son otra cosa: ni high, ni heroic ni low, ni dark fantasy, son fantasía dungeonera, son su propio género.", 
	);

var articulo5 = new Articulo(
	"Información Asimétrica",
	"Duamn Figueroa",
	[tagBlog, tagRolerodromo],
	"7/1/2016",
	"¡Hey! Hace mucho tiempo que no tiro unas líneas por acá, creo que ya es hora de que estire estos músculos rolerosóficos claramente oxidados. Originalmente quería publicar este artículo recién salido de la Back to the Dungeon que tuvimos a mediados de Marzo. Esta BttD me di el gusto de dirigir Miseries and Misfortunes, y la verdad es que la pasé bomba. Una de las cosas más entretenidas que saqué de la experiencia fue preparar la aventura y, como no quiero seguir descarrilando, voy a hablar de algo muy particular que me gusta hacer en mis mesas usando esta aventura como ejemplo.",
	);

var articulo6 = new Articulo(
	"Gaceta Rúnica: ¡Muchos juegos y mecenazgos nuevos!",
	"Juan Manuel Avila",
	[tagBlog, tagNoticia],
	"5/9/2016",
	"En la entrega de la Gaceta Rúnica de esta quincena vamos a intentar cubrir la avalancha de lanzamientos de juegos nuevos y proyectos de financiamiento que están sacudiendo el mundo de los juegos de rol.",
	);

var articulo7 = new Articulo(
	"Desierto y pantano",
	"Joaquín Ollo",
	[tagBlog, tagCuentacuentos],
	"9/10/2015",
	"EL mundo está hecho de contrastes. El mundo que quedó, al menos. El desierto es tierra de tránsito, de seres silenciosos, monocromía,  y mercaderes. Los restos de las ciudades son zonas hundidas y pantanosas: neón, derroche, fiestas, pieles picadas y rostros consumidos.",	
	);

var articulo8 = new Articulo(
	"Mundos virtuales",
	"Joaquín Ollo",
	[tagBlog, tagMazoMuchasCosas],
	"5/7/2015",
	'Buen día! Hoy nos toca inaugurar columna: "Mazo de muchas cosas" es una nueva columna quincenal, en la que nos relajamos un poco y nos dedicamos, esencialmente, a tirar links de cosas que nos parezcan interesantes. Lo importante acá es darte recursos para tus partidas de rol, ya sea música, imágenes, videos, o artículos: si te deja pensando y anotando ideas para jugar o diseñar, vamos bien.',
	);

var articulo9 = new Articulo(
	"El ascenso del gólem",
	"Joaquín Ollo",
	[tagBlog, tagCuentacuentos],
	"7/4/2015",
	'Lo siguiente es una sesión de rol improvisada para un amigo que nunca había jugado, a través del chat. Veníamos hablando hace rato de esta actividad, y ya que no había posibilidad de jugar presencialmente, ensayé esto que leerán a continuación. Aprovechando que fue por chat, reproduzco la "sesión" como diálogo entre jugador y GM (obviamente adaptado). La voz del GM aparece con sangría, y en cursiva. Ah, peligro: nos pusimos algo poéticos o solemnes mientras jugábamos, no se lo tomen demasiado en serio jajaj, no nos estamos creyendo Góngora.',	
	);

var articulos = [articulo1, articulo2, articulo3, articulo4, articulo5, articulo6, articulo7, articulo8, articulo9];

/* DOCUMENT READY PROGRAMAR DEBAJO */

$(document).ready(function(){
    
/* BUCLE PARA AGREGAR ARTICULOS AL DOM */

    for (var i = 0; i < articulos.length; i++) {
    	if (i === 0) {
    		$("#destacado").prepend(
    			'<h3><a href="#">'+ articulos[i].titulo + ' </a>' + allGlyphHTML(articulos[i].tags) +'</h3>' +
					'<div class="col-md-10">' +
						'<p>' + articulos[i].texto + '</p>' +
					'</div>')};
		$("#display-articulos-secundarios").append('<div class="col-md-6 articulo">' +
			'<h4><a href="#">' + articulos[i].titulo + ' </a>' + allGlyphHTML(articulos[i].tags) + '</h4>' +
			'<p class="hidden-xs">'+ articulos[i].texto +'</p>'
			);
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

    };

/* LLAMADO A FUNCIÓN DE EXPANDIR BARRA AL PRESIONAR BOTON + O - */

    $("#boton-expandir-barra").click(function(){
    	if (!isBarraExpandida()) {
    		manipularBarra("expandir");
    	} else {
			manipularBarra("contraer");
    	};
    });

/* EVENT LISTENER PARA CLICK DE LOS BOTONES INFERIORES */

	/*$("#boton-compras").on("click", function(){
    	botonera("#boton-compras");
    });

    $("#boton-login").on("click", function(){
    	botonera("#boton-login");
    });

 	$("#boton-busqueda").on("click", function(){
    	botonera("#boton-busqueda");
    });

    $("#boton-registro").on("click", function () {
        botonera("#boton-registro");
    });*/

    $(".boton-lateral").on("click", function () {
        activarBoton($(this));
    });

    

/* ACTIVACIÓN DE TOOLTIP DE BOOTSTRAP */ 

    $(function () {
	  $('[data-toggle="tooltip"]').tooltip()
	});
});