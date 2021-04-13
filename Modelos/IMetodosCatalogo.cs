using Modelos.Entidades;
using System.Collections.Generic;

namespace Modelos
{
    //interfaz genérica para los métodos de todos los catálogos futuros
    interface IIMetodosCatalogo<T>
    {
        public bool Guardar(EArticulo articulo);
        public bool Modificar(EArticulo aritculo);
        public bool Desactivar(EArticulo aritculo);

        public List<EArticulo> Listar();
    }
}
