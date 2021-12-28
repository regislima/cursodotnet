using api.Domain.Models;
using api.Resources;
using AutoMapper;

namespace api.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<CategoryResource, Category>();
            CreateMap<ProductResource, Product>();
            CreateMap<UserResource, User>();
        }
    }
}