using Modelos.Entidades;
using AutoMapper;
using System.Collections.Generic;

namespace Controladores
{
    //este catálogo hereda de Modelos.EArticulo, así que tengo todas las propiedades de EArticulo.
    public class MArticuloController : EArticulo
    {
        //configuramos el automapper para pasar métodos de un objeto a otro.
        public IConfigurationProvider config = new MapperConfiguration(cfg =>
           cfg.CreateMap<EArticulo, MArticuloController>()
            .ForMember(dest => dest.ID, act => act.MapFrom(src => src.ID))
            .ForMember(dest => dest.Eliminado, act => act.MapFrom(src => src.Eliminado))
            .ForMember(dest => dest.Descripcion, act => act.MapFrom(src => src.Descripcion))
            .ForMember(dest=>dest.Precio,act=>act.MapFrom(src=>src.Precio))
       );
        //método para mapear de un objeto del modelo a uno del controlador.
        public MArticuloController Map(EArticulo origen)
        {
            var mapper = new Mapper(config);
            return mapper.Map<EArticulo,MArticuloController>(origen);
        }
        //lo mismo pero para listas.
        public List<MArticuloController> MapList(List<EArticulo> origenList)
        {
            var mapper = new Mapper(config);
            return mapper.Map<List<EArticulo>, List<MArticuloController>>(origenList);
        }
    }
}
