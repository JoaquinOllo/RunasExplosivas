using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    public static class LinqExtension
    {

        public static int IndexOfMinDate(this IEnumerable<DateTime> source)
        {
            if (source == null)
                return -1;

            DateTime minValue = DateTime.MaxValue;
            int minIndex = -1;
            int index = -1;

            foreach (DateTime date in source)
            {
                index++;

                if (DateTime.Compare(date, minValue) < 0)
                {
                    minValue = date;
                    minIndex = index;
                }
            }

            return minIndex;
        }

        public static int IndexOfMinDate(this IEnumerable<DateTime> source, DateTime minDate)
        {
            if (source == null)
                return -1;

            DateTime minValue = DateTime.MaxValue;
            int minIndex = -1;
            int index = -1;

            foreach (DateTime date in source)
            {
                index++;

                if (DateTime.Compare(date, minDate) > 0 && DateTime.Compare(date, minValue) < 0)
                {
                    minValue = date;
                    minIndex = index;
                }
            }

            return minIndex;
        }

        public static string IterarOrdenarComentarios (int ArtID,
            int NivelDeAnidacion = 0, int MarcadorID = default(int), DateTime MarcadorFecha = default(DateTime))
        {
            using (RunasContext db = new Models.RunasContext())
            {
                IEnumerable<ComentarioEnArticulo> ListaComentarios = NivelDeAnidacion == 0 ?
                    db.ComentariosEnArticulos
                    .Include("ArticuloComentado").Include("RespuestaA")
                    .Where(C => C.ArticuloComentado.ID == ArtID) :
                    db.ComentariosEnArticulos
                    .Include("ArticuloComentado").Include("RespuestaA")
                    .Where(C => C.ArticuloComentado.ID == ArtID && C.RespuestaA.ID == MarcadorID);
                string CadenaDestino = string.Empty;
                MarcadorFecha = MarcadorFecha == default(DateTime) ? DateTime.MinValue : MarcadorFecha;
                if (NivelDeAnidacion >= 4)
                {
                    return CadenaDestino;
                }

                while (true)
                {
                    MarcadorID = ListaComentarios.Select(C => C.Fecha).IndexOfMinDate(MarcadorFecha);
                    MarcadorID = ListaComentarios.ElementAt(MarcadorID).ID;
                    MarcadorFecha = ListaComentarios.AsEnumerable().First(C => C.ID == MarcadorID).Fecha;
                    if (MarcadorID >= 0)
                    {
                        CadenaDestino += MarcadorID.ToString() + ";"
                            + IterarOrdenarComentarios(ArtID, NivelDeAnidacion + 1, MarcadorID, MarcadorFecha);
                    }
                    else
                    {
                        return CadenaDestino;
                    }
                }
            }
        }
    }
}