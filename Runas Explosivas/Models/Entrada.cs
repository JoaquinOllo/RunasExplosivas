using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    public abstract class Entrada
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Titulo { get; set; }
        public List<Usuario> Autores { get; set; }
        public DateTime Fecha { get; set; }
        public string Link { get; set; }
        public string Texto { get; set; }
        public string Imagen { get; set; }
        public abstract string AllTags { get; set; }

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

        public string GetAllAutores ()
        {

            string CadenaDeAutores = "";
            if (Autores.Count > 1)
            {
                CadenaDeAutores = "Autores: ";
            } else
            {
                CadenaDeAutores = "Autor: ";
            }
            foreach (Usuario Autor in Autores)
            {
                CadenaDeAutores = CadenaDeAutores + Autor.Nombre;
            }
            
            return CadenaDeAutores;
        }

        abstract public bool SearchTag(string ValorABuscar);
        abstract public HtmlString AllGlyphHTML();
    }
}