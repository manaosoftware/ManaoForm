namespace Umbraco.Tests.IoC
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            //Mapper.CreateMap<IPublishedContent, SearchResultViewModel>()
            //    .ForMember(dest => dest.Title, opt => { opt.MapFrom(source => source.HasValue("menuTitle") ? source.GetPropertyValue("menuTitle").ToString() : source.Name); })
            //    .ForMember(dest => dest.Content, opt => { opt.MapFrom(source => source); })
            //    ;
        }
    }
}
