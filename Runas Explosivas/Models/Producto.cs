using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    [Table("Productos")]
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage ="Títulos de productos no pueden exceder los 50 caracteres")]
        public string Titulo { get; set; }
        [Required]
        public virtual ICollection<Usuario> Autores { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Url]
        public string Link { get; set; }
        [Required]
        public string Texto { get; set; }
        [Required]
        public string Imagen { get; set; }
        [Required]
        public int Stock { get; set; }

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

        public virtual ICollection<Categoria> Categorias { get; set; }
        public float Precio { get; set; }

        [NotMapped]
        public string AllTags
        {
            get
            {
                string _AllTags = string.Empty;
                foreach (Categoria Categoria in Categorias)
                {
                    _AllTags = _AllTags + "." + Categoria.Nombre;
                }
                return _AllTags;
            }
        }

        /// <summary>
        /// Método para buscar una Categoría en la lista de Categorías.
        /// </summary>
        /// <param name="ValorABuscar">Parámetro que debe coincidir con el atributo Nombre de la Categoría que se esté buscando.</param>
        /// <returns>Devuelve Booleano true si la Categoría es encontrada en la lista, False en caso contrario.</returns>
        public bool SearchTag(string ValorABuscar)
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
    }
}