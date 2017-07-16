using Runas_Explosivas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Runas_Explosivas.Controllers
{
    public class TestController : Controller
    {
        // GET: Test

        public string PruebaEF2()
        {
            using (RunasContext db = new Models.RunasContext())
            {
                Usuario RunasExplosivas = new Usuario()
                {
                    Mail = "RunasExplosivas@gmail.com",
                    Nombre = "Staff de Runas Explosivas",
                    Password = "RunEx",
                    IsAdmin = true
                };
                db.Usuarios.Add(RunasExplosivas);

                Usuario JuanmaAvila = new Usuario()
                {
                    Mail = "Juanma@gmail.com",
                    Nombre = "Juan Manuel Ávila",
                    Password = "RunEx",
                    IsAdmin = true
                };
                db.Usuarios.Add(JuanmaAvila);
                Usuario JoaquinOllo = new Usuario()
                {
                    Mail = "joaquinollo@gmail.com",
                    Nombre = "Joaquín Ollo",
                    Password = "RunEx",
                    IsAdmin = true
                };
                db.Usuarios.Add(JoaquinOllo);

                Usuario DuamnRuassol = new Usuario()
                {
                    Mail = "Duamn@gmail.com",
                    Nombre = "Duamn Figueroa Ruassol",
                    Password = "RunEx",
                    IsAdmin = true
                };
                db.Usuarios.Add(DuamnRuassol);

                Usuario MartinvanHoutte = new Usuario()
                {
                    Mail = "Martin@gmail.com",
                    Nombre = "Martín Van Houtte",
                    Password = "RunEx",
                    IsAdmin = true
                };
                db.Usuarios.Add(MartinvanHoutte);

                Usuario FrancoAllieri = new Usuario()
                {
                    Mail = "Franco@gmail.com",
                    Nombre = "Franco Allieri",
                    Password = "RunEx",
                    IsAdmin = true
                };
                db.Usuarios.Add(FrancoAllieri);

                Tag TagBlog = new Tag("blog", "pencil", true);
                Tag TagPodcast = new Tag("podcast", "headphones", true);
                Tag TagEditorial = new Tag("editorial", "book", true);
                Tag TagNoticia = new Tag("noticia", "info-sign", true);
                Tag TagResena = new Tag("reseña", "flag", true);
                Tag TagCuentacuentos = new Tag("cuentacuentos", "file", true);
                Tag TagRolerosofia = new Tag("rolerosofía", "eye-open", true);
                Tag TagRolerodromo = new Tag("roleródromo", "flash", true);
                Tag TagDandD = new Tag("d&d");
                Tag TagDisenho = new Tag("diseño");
                Tag TagNacional = new Tag("argentina");
                Tag TagLatAm = new Tag("latinoamérica");
                Tag TagEvento = new Tag("evento");
                Tag TagConcurso = new Tag("concurso");
                Tag TagTeoria = new Tag("teoría");
                Tag TagIndie = new Tag("indie");
                Tag TagCrowfund = new Tag("crowfunding");
                db.Tags.Add(TagBlog);
                db.Tags.Add(TagPodcast);
                db.Tags.Add(TagEditorial);
                db.Tags.Add(TagResena);
                db.Tags.Add(TagCuentacuentos);
                db.Tags.Add(TagRolerosofia);
                db.Tags.Add(TagRolerodromo);
                db.Tags.Add(TagDandD);
                db.Tags.Add(TagDisenho);
                db.Tags.Add(TagNacional);
                db.Tags.Add(TagLatAm);
                db.Tags.Add(TagEvento);
                db.Tags.Add(TagConcurso);
                db.Tags.Add(TagPodcast);
                db.Tags.Add(TagTeoria);
                db.Tags.Add(TagIndie);
                db.Tags.Add(TagCrowfund);

                Articulo nuevoArticulo = new Articulo(
                    "PE nº66: Aplicando Resolución de Conflictos a tu Mesa",
                    new List<Usuario>() { JuanmaAvila, DuamnRuassol, MartinvanHoutte, FrancoAllieri },
                    new DateTime(2017, 3, 30, 12, 00, 00),
                    "¡Otro Miércoles de #PodcastExplosivo! Hoy vamos a hablar sobre cómo aplicar lo que aprendimos de juegos con resoluciones de conflicto no binarias a otros juegos más tradicionales. Ideas new school para juegos con raíces en los 80'/90'.",
                    "https://www.youtube.com/watch?v=y-GIvQAsn6w",
                    "",
                    new List<Tag> { TagPodcast, TagRolerodromo }
                    );

                Articulo articulo2 = new Articulo(
                    "PE nº65 - Las Grandes Tres, parte 3: ¿Qué hacen personajes y jugadores?",
                    new List<Usuario>() { JuanmaAvila, DuamnRuassol, MartinvanHoutte, FrancoAllieri },
                    new DateTime(2017, 3, 22, 12, 00, 00),
                    "¡Primer (nuevo) Miércoles del #PodcastExplosivo! Hoy vamos a terminar un tema vital para cualquier tipo de diseño rolero: la tercera y última parte de las Grandes Tres preguntas que todo diseñador debería hacerse en algún momento antes de publicar.",
                    "https://www.youtube.com/watch?v=sBlWx6Z28Pw",
                    "",
                    new List<Tag> { TagPodcast, TagDisenho }
                );

                Articulo articulo3 = new Articulo(
                    "Reseña: Princes of the Apocalypse D&D",
                    new List<Usuario>() { RunasExplosivas },
                    new DateTime(2016, 8, 8, 12, 00, 00),
                    "seawefaefdfadfaefaefaefsdfasd",
                    "",
                    "",
                    new List<Tag> { TagBlog, TagResena, TagDandD }
                    );

                Articulo articulo4 = new Articulo(
                    "Murder Hobos & Fantasy Rockstars",
                    new List<Usuario>() { DuamnRuassol },
                    new DateTime(2016, 7, 20, 12, 00, 00),
                    "Ninguna party de aventureros es la Comunidad del Anillo. Momento, tal vez me estoy adelantando, volvamos unos pasos en mi tren de pensamiento. Dungeons and Dragons no es El Señor de los Anillos. Tampoco es Conan el Bárbaro, ni El Nombre del Viento. D&D, y la miríada de derivados que originó, son otra cosa: ni high, ni heroic ni low, ni dark fantasy, son fantasía dungeonera, son su propio género.",
                    "",
                    "",
                    new List<Tag> { TagBlog, TagRolerosofia, TagDandD }
                    );

                Articulo articulo5 = new Articulo(
                    "Información Asimétrica",
                    new List<Usuario>() { DuamnRuassol },
                    new DateTime(2016, 7, 1, 12, 00, 00),
                    "¡Hey! Hace mucho tiempo que no tiro unas líneas por acá, creo que ya es hora de que estire estos músculos rolerosóficos claramente oxidados. Originalmente quería publicar este artículo recién salido de la Back to the Dungeon que tuvimos a mediados de Marzo. Esta BttD me di el gusto de dirigir Miseries and Misfortunes, y la verdad es que la pasé bomba. Una de las cosas más entretenidas que saqué de la experiencia fue preparar la aventura y, como no quiero seguir descarrilando, voy a hablar de algo muy particular que me gusta hacer en mis mesas usando esta aventura como ejemplo.",
                    "",
                    "",
                    new List<Tag> { TagBlog, TagRolerosofia }
                    );

                Articulo articulo6 = new Articulo(
                    "Gaceta Rúnica: ¡Muchos juegos y mecenazgos nuevos!",
                    new List<Usuario>() { JuanmaAvila },
                    new DateTime(2016, 9, 5, 12, 00, 00),
                    "En la entrega de la Gaceta Rúnica de esta quincena vamos a intentar cubrir la avalancha de lanzamientos de juegos nuevos y proyectos de financiamiento que están sacudiendo el mundo de los juegos de rol.",
                    "",
                    "",
                    new List<Tag> { TagBlog, TagNoticia }
                    );

                Articulo articulo7 = new Articulo(
                    "Desierto y pantano",
                    new List<Usuario>() { JoaquinOllo },
                    new DateTime(2015, 10, 9, 12, 00, 00),
                    "El mundo está hecho de contrastes. El mundo que quedó, al menos. El desierto es tierra de tránsito, de seres silenciosos, monocromía,  y mercaderes. Los restos de las ciudades son zonas hundidas y pantanosas: neón, derroche, fiestas, pieles picadas y rostros consumidos.",
                    "",
                    "",
                    new List<Tag> { TagBlog, TagCuentacuentos }
                    );

                Articulo articulo8 = new Articulo(
                    "Mundos virtuales",
                    new List<Usuario>() { JoaquinOllo },
                    new DateTime(2015, 7, 5, 12, 00, 00),
                    "Buen día! Hoy nos toca inaugurar columna: \"Mazo de muchas cosas\" es una nueva columna quincenal, en la que nos relajamos un poco y nos dedicamos, esencialmente, a tirar links de cosas que nos parezcan interesantes. Lo importante acá es darte recursos para tus partidas de rol, ya sea música, imágenes, videos, o artículos: si te deja pensando y anotando ideas para jugar o diseñar, vamos bien.",
                    "",
                    "",
                    new List<Tag> { TagBlog, TagCuentacuentos }
                    );

                Articulo articulo9 = new Articulo(
                    "El ascenso del gólem",
                    new List<Usuario>() { JoaquinOllo },
                    new DateTime(2015, 4, 7, 12, 00, 00),
                    "Lo siguiente es una sesión de rol improvisada para un amigo que nunca había jugado, a través del chat. Veníamos hablando hace rato de esta actividad, y ya que no había posibilidad de jugar presencialmente, ensayé esto que leerán a continuación. Aprovechando que fue por chat, reproduzco la \"sesión\" como diálogo entre jugador y GM (obviamente adaptado). La voz del GM aparece con sangría, y en cursiva. Ah, peligro: nos pusimos algo poéticos o solemnes mientras jugábamos, no se lo tomen demasiado en serio jajaj, no nos estamos creyendo Góngora.",
                    "",
                    "",
                    new List<Tag> { TagBlog, TagCuentacuentos }
                    );

                List<Articulo> Articulos = new List<Articulo>();
                Articulos.Add(nuevoArticulo);
                Articulos.Add(articulo2);
                Articulos.Add(articulo3);
                Articulos.Add(articulo4);
                Articulos.Add(articulo5);
                Articulos.Add(articulo6);
                Articulos.Add(articulo7);
                Articulos.Add(articulo8);
                Articulos.Add(articulo9);

                foreach (Articulo articulo in Articulos)
                {
                    db.Articulos.Add(articulo);
                }
                db.SaveChanges();
            }
            return "listo";
        }

        public string PruebaEF3()
        {
            using (RunasContext db = new Models.RunasContext())
            {
                IQueryable<Articulo> Articulos = db.Articulos;
                IQueryable<Usuario> Usuarios = db.Usuarios;
                IQueryable<ComentarioEnArticulo> Comentarios = db.ComentariosEnArticulos;
                ComentarioEnArticulo NuevoComentario = new ComentarioEnArticulo()
                {
                    Autor = Usuarios.Single(au => au.Mail == "joaquinollo@gmail.com"),
                    Fecha = new DateTime(2015, 7, 5, 12, 00, 00),
                    Texto = "Creo que la nueva edición es un espanto, los odio a todos.",
                    ArticuloComentado = Articulos.Single(au => au.ID == 1),
                    RespuestaA = Comentarios.Single(c => c.ID == 1)
                };
                db.ComentariosEnArticulos.Add(NuevoComentario);
                db.SaveChanges();
                return "listo";
            }
        }

        public int PruebaEF4()
        {
            using (RunasContext db = new Models.RunasContext())
            {
                ComentarioEnArticulo ComentariosEnArticulo = db.ComentariosEnArticulos.Include("RespuestaA").Single(com => com.ID == 2);
                return ComentariosEnArticulo.BuscadorRespuestaARecursivo();
            }
        }
    }
}