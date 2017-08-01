$(document).ready(function () {

    /* FUNCIONES DE PÁGINA DE ADMINISTRACIÓN */

    // DISPLAY DE SECCIONES DEL NAVBAR

    $(".boton-display").click(function () {
        if (!$(this).hasClass("active")) {
            var display = $(this).data("display");
            $(".main").children().slideUp("fast", function () {
                $(display).slideDown(300);
            });
            $(this).toggleClass("active");
        } else {
            $(".main").children().slideUp("fast");
            $(this).toggleClass("active");
        }
    });

    // AGREGADO Y RESETEO DINÁMICO DE TAGS Y AUTORES A LOS FORMULARIOS DE ADMINPAGE

    var AutorPorDefecto = $(".inputAutores").val();

    $(".boton-agregar-contenido").click(function () {
        var formularioIndividual = $(this).prev();
        var formularioGlobal = $(formularioIndividual).parentsUntil("form", ".form-group").prev().children("input");
        var separador = $(formularioGlobal).val() == "" ? "" : " ";
        if ($(formularioIndividual).val() != "") {
            $(formularioGlobal).val($(formularioGlobal).val() + separador + $(formularioIndividual).val().toLowerCase());
            $(formularioIndividual).val("");
        }
    });

    // DESACTIVACIÓN DE ENVÍO DE FORMULARIO AL PRESIONAR ENTER

    $(':input:not(textarea)').keypress(function (event) {
        return event.keyCode != 13;
    });

    $(".boton-reset-contenido").click(function () {
        var formulario = $(this).prev(".form-group").prev(".form-group").children("input");
        $(formulario).val("");
    });

    $(".boton-reset-autores").click(function () {
        var formulario = $(this).parent(".btn-group").prev(".form-group").prev(".form-group").children("input");
        if ($(this).data("autor") != "default") {
            $(formulario).val($(this).data("autor"));
        } else {
            $(formulario).val(AutorPorDefecto);
        }
    });

    // ENVÍO DE FORMULARIO DE BÚSQUEDA POR AJAX

    $('.busqueda-articulos-admin').submit(function () {
        var formBusqueda = $(this);
        $.ajax({
            url: '/Home/BuscarArticulo',
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: {
                ValorABuscar: $("[name=ValorABuscar]", this).val(),
                TipoDeBusqueda: $("[name=TipoDeBusqueda]:checked", this).val()
            },
            success: function (result) {
                if (result != null) {
                    for (var i = 0; i < result.length; i++) {
                        $(formBusqueda).next().children(".form-group").append(
                            '<div class="radio"><input type="radio" name="blogpostID"'
                            + 'value="' + result[i]["ID"] + '">' + '<a href="/Home/Articulo?blogPostId=' + result[i]["ID"] + '" target="_blank">'
                            + result[i]["Titulo"] + '</a></div >'
                        );
                    };
                    $(formBusqueda).fadeOut(100).next().fadeIn(300);
                } else { };
            }
        });
        return false;
    });

    // SELECCIÓN DE ARTÍCULO PARA EDITAR

    $("#form-seleccion-art-editar").submit(function () {
        $.ajax({
            url: '/Admin/ArticuloIndividual',
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: {
                artID: $("[name=blogpostID]:checked", this).val()
            },
            success: function (result) {
                if (result != null) {
                    $("#edicion-articulos [name=articuloAModificarID]").val(result["ID"]);        
                    $("#edicion-articulos [name=inputTitulo]").val(result["Titulo"]);
                    $("#edicion-articulos [name=inputLink]").val(result["Link"]);
                    $("#edicion-articulos [name=inputTexto]").val(result["Texto"]);
                    $("#edicion-articulos [name=inputImagen]").val(result["Imagen"]);
                    $("#edicion-articulos [name=inputAutores]").val(result["Autores"]);
                    $("#edicion-articulos [name=inputTags]").val(result["Tags"]);
                };
                $("#form-seleccion-art-editar").fadeOut(100).next().fadeIn(300);
            }
        });
        return false;
    });
});