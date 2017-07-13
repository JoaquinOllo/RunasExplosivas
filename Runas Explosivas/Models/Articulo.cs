using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    /// <summary>
    /// Representa a la entidad Artículo del blog
    /// </summary>
    [Table("Articulos")]
    public class Articulo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Titulo { get; set; }
        public virtual ICollection<Usuario> Autores { get; set; }
        public DateTime Fecha { get; set; }
        public string Link { get; set; }
        public string Texto { get; set; }
        public string Imagen { get; set; }

        /// <summary>
        /// Método para obtener los primeros caracteres de un texto de artículo, para mostrar en páginas de inicio
        /// </summary>
        /// <param name="characters">Parámetro para especificar cantidad de caracteres del preview</param>
        /// <returns>Devuelve un string con los primeros caracteres del texto.</returns>
        public string GetPreviewText(int characters)
        {
            if (characters >= Texto.Length)
            {
                characters = Texto.Length;
            }
            return Texto.Substring(0, characters) + "...";
        }

        public string GetAllAutores()
        {

            string CadenaDeAutores = "";
            if (Autores.Count > 1)
            {
                CadenaDeAutores = "Autores: ";
            }
            else
            {
                CadenaDeAutores = "Autor: ";
            }
            foreach (Usuario Autor in Autores)
            {
                CadenaDeAutores = CadenaDeAutores + Autor.Nombre;
            }

            return CadenaDeAutores;
        }

        public virtual ICollection<Tag> Tags { get; set; }

        [NotMapped]
        public string AllTags
        {
            get
            {
                string _AllTags = string.Empty;
                foreach (Tag tag in Tags)
                {
                    _AllTags = _AllTags + "." + tag.Nombre;
                }
                return _AllTags;
            }
        }

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