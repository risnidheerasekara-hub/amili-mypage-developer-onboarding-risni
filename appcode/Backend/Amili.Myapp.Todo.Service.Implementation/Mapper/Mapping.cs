using Amili.Myapp.Todo.Service.Core.Models.Request;
using Amili.Myapp.Todo.Service.Core.Models.Response;
using AutoMapper;
using DataModels = Amili.Myapp.Todo.Service.Core.DataModels;

namespace Amili.Myapp.Todo.Service.Implementation.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // CreateMap<TSource, TDestination>() 
        CreateMap<DataModels.Todo, TodoResponse>();
        CreateMap<CreateTodoRequest, DataModels.Todo>().ForMember(
            dest => dest.CreatedAt, src => src.MapFrom(x => DateTime.UtcNow));
    }
}