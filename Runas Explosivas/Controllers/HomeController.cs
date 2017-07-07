using Runas_Explosivas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Runas_Explosivas.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /// DECLARACIÓN DE USUARIO PRUEBA1

            Usuario RunasExplosivas = new Usuario()
            {
                Mail = "RunasExplosivas@gmail.com",
                Nombre = "Staff de Runas Explosivas",
                Password = "RunEx"
            };

            Usuario JuanmaAvila = new Usuario()
            {
                Mail = "RunasExplosivas@gmail.com",
                Nombre = "Juan Manuel Ávila",
                Password = "RunEx"
            };
            Usuario JoaquinOllo = new Usuario()
            {
                Mail = "joaquinollo@gmail.com",
                Nombre = "Joaquín Ollo",
                Password = "RunEx"
            };
            Usuario DuamnRuassol = new Usuario()
            {
                Mail = "RunasExplosivas@gmail.com",
                Nombre = "Duamn Figueroa Ruassol",
                Password = "RunEx"
            };
            Usuario MartinvanHoutte = new Usuario()
            {
                Mail = "RunasExplosivas@gmail.com",
                Nombre = "Martín Van Houtte",
                Password = "RunEx"
            };

            /// DECLARACIÓN DE TAGS

            Tags TagBlog = new Tags(".blog", "pencil", true);

            Tags TagPodcast = new Tags(".podcast", "headphones", true);

            Tags TagEditorial = new Tags(".editorial", "book", true);

            Tags TagNoticia = new Tags(".noticia", "info-sign", true);

            Tags TagResena = new Tags(".reseña", "flag", true);

            Tags TagCuentacuentos = new Tags(".cuentacuentos", "file", true);

            Tags TagRolerosofia = new Tags(".rolerosofía", "eye-open", true);

            Tags TagRolerodromo = new Tags(".roleródromo", "flash", true);

            /// DECLARACIÓN DE ARTÍCULOS


            Articulo articulo1 = new Articulo(
                1,
                "PE nº66: Aplicando Resolución de Conflictos a tu Mesa", 
                RunasExplosivas, 
                new DateTime(2017, 3, 30, 12, 00, 00),
                "¡Otro Miércoles de #PodcastExplosivo! Hoy vamos a hablar sobre cómo aplicar lo que aprendimos de juegos con resoluciones de conflicto no binarias a otros juegos más tradicionales. Ideas new school para juegos con raíces en los 80'/90'.",
                "https://www.youtube.com/watch?v=y-GIvQAsn6w", 
                "",
                TagPodcast
                );

            Articulo articulo2 = new Articulo(
                2,
                "PE nº65 - Las Grandes Tres, parte 3: ¿Qué hacen personajes y jugadores?",
                RunasExplosivas,
                new DateTime(2017, 3, 22, 12, 00, 00),
                "¡Primer (nuevo) Miércoles del #PodcastExplosivo! Hoy vamos a terminar un tema vital para cualquier tipo de diseño rolero: la tercera y última parte de las Grandes Tres preguntas que todo diseñador debería hacerse en algún momento antes de publicar.",
                "https://www.youtube.com/watch?v=sBlWx6Z28Pw",
                "",
                TagPodcast
                );

            Articulo articulo3 = new Articulo(
                3,
                "Reseña: Princes of the Apocalypse D&D",
                RunasExplosivas,
                new DateTime(2016, 8, 8, 12, 00, 00),
                "seawefaefdfadfaefaefaefsdfasd",
                "",
                "",
                TagBlog
                );

            Articulo articulo4 = new Articulo(
                4,
                "Murder Hobos & Fantasy Rockstars",
                DuamnRuassol,
                new DateTime(2016, 7, 20, 12, 00, 00),
                "Ninguna party de aventureros es la Comunidad del Anillo. Momento, tal vez me estoy adelantando, volvamos unos pasos en mi tren de pensamiento. Dungeons and Dragons no es El Señor de los Anillos. Tampoco es Conan el Bárbaro, ni El Nombre del Viento. D&D, y la miríada de derivados que originó, son otra cosa: ni high, ni heroic ni low, ni dark fantasy, son fantasía dungeonera, son su propio género.",
                "",
                "",
                TagBlog
                );

            Articulo articulo5 = new Articulo(
                5,
                "Información Asimétrica",
                DuamnRuassol,
                new DateTime(2016, 7, 1, 12, 00, 00),
                "¡Hey! Hace mucho tiempo que no tiro unas líneas por acá, creo que ya es hora de que estire estos músculos rolerosóficos claramente oxidados. Originalmente quería publicar este artículo recién salido de la Back to the Dungeon que tuvimos a mediados de Marzo. Esta BttD me di el gusto de dirigir Miseries and Misfortunes, y la verdad es que la pasé bomba. Una de las cosas más entretenidas que saqué de la experiencia fue preparar la aventura y, como no quiero seguir descarrilando, voy a hablar de algo muy particular que me gusta hacer en mis mesas usando esta aventura como ejemplo.",
                "",
                "",
                TagBlog
                );

            Articulo articulo6 = new Articulo(
                6,
                "Gaceta Rúnica: ¡Muchos juegos y mecenazgos nuevos!",
                JuanmaAvila,
                new DateTime(2016, 9, 5, 12, 00, 00),
                "En la entrega de la Gaceta Rúnica de esta quincena vamos a intentar cubrir la avalancha de lanzamientos de juegos nuevos y proyectos de financiamiento que están sacudiendo el mundo de los juegos de rol.",
                "",
                "",
                TagBlog
                );

            Articulo articulo7 = new Articulo(
                7,
                "Desierto y pantano",
                JoaquinOllo,
                new DateTime(2015, 10, 9, 12, 00, 00),
                "EL mundo está hecho de contrastes. El mundo que quedó, al menos. El desierto es tierra de tránsito, de seres silenciosos, monocromía,  y mercaderes. Los restos de las ciudades son zonas hundidas y pantanosas: neón, derroche, fiestas, pieles picadas y rostros consumidos.",
                "",
                "",
                TagBlog
                );

            Articulo articulo8 = new Articulo(
                8,
                "Mundos virtuales",
                JoaquinOllo,
                new DateTime(2015, 7, 5, 12, 00, 00),
                "Buen día! Hoy nos toca inaugurar columna: \"Mazo de muchas cosas\" es una nueva columna quincenal, en la que nos relajamos un poco y nos dedicamos, esencialmente, a tirar links de cosas que nos parezcan interesantes. Lo importante acá es darte recursos para tus partidas de rol, ya sea música, imágenes, videos, o artículos: si te deja pensando y anotando ideas para jugar o diseñar, vamos bien.",
                "",
                "",
                TagBlog
                );

            Articulo articulo9 = new Articulo(
                9,
                "El ascenso del gólem",
                JoaquinOllo,
                new DateTime(2015, 4, 7, 12, 00, 00),
                "Lo siguiente es una sesión de rol improvisada para un amigo que nunca había jugado, a través del chat. Veníamos hablando hace rato de esta actividad, y ya que no había posibilidad de jugar presencialmente, ensayé esto que leerán a continuación. Aprovechando que fue por chat, reproduzco la \"sesión\" como diálogo entre jugador y GM (obviamente adaptado). La voz del GM aparece con sangría, y en cursiva. Ah, peligro: nos pusimos algo poéticos o solemnes mientras jugábamos, no se lo tomen demasiado en serio jajaj, no nos estamos creyendo Góngora.",
                "",
                "",
                TagBlog
                );

            List<Articulo> Articulos = new List<Articulo>();
            Articulos.Add(articulo1);
            Articulos.Add(articulo2);
            Articulos.Add(articulo3);
            Articulos.Add(articulo4);
            Articulos.Add(articulo5);
            Articulos.Add(articulo6);
            Articulos.Add(articulo7);
            Articulos.Add(articulo8);
            Articulos.Add(articulo9);

            ViewBag.Articulos = Articulos;

            // transferir artículos del JS a este método.
            // Carga temporal de artículos manual en método Index del controlador Home. Migrar a futuro a base de datos.
            return View();
        }

        public ActionResult Podcast()
        {
            Usuario RunasExplosivas = new Usuario()
            {
                Mail = "RunasExplosivas@gmail.com",
                Nombre = "Staff de Runas Explosivas",
                Password = "RunEx"
            };

            Usuario JuanmaAvila = new Usuario()
            {
                Mail = "RunasExplosivas@gmail.com",
                Nombre = "Juan Manuel Ávila",
                Password = "RunEx"
            };
            Usuario JoaquinOllo = new Usuario()
            {
                Mail = "joaquinollo@gmail.com",
                Nombre = "Joaquín Ollo",
                Password = "RunEx"
            };
            Usuario DuamnRuassol = new Usuario()
            {
                Mail = "RunasExplosivas@gmail.com",
                Nombre = "Duamn Figueroa Ruassol",
                Password = "RunEx"
            };
            Usuario MartinvanHoutte = new Usuario()
            {
                Mail = "RunasExplosivas@gmail.com",
                Nombre = "Martín Van Houtte",
                Password = "RunEx"
            };

            /// DECLARACIÓN DE TAGS

            Tags TagBlog = new Tags(".blog", "pencil", true);

            Tags TagPodcast = new Tags(".podcast", "headphones", true);

            Tags TagEditorial = new Tags(".editorial", "book", true);

            Tags TagNoticia = new Tags(".noticia", "info-sign", true);

            Tags TagResena = new Tags(".reseña", "flag", true);

            Tags TagCuentacuentos = new Tags(".cuentacuentos", "file", true);

            Tags TagRolerosofia = new Tags(".rolerosofía", "eye-open", true);

            Tags TagRolerodromo = new Tags(".roleródromo", "flash", true);

            /// DECLARACIÓN DE ARTÍCULOS


            Articulo articulo1 = new Articulo(
                1,
                "PE nº66: Aplicando Resolución de Conflictos a tu Mesa",
                RunasExplosivas,
                new DateTime(2017, 3, 30, 12, 00, 00),
                "¡Otro Miércoles de #PodcastExplosivo! Hoy vamos a hablar sobre cómo aplicar lo que aprendimos de juegos con resoluciones de conflicto no binarias a otros juegos más tradicionales. Ideas new school para juegos con raíces en los 80'/90'.",
                "https://www.youtube.com/watch?v=y-GIvQAsn6w",
                "",
                TagPodcast
                );

            Articulo articulo2 = new Articulo(
                2,
                "PE nº65 - Las Grandes Tres, parte 3: ¿Qué hacen personajes y jugadores?",
                RunasExplosivas,
                new DateTime(2017, 3, 22, 12, 00, 00),
                "¡Primer (nuevo) Miércoles del #PodcastExplosivo! Hoy vamos a terminar un tema vital para cualquier tipo de diseño rolero: la tercera y última parte de las Grandes Tres preguntas que todo diseñador debería hacerse en algún momento antes de publicar.",
                "https://www.youtube.com/watch?v=sBlWx6Z28Pw",
                "",
                TagPodcast
                );

            Articulo articulo3 = new Articulo(
                3,
                "Reseña: Princes of the Apocalypse D&D",
                RunasExplosivas,
                new DateTime(2016, 8, 8, 12, 00, 00),
                "seawefaefdfadfaefaefaefsdfasd",
                "",
                "",
                TagBlog
                );

            Articulo articulo4 = new Articulo(
                4,
                "Murder Hobos & Fantasy Rockstars",
                DuamnRuassol,
                new DateTime(2016, 7, 20, 12, 00, 00),
                "Ninguna party de aventureros es la Comunidad del Anillo. Momento, tal vez me estoy adelantando, volvamos unos pasos en mi tren de pensamiento. Dungeons and Dragons no es El Señor de los Anillos. Tampoco es Conan el Bárbaro, ni El Nombre del Viento. D&D, y la miríada de derivados que originó, son otra cosa: ni high, ni heroic ni low, ni dark fantasy, son fantasía dungeonera, son su propio género.",
                "",
                "",
                TagBlog
                );

            Articulo articulo5 = new Articulo(
                5,
                "Información Asimétrica",
                DuamnRuassol,
                new DateTime(2016, 7, 1, 12, 00, 00),
                "¡Hey! Hace mucho tiempo que no tiro unas líneas por acá, creo que ya es hora de que estire estos músculos rolerosóficos claramente oxidados. Originalmente quería publicar este artículo recién salido de la Back to the Dungeon que tuvimos a mediados de Marzo. Esta BttD me di el gusto de dirigir Miseries and Misfortunes, y la verdad es que la pasé bomba. Una de las cosas más entretenidas que saqué de la experiencia fue preparar la aventura y, como no quiero seguir descarrilando, voy a hablar de algo muy particular que me gusta hacer en mis mesas usando esta aventura como ejemplo.",
                "",
                "",
                TagBlog
                );

            Articulo articulo6 = new Articulo(
                6,
                "Gaceta Rúnica: ¡Muchos juegos y mecenazgos nuevos!",
                JuanmaAvila,
                new DateTime(2016, 9, 5, 12, 00, 00),
                "En la entrega de la Gaceta Rúnica de esta quincena vamos a intentar cubrir la avalancha de lanzamientos de juegos nuevos y proyectos de financiamiento que están sacudiendo el mundo de los juegos de rol.",
                "",
                "",
                TagBlog
                );

            Articulo articulo7 = new Articulo(
                7,
                "Desierto y pantano",
                JoaquinOllo,
                new DateTime(2015, 10, 9, 12, 00, 00),
                "EL mundo está hecho de contrastes. El mundo que quedó, al menos. El desierto es tierra de tránsito, de seres silenciosos, monocromía,  y mercaderes. Los restos de las ciudades son zonas hundidas y pantanosas: neón, derroche, fiestas, pieles picadas y rostros consumidos.",
                "",
                "",
                TagBlog
                );

            Articulo articulo8 = new Articulo(
                8,
                "Mundos virtuales",
                JoaquinOllo,
                new DateTime(2015, 7, 5, 12, 00, 00),
                "Buen día! Hoy nos toca inaugurar columna: \"Mazo de muchas cosas\" es una nueva columna quincenal, en la que nos relajamos un poco y nos dedicamos, esencialmente, a tirar links de cosas que nos parezcan interesantes. Lo importante acá es darte recursos para tus partidas de rol, ya sea música, imágenes, videos, o artículos: si te deja pensando y anotando ideas para jugar o diseñar, vamos bien.",
                "",
                "",
                TagBlog
                );

            Articulo articulo9 = new Articulo(
                9,
                "El ascenso del gólem",
                JoaquinOllo,
                new DateTime(2015, 4, 7, 12, 00, 00),
                "Lo siguiente es una sesión de rol improvisada para un amigo que nunca había jugado, a través del chat. Veníamos hablando hace rato de esta actividad, y ya que no había posibilidad de jugar presencialmente, ensayé esto que leerán a continuación. Aprovechando que fue por chat, reproduzco la \"sesión\" como diálogo entre jugador y GM (obviamente adaptado). La voz del GM aparece con sangría, y en cursiva. Ah, peligro: nos pusimos algo poéticos o solemnes mientras jugábamos, no se lo tomen demasiado en serio jajaj, no nos estamos creyendo Góngora.",
                "",
                "",
                TagBlog
                );

            List<Articulo> Articulos = new List<Articulo>();
            Articulos.Add(articulo1);
            Articulos.Add(articulo2);
            Articulos.Add(articulo3);
            Articulos.Add(articulo4);
            Articulos.Add(articulo5);
            Articulos.Add(articulo6);
            Articulos.Add(articulo7);
            Articulos.Add(articulo8);
            Articulos.Add(articulo9);

            List<Articulo> ArticulosFiltradosPodcast = new List<Articulo>();

            foreach (Articulo articulo in Articulos)
            {
                if (articulo.Tags.Nombre == TagPodcast.Nombre)
                {
                    ArticulosFiltradosPodcast.Add(articulo);
                }
            }

            ViewBag.Articulos = ArticulosFiltradosPodcast;

            return View();
        }

        public ActionResult Blog()
        {
            Usuario RunasExplosivas = new Usuario()
            {
                Mail = "RunasExplosivas@gmail.com",
                Nombre = "Staff de Runas Explosivas",
                Password = "RunEx"
            };

            Usuario JuanmaAvila = new Usuario()
            {
                Mail = "RunasExplosivas@gmail.com",
                Nombre = "Juan Manuel Ávila",
                Password = "RunEx"
            };
            Usuario JoaquinOllo = new Usuario()
            {
                Mail = "joaquinollo@gmail.com",
                Nombre = "Joaquín Ollo",
                Password = "RunEx"
            };
            Usuario DuamnRuassol = new Usuario()
            {
                Mail = "RunasExplosivas@gmail.com",
                Nombre = "Duamn Figueroa Ruassol",
                Password = "RunEx"
            };
            Usuario MartinvanHoutte = new Usuario()
            {
                Mail = "RunasExplosivas@gmail.com",
                Nombre = "Martín Van Houtte",
                Password = "RunEx"
            };

            /// DECLARACIÓN DE TAGS

            Tags TagBlog = new Tags(".blog", "pencil", true);

            Tags TagPodcast = new Tags(".podcast", "headphones", true);

            Tags TagEditorial = new Tags(".editorial", "book", true);

            Tags TagNoticia = new Tags(".noticia", "info-sign", true);

            Tags TagResena = new Tags(".reseña", "flag", true);

            Tags TagCuentacuentos = new Tags(".cuentacuentos", "file", true);

            Tags TagRolerosofia = new Tags(".rolerosofía", "eye-open", true);

            Tags TagRolerodromo = new Tags(".roleródromo", "flash", true);

            /// DECLARACIÓN DE ARTÍCULOS


            Articulo articulo1 = new Articulo(
                1,
                "PE nº66: Aplicando Resolución de Conflictos a tu Mesa",
                RunasExplosivas,
                new DateTime(2017, 3, 30, 12, 00, 00),
                "¡Otro Miércoles de #PodcastExplosivo! Hoy vamos a hablar sobre cómo aplicar lo que aprendimos de juegos con resoluciones de conflicto no binarias a otros juegos más tradicionales. Ideas new school para juegos con raíces en los 80'/90'.",
                "https://www.youtube.com/watch?v=y-GIvQAsn6w",
                "",
                TagPodcast
                );

            Articulo articulo2 = new Articulo(
                2,
                "PE nº65 - Las Grandes Tres, parte 3: ¿Qué hacen personajes y jugadores?",
                RunasExplosivas,
                new DateTime(2017, 3, 22, 12, 00, 00),
                "¡Primer (nuevo) Miércoles del #PodcastExplosivo! Hoy vamos a terminar un tema vital para cualquier tipo de diseño rolero: la tercera y última parte de las Grandes Tres preguntas que todo diseñador debería hacerse en algún momento antes de publicar.",
                "https://www.youtube.com/watch?v=sBlWx6Z28Pw",
                "",
                TagPodcast
                );

            Articulo articulo3 = new Articulo(
                3,
                "Reseña: Princes of the Apocalypse D&D",
                RunasExplosivas,
                new DateTime(2016, 8, 8, 12, 00, 00),
                "seawefaefdfadfaefaefaefsdfasd",
                "",
                "",
                TagBlog
                );

            Articulo articulo4 = new Articulo(
                4,
                "Murder Hobos & Fantasy Rockstars",
                DuamnRuassol,
                new DateTime(2016, 7, 20, 12, 00, 00),
                "Ninguna party de aventureros es la Comunidad del Anillo. Momento, tal vez me estoy adelantando, volvamos unos pasos en mi tren de pensamiento. Dungeons and Dragons no es El Señor de los Anillos. Tampoco es Conan el Bárbaro, ni El Nombre del Viento. D&D, y la miríada de derivados que originó, son otra cosa: ni high, ni heroic ni low, ni dark fantasy, son fantasía dungeonera, son su propio género.",
                "",
                "",
                TagBlog
                );

            Articulo articulo5 = new Articulo(
                5,
                "Información Asimétrica",
                DuamnRuassol,
                new DateTime(2016, 7, 1, 12, 00, 00),
                "¡Hey! Hace mucho tiempo que no tiro unas líneas por acá, creo que ya es hora de que estire estos músculos rolerosóficos claramente oxidados. Originalmente quería publicar este artículo recién salido de la Back to the Dungeon que tuvimos a mediados de Marzo. Esta BttD me di el gusto de dirigir Miseries and Misfortunes, y la verdad es que la pasé bomba. Una de las cosas más entretenidas que saqué de la experiencia fue preparar la aventura y, como no quiero seguir descarrilando, voy a hablar de algo muy particular que me gusta hacer en mis mesas usando esta aventura como ejemplo.",
                "",
                "",
                TagBlog
                );

            Articulo articulo6 = new Articulo(
                6,
                "Gaceta Rúnica: ¡Muchos juegos y mecenazgos nuevos!",
                JuanmaAvila,
                new DateTime(2016, 9, 5, 12, 00, 00),
                "En la entrega de la Gaceta Rúnica de esta quincena vamos a intentar cubrir la avalancha de lanzamientos de juegos nuevos y proyectos de financiamiento que están sacudiendo el mundo de los juegos de rol.",
                "",
                "",
                TagBlog
                );

            Articulo articulo7 = new Articulo(
                7,
                "Desierto y pantano",
                JoaquinOllo,
                new DateTime(2015, 10, 9, 12, 00, 00),
                "EL mundo está hecho de contrastes. El mundo que quedó, al menos. El desierto es tierra de tránsito, de seres silenciosos, monocromía,  y mercaderes. Los restos de las ciudades son zonas hundidas y pantanosas: neón, derroche, fiestas, pieles picadas y rostros consumidos.",
                "",
                "",
                TagBlog
                );

            Articulo articulo8 = new Articulo(
                8,
                "Mundos virtuales",
                JoaquinOllo,
                new DateTime(2015, 7, 5, 12, 00, 00),
                "Buen día! Hoy nos toca inaugurar columna: \"Mazo de muchas cosas\" es una nueva columna quincenal, en la que nos relajamos un poco y nos dedicamos, esencialmente, a tirar links de cosas que nos parezcan interesantes. Lo importante acá es darte recursos para tus partidas de rol, ya sea música, imágenes, videos, o artículos: si te deja pensando y anotando ideas para jugar o diseñar, vamos bien.",
                "",
                "",
                TagBlog
                );

            Articulo articulo9 = new Articulo(
                9,
                "El ascenso del gólem",
                JoaquinOllo,
                new DateTime(2015, 4, 7, 12, 00, 00),
                "Lo siguiente es una sesión de rol improvisada para un amigo que nunca había jugado, a través del chat. Veníamos hablando hace rato de esta actividad, y ya que no había posibilidad de jugar presencialmente, ensayé esto que leerán a continuación. Aprovechando que fue por chat, reproduzco la \"sesión\" como diálogo entre jugador y GM (obviamente adaptado). La voz del GM aparece con sangría, y en cursiva. Ah, peligro: nos pusimos algo poéticos o solemnes mientras jugábamos, no se lo tomen demasiado en serio jajaj, no nos estamos creyendo Góngora.",
                "",
                "",
                TagBlog
                );

            List<Articulo> Articulos = new List<Articulo>();
            Articulos.Add(articulo1);
            Articulos.Add(articulo2);
            Articulos.Add(articulo3);
            Articulos.Add(articulo4);
            Articulos.Add(articulo5);
            Articulos.Add(articulo6);
            Articulos.Add(articulo7);
            Articulos.Add(articulo8);
            Articulos.Add(articulo9);

            List<Articulo> ArticulosFiltradosBlog = new List<Articulo>();

            foreach (Articulo articulo in Articulos)
            {
                if (articulo.Tags.Nombre == TagBlog.Nombre)
                {
                    ArticulosFiltradosBlog.Add(articulo);
                }
            }

            ViewBag.Articulos = ArticulosFiltradosBlog;

            return View();
        }

        public ActionResult Editorial()
        {
            return View();
        }

        public ActionResult Contacto()
        {
            return View();
        }

        public ActionResult Articulo(int blogpostID)
        {
            /// DECLARACIÓN DE USUARIO PRUEBA1

            Usuario RunasExplosivas = new Usuario();
            RunasExplosivas.Mail = "RunasExplosivas@gmail.com";
            RunasExplosivas.Nombre = "Staff de Runas Explosivas";
            RunasExplosivas.Password = "RunEx";

            Usuario JuanmaAvila = new Usuario();
            JuanmaAvila.Mail = "RunasExplosivas@gmail.com";
            JuanmaAvila.Nombre = "Juan Manuel Ávila";
            JuanmaAvila.Password = "RunEx";

            Usuario JoaquinOllo = new Usuario();
            JoaquinOllo.Mail = "joaquinollo@gmail.com";
            JoaquinOllo.Nombre = "Joaquín Ollo";
            JoaquinOllo.Password = "RunEx";

            Usuario DuamnRuassol = new Usuario();
            DuamnRuassol.Mail = "RunasExplosivas@gmail.com";
            DuamnRuassol.Nombre = "Duamn Figueroa Ruassol";
            DuamnRuassol.Password = "RunEx";

            Usuario MartinvanHoutte = new Usuario();
            MartinvanHoutte.Mail = "RunasExplosivas@gmail.com";
            MartinvanHoutte.Nombre = "Martín Van Houtte";
            MartinvanHoutte.Password = "RunEx";

            /// DECLARACIÓN DE TAGS

            Tags TagBlog = new Tags(".blog", "pencil", true);

            Tags TagPodcast = new Tags(".podcast", "headphones", true);

            Tags TagEditorial = new Tags(".editorial", "book", true);

            Tags TagNoticia = new Tags(".noticia", "info-sign", true);

            Tags TagResena = new Tags(".reseña", "flag", true);

            Tags TagCuentacuentos = new Tags(".cuentacuentos", "file", true);

            Tags TagRolerosofia = new Tags(".rolerosofía", "eye-open", true);

            Tags TagRolerodromo = new Tags(".roleródromo", "flash", true);

            /// DECLARACIÓN DE ARTÍCULOS


            Articulo articulo1 = new Articulo(
                1,
                "PE nº66: Aplicando Resolución de Conflictos a tu Mesa",
                RunasExplosivas,
                new DateTime(2017, 3, 30, 12, 00, 00),
                "¡Otro Miércoles de #PodcastExplosivo! Hoy vamos a hablar sobre cómo aplicar lo que aprendimos de juegos con resoluciones de conflicto no binarias a otros juegos más tradicionales. Ideas new school para juegos con raíces en los 80'/90'.",
                "https://www.youtube.com/watch?v=y-GIvQAsn6w",
                "",
                TagPodcast
                );

            Articulo articulo2 = new Articulo(
                2,
                "PE nº65 - Las Grandes Tres, parte 3: ¿Qué hacen personajes y jugadores?",
                RunasExplosivas,
                new DateTime(2017, 3, 22, 12, 00, 00),
                "¡Primer (nuevo) Miércoles del #PodcastExplosivo! Hoy vamos a terminar un tema vital para cualquier tipo de diseño rolero: la tercera y última parte de las Grandes Tres preguntas que todo diseñador debería hacerse en algún momento antes de publicar.",
                "https://www.youtube.com/watch?v=sBlWx6Z28Pw",
                "",
                TagPodcast
                );

            Articulo articulo3 = new Articulo(
                3,
                "Reseña: Princes of the Apocalypse D&D",
                RunasExplosivas,
                new DateTime(2016, 8, 8, 12, 00, 00),
                "seawefaefdfadfaefaefaefsdfasd",
                "",
                "",
                TagBlog
                );

            Articulo articulo4 = new Articulo(
                4,
                "Murder Hobos & Fantasy Rockstars",
                DuamnRuassol,
                new DateTime(2016, 7, 20, 12, 00, 00),
                "Ninguna party de aventureros es la Comunidad del Anillo. Momento, tal vez me estoy adelantando, volvamos unos pasos en mi tren de pensamiento. Dungeons and Dragons no es El Señor de los Anillos. Tampoco es Conan el Bárbaro, ni El Nombre del Viento. D&D, y la miríada de derivados que originó, son otra cosa: ni high, ni heroic ni low, ni dark fantasy, son fantasía dungeonera, son su propio género.",
                "",
                "",
                TagBlog
                );

            Articulo articulo5 = new Articulo(
                5,
                "Información Asimétrica",
                DuamnRuassol,
                new DateTime(2016, 7, 1, 12, 00, 00),
                "¡Hey! Hace mucho tiempo que no tiro unas líneas por acá, creo que ya es hora de que estire estos músculos rolerosóficos claramente oxidados. Originalmente quería publicar este artículo recién salido de la Back to the Dungeon que tuvimos a mediados de Marzo. Esta BttD me di el gusto de dirigir Miseries and Misfortunes, y la verdad es que la pasé bomba. Una de las cosas más entretenidas que saqué de la experiencia fue preparar la aventura y, como no quiero seguir descarrilando, voy a hablar de algo muy particular que me gusta hacer en mis mesas usando esta aventura como ejemplo.",
                "",
                "",
                TagBlog
                );

            Articulo articulo6 = new Articulo(
                6,
                "Gaceta Rúnica: ¡Muchos juegos y mecenazgos nuevos!",
                JuanmaAvila,
                new DateTime(2016, 9, 5, 12, 00, 00),
                "En la entrega de la Gaceta Rúnica de esta quincena vamos a intentar cubrir la avalancha de lanzamientos de juegos nuevos y proyectos de financiamiento que están sacudiendo el mundo de los juegos de rol.",
                "",
                "",
                TagBlog
                );

            Articulo articulo7 = new Articulo(
                7,
                "Desierto y pantano",
                JoaquinOllo,
                new DateTime(2015, 10, 9, 12, 00, 00),
                "EL mundo está hecho de contrastes. El mundo que quedó, al menos. El desierto es tierra de tránsito, de seres silenciosos, monocromía,  y mercaderes. Los restos de las ciudades son zonas hundidas y pantanosas: neón, derroche, fiestas, pieles picadas y rostros consumidos.",
                "",
                "",
                TagBlog
                );

            Articulo articulo8 = new Articulo(
                8,
                "Mundos virtuales",
                JoaquinOllo,
                new DateTime(2015, 7, 5, 12, 00, 00),
                "Buen día! Hoy nos toca inaugurar columna: \"Mazo de muchas cosas\" es una nueva columna quincenal, en la que nos relajamos un poco y nos dedicamos, esencialmente, a tirar links de cosas que nos parezcan interesantes. Lo importante acá es darte recursos para tus partidas de rol, ya sea música, imágenes, videos, o artículos: si te deja pensando y anotando ideas para jugar o diseñar, vamos bien.",
                "",
                "",
                TagBlog
                );

            Articulo articulo9 = new Articulo(
                9,
                "El ascenso del gólem",
                JoaquinOllo,
                new DateTime(2015, 4, 7, 12, 00, 00),
                "Lo siguiente es una sesión de rol improvisada para un amigo que nunca había jugado, a través del chat. Veníamos hablando hace rato de esta actividad, y ya que no había posibilidad de jugar presencialmente, ensayé esto que leerán a continuación. Aprovechando que fue por chat, reproduzco la \"sesión\" como diálogo entre jugador y GM (obviamente adaptado). La voz del GM aparece con sangría, y en cursiva. Ah, peligro: nos pusimos algo poéticos o solemnes mientras jugábamos, no se lo tomen demasiado en serio jajaj, no nos estamos creyendo Góngora.",
                "",
                "",
                TagBlog
                );

            List<Articulo> Articulos = new List<Articulo>();
            Articulos.Add(articulo1);
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
                if (blogpostID == articulo.ID)
                {
                    ViewBag.Articulo = articulo;
                    switch (articulo.Tags.Nombre) // CAMBIAR ESTO CUANDO SE IMPLEMENTE LISTA DE TAGS NUEVAMENTE
                    {
                        case ".blog":
                            ViewBag.Title = "Blog";
                            ViewBag.Btn = "btn-lg";
                            ViewBag.BtnUsuario = "";
                            ViewBag.BtnBuscar = "";
                            ViewBag.BtnCarrito = "hide";
                            ViewBag.HeaderText = ".blog";
                            ViewBag.HeaderGlyph = "glyphicon glyphicon-pencil";
                            break;
                        case ".podcast":
                            ViewBag.Title = "Podcast";
                            ViewBag.Btn = "btn-lg";
                            ViewBag.BtnUsuario = "";
                            ViewBag.BtnBuscar = "";
                            ViewBag.BtnCarrito = "hide";
                            ViewBag.HeaderText = ".podcast";
                            ViewBag.HeaderGlyph = "glyphicon glyphicon-headphones";
                            break;
                        default:
                            break;
                    }
                    ViewBag.Title = ViewBag.Title + ": " + articulo.Titulo;
                    break;
                }
            }

            return View();
        }

        public ActionResult PruebasVarias()
        {
            return View();
        }

        public ActionResult Mensaje(string nombre, string email, string mensaje, string SendTo)
        {
            SmtpClient clienteSmtp = new SmtpClient();
            clienteSmtp.Host = "smtp.gmail.com";
            clienteSmtp.Port = 587;
            clienteSmtp.Credentials = new NetworkCredential("joaquinollo@gmail.com", "qwe11ny23aZ");
            clienteSmtp.EnableSsl = true;


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("joaquinollo@gmail.com", "Joaquin Ollo");
            mail.To.Add("joaquinollo@gmail.com");
            mail.Subject = "Te contactaron del blog";
            mail.Body = nombre + " (" + email + ") te contactó desde la aplicación, y te dejó el sig. mensaje: " + mensaje;
            clienteSmtp.Send(mail);

            //mail para el usuario
            MailMessage mensajeParaUsuario = new MailMessage();
            mensajeParaUsuario.From = new MailAddress("joaquinollo@gmail.com", "Joaquin Ollo");
            mensajeParaUsuario.To.Add(email);
            mensajeParaUsuario.Subject="Gracias por contactarte con el Blog!";
            mensajeParaUsuario.Body= "Gracias por contactarte con el staff de Runas Explosivas! Tu mensaje será respondido a la brevedad.";

            clienteSmtp.Send(mensajeParaUsuario);
            return View("Index");
        }


    }
}