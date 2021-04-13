using System.Collections.Generic;
using Modelos;
using Modelos.Entidades;

namespace Controladores
{
    public class CArticulos
    {
        public string Guardar(string descripcion, double precio)
        {
            EArticulo articulo = new EArticulo { Descripcion = descripcion, Precio = precio };
            MArticulos mArticulo = new MArticulos();
            if (mArticulo.Guardar(articulo))
            {
                return "Artículo guardado correctamente";
            }
            else
            {
                return "Error:" + mArticulo.Error;
            }
            
        }
        public string Desactivar(int id)
        {
            EArticulo articulo = new EArticulo { ID=id };
            MArticulos mArticulo = new MArticulos();
            if (mArticulo.Desactivar(articulo))
            {
                return "Artículo desactivado.";
            }
            else
            {
                return "Error:" + mArticulo.Error;
            }
        }
        public string Modificar(int id, string descripcion, double precio)
        {
            MArticulos mArticulo = new MArticulos();
            EArticulo articulo = new EArticulo { ID = id, Descripcion=descripcion, Precio=precio };
            if (mArticulo.Modificar(articulo))
            {
                return "Artículo modificado correctamente";
            }
            else
            {
                return "Error:" + mArticulo.Error;
            }
        }
        public List<MArticuloController> Listar()
        {
            return new MArticuloController().MapList( new MArticulos().Listar());
        }
    }
}
