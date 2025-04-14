using AutoMapper;
using ToDoListManagement.Bll.DTOs;
using ToDoListManagement.Dal.Entity;

namespace ToDoListManagement.Bll.Mappers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ToDoItem, ToDoListCreateDto>().ReverseMap();
        CreateMap<ToDoItem, ToDoListGetDto>().ReverseMap();
    }
}
