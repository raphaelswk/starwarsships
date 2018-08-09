using AutoMapper;
using SWSC.Entities;
using SWSC.Presentation.MVC.ViewModels;

namespace SWSC.Presentation.MVC.Mappers
{
    public static class SWSCMapper
    {
        static SWSCMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<StarshipVM, Starship>();
                cfg.CreateMap<Starship, StarshipVM>();
            });
        }

        public static P Map<T, P>(T source)
        {
            return Mapper.Map<T, P>(source);
        }
    }
}
