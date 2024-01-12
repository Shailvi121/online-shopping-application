using AutoMapper;
using Online_Shopping_Application.API.ViewModel;

namespace Online_Shopping_Application.API.Mapper
{


    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryViewModel, Category>().ReverseMap(); ;
            CreateMap<ProductViewModel, Product>().ReverseMap();
        }


    }
}
    
