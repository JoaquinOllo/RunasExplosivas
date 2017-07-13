using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    [Table("Productos")]
    public class Producto : Entrada
    {
        public List<Categoria> Categorias { get; set; }
        public float Precio { get; set; }
        public int Stock { get; set; }

        public override string AllTags
        {
            get
            {
                foreach (Categoria Categoria in Categorias)
                {
                    AllTags = AllTags + "." + Categoria.Nombre;
                }
                return AllTags;
            }
            set { AllTags = value; }
        }

        /// <summary>
        /// Método para buscar una Categoría en la lista de Categorías.
        /// </summary>
        /// <param name="ValorABuscar">Parámetro que debe coincidir con el atributo Nombre de la Categoría que se esté buscando.</param>
        /// <returns>Devuelve Booleano true si la Categoría es encontrada en la lista, False en caso contrario.</returns>
        public override bool SearchTag(string ValorABuscar)
        {
            foreach (Categoria Categoria in Categorias)
            {
                if (Categoria.Nombre == ValorABuscar)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Método para obtener HTML de Glyphicons
        /// </summary>
        /// <returns>Devuelve htmlstring con el HTML del glyphicon</returns>
        public override HtmlString AllGlyphHTML()
        {
            String CadenaGlyphs = "";
            HtmlString CadenaGlyphsHTML = new HtmlString("");
            foreach (Categoria Categoria in Categorias)
            {
                CadenaGlyphs = CadenaGlyphs + Categoria.GlyphHTML.ToString();
            }
            CadenaGlyphsHTML = new HtmlString(CadenaGlyphs);
            return CadenaGlyphsHTML;
        }
    }
}