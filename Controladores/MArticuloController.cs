using Modelos.Entidades;
using AutoMapper;
using System.Collections.Generic;

namespace Controladores
{
    public class MArticuloController : EArticulo
    {
        public IConfigurationProvider config = new MapperConfiguration(cfg =>
           cfg.CreateMap<EArticulo, MArticuloController>()
            .ForMember(dest => dest.ID, act => act.MapFrom(src => src.ID))
            .ForMember(dest => dest.Eliminado, act => act.MapFrom(src => src.Eliminado))
            .ForMember(dest => dest.Descripcion, act => act.MapFrom(src => src.Descripcion))
            .ForMember(dest=>dest.Precio,act=>act.MapFrom(src=>src.Precio))
       );
        public MArticuloController Map(EArticulo origen)
        {
            var mapper = new Mapper(config);
            return mapper.Map<EArticulo,MArticuloController>(origen);
        }
        public List<MArticuloController> MapList(List<EArticulo> origenList)
        {
            var mapper = new Mapper(config);
            return mapper.Map<List<EArticulo>, List<MArticuloController>>(origenList);
        }
    }
}
