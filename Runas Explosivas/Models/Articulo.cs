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
    public class Articulo : Entrada
    {
        public List<Tag> Tags { get; set; }

        public override string AllTags
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
            set
            {
                string _AllTags = string.Empty;
                _AllTags = value;
            }
        }

        public Articulo (string nTitulo, List<Usuario> nAutores, DateTime nFecha, string nTexto, string nLink, string nImagen, List<Tag> nTags)
        {
            Titulo = nTitulo;
            Autores = nAutores;
            Fecha = nFecha;
            Texto = nTexto;
            Link = nLink;
            Imagen = nImagen;
            Tags = nTags;
            //Tags = new List<Tag>();
            //foreach (Tag tag in nTags)
            //{
            //    Tags.Add(tag);
            //}
        }
        /// <summary>
        /// Método para obtener HTML de Glyphicons
        /// </summary>
        /// <returns>Devuelve htmlstring con el HTML del glyphicon</returns>
        public override HtmlString AllGlyphHTML()
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
        public override bool SearchTag (string ValorABuscar)
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