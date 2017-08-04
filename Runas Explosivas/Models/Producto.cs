using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Hosting;

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
/// Método que devuelve un fragmento del largo especificado del texto descriptivo del artículo.
/// </summary>
/// <param name="lengthPreview">length de tipo entero del preview del artículo a devolver</param>
/// <returns>devuelve un string del largo especificado más puntos suspensivos</returns>
        public string GetPreviewText(int lengthPreview)
        {
            List<string> PuntosDeParada = new List<string>() { ".", ",", " ", ":", ";" };
            int IndexDeParada = Texto.Length;
            if (lengthPreview > Texto.Length)
            {
                IndexDeParada = Texto.Length;
            }
            else
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
            string CadenaDeAutores = Autores.Count == 1 ? "Autor: " : "Autores: ";
            return CadenaDeAutores + String.Join(", ", Autores.Select(au => au.Nombre).ToArray());
        }

        public virtual ICollection<Categoria> Categorias { get; set; }
        public virtual ICollection<ProductoEnCarro> ProductosEnCarro { get; set; }


        public float Precio { get; set; }

        /// <summary>
        /// Método que devuelve una lista de las categorías con el formato apropiado para ser mostradas en la vista 
        /// </summary>
        /// <param name="soloPrioridad">parámetro booleano, true por defecto, para especificar si la lista contiene sólo las categorías con prioridad = true, o no</param>
        /// <param name="cantidadElementos">parámetro entero, 3 por defecto, para especificar la cantidad de elementos que debe contener la lista de categorías. Si se pasa 0 como parámetro, se muestran todas las categorías.</param>
        /// <returns></returns>
        public List<string> AllCategorias(bool soloPrioridad = true, int cantidadElementos = 3)
        {
            IEnumerable<string> _AllCategorias;
            if (soloPrioridad)
            {
                _AllCategorias = Categorias.Where(cat => cat.Prioridad).Select(cat => "." + cat.Nombre);
            } else
            {
                _AllCategorias = Categorias.Select(cat => "." + cat.Nombre);
            }
            if (cantidadElementos == 0)
            {
                return _AllCategorias.ToList();
            } else
            {
                return _AllCategorias.Take(cantidadElementos).ToList();
            }
        }

        public string CategoriasToString
        {
            get
            {
                return String.Join("", AllCategorias(false, 0).ToArray());
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

        public string ImagenFullPath
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Imagen))
                {
                    return "~/Content/Images/Tienda/" + Imagen;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}