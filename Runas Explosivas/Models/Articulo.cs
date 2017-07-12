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
    public class Articulo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Titulo { get; set; }
        public Usuario Autor { get; set; }
        public List<Tags> Tags { get; set; }
        public DateTime Fecha { get; set; }
        public string Texto { get; set; }
        public string Link { get; set; }
        public string Imagen { get; set; }
        public string PreviewText { get; set; }
        public string AllTags { get; }
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

        public Articulo (int nID, string nTitulo, Usuario nAutor, DateTime nFecha, string nTexto, string nLink, string nImagen, params Tags[] nTags)
        {
            ID = nID;
            Titulo = nTitulo;
            Autor = nAutor;
            Fecha = nFecha;
            Texto = nTexto;
            Link = nLink;
            Imagen = nImagen;
            Tags = new List<Tags>();
            foreach (Tags tag in nTags)
            {
                Tags.Add(tag);
                AllTags = AllTags + tag.Nombre;
            }
        }
        /// <summary>
        /// Método para obtener HTML de Glyphicons
        /// </summary>
        /// <param name="Tag">Parámetro Tag de clase Tags</param>
        /// <returns>Devuelve string con el HTML del glyphicon</returns>
        public HtmlString AllGlyphHTML()
        {
            String CadenaGlyphs = "";
            HtmlString CadenaGlyphsHTML = new HtmlString("");
            foreach (Tags tag in Tags)
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
            foreach (Tags tag in Tags)
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