using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Runas_Explosivas.Models
{
    [Table("Articulos")]
    public class Articulo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Títulos de artículos no pueden exceder los 100 caracteres")]
        public string Titulo { get; set; }
        [Required]
        public virtual ICollection<Usuario> Autores { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Url]
        public string Link { get; set; }
        [Required]
        public string Texto { get; set; }
        public string Imagen { get; set; }

        public string ImagenFullPath
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Imagen))
                {
                    return "~\\Content\\Images\\Articulos\\" + Imagen;
                } else
                {
                    return null;
                }
            }
        }


        /// <summary>
        /// Método que devuelve un fragmento del largo especificado del texto descriptivo del artículo.
        /// </summary>
        /// <param name="lengthPreview">length de tipo entero del preview del artículo a devolver</param>
        /// <returns>devuelve un string del largo especificado más puntos suspensivos</returns>
        public string GetPreviewText(int lengthPreview)
        {
            List<string> PuntosDeParada = new List<string>() {".", ",", " ", ":", ";" };
            int IndexDeParada = Texto.Length;
            if (lengthPreview > Texto.Length)
            {
                IndexDeParada = Texto.Length;
            } else
            {
                for (int i = lengthPreview; i > 0; i--)
                {
                    if (PuntosDeParada.Contains(Texto[i].ToString()))
                    {
                        IndexDeParada = i;
                        i = 0;
                    }
                }
            }

            return Texto.Substring(0, IndexDeParada) + "...";
        }

        /// <summary>
        /// Método que devuelve el total de autores de un artículo como un string
        /// </summary>
        /// <returns>Devuelve un string con los nombres de los autores separados por coma</returns>
        public string GetAllAutores()
        {
            string CadenaDeAutores = Autores.Count == 1 ? "Autor: ": "Autores: ";
            return CadenaDeAutores + String.Join(", ", Autores.Select(au => au.Nombre).ToArray());
        }

        public virtual ICollection<Tag> Tags { get; set; }

        /// <summary>
        /// Propiedad string para acceder a todos los tags de un artículo, separados por "."
        /// </summary>
        [NotMapped]
        public string AllTags
        {
            get
            {
                string _AllTags = String.Join(".", Tags.Select(t => t.Nombre).ToArray());
                return _AllTags;
            }
        }

        ///Constructores
        public Articulo (string nTitulo, ICollection<Usuario> nAutores, DateTime nFecha, string nTexto, string nLink, string nImagen, ICollection<Tag> nTags)
        {
            Titulo = nTitulo;
            Autores = nAutores;
            Fecha = nFecha;
            Texto = nTexto;
            Link = nLink;
            Imagen = nImagen;
            Tags = nTags;
        }

        public Articulo()
        {
        }

        /// <summary>
        /// Método para obtener HTML de Glyphicons
        /// </summary>
        /// <returns>Devuelve htmlstring con el HTML del glyphicon</returns>
        public HtmlString AllGlyphHTML()
        {
            String CadenaGlyphs = "";
            HtmlString CadenaGlyphsHTML = new HtmlString("");
            foreach (Tag tag in Tags)
            {
                CadenaGlyphs = CadenaGlyphs + tag.GlyphHTML.ToString();
            }
            CadenaGlyphsHTML = new HtmlString (CadenaGlyphs);
            return CadenaGlyphsHTML;
        }

        /// <summary>
        /// Método para buscar un Tag en la lista de Tags.
        /// </summary>
        /// <param name="ValorABuscar">Parámetro que debe coincidir con el atributo Nombre del Tag que se esté buscando.</param>
        /// <returns>Devuelve Booleano true si el Tag es encontrado en la lista, False en caso contrario.</returns>
        public bool SearchTag (string ValorABuscar)
        {
            foreach (Tag tag in Tags)
            {
                if (tag.Nombre == ValorABuscar)
                {
                    return true;
                }
            }
            return false;
        }
    }
}