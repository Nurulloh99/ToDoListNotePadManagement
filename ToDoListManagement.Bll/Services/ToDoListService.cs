using AutoMapper;
using FluentValidation;
using ToDoListManagement.Bll.DTOs;
using ToDoListManagement.Dal.Entity;
using ToDoListManagement.Repository.Services;
using ToDoListManagement.Repository.Settings;

namespace ToDoListManagement.Bll.Services;

public class ToDoListService : IToDoListService
{
    private readonly IToDoListRepositoryAdoNet _toDoListRepositoryAdoNet;
    private readonly IMapper _mapper;
    private readonly IValidator<ToDoListCreateDto> _createDtoValidator;
    private readonly IValidator<ToDoListGetDto> _getDtoValidator;
    private readonly string _connectionString;

    public ToDoListService(IToDoListRepositoryAdoNet toDoListRepositoryAdoNet, IMapper mapper, IValidator<ToDoListCreateDto> validator, IValidator<ToDoListGetDto> getDtoValidator)
    {
        _toDoListRepositoryAdoNet = toDoListRepositoryAdoNet;
        _mapper = mapper;
        _createDtoValidator = validator;
        _getDtoValidator = getDtoValidator;
    }

    public async Task DeleteToDoItemAsync(long toDoItemId)
    {
        await _toDoListRepositoryAdoNet.DeleteToDoItemAsync(toDoItemId);
    }

    public Task<long> InsertToDoItem(ToDoListCreateDto toDoItem)
    {
        var resOfValidator = _createDtoValidator.Validate(toDoItem);
        if (!resOfValidator.IsValid)
        {
            throw new Exception("ToDoList is not valid");
        }
        var todolistEntity = _mapper.Map<ToDoItem>(toDoItem);
        todolistEntity.CreatedAt = DateTime.UtcNow;
        return _toDoListRepositoryAdoNet.InsertToDoItem(todolistEntity);
    }

    public async Task<ICollection<ToDoListGetDto>> SelectAllToDoItemsAsync(int skip, int take)
    {
        var res = await _toDoListRepositoryAdoNet.SelectAllToDoItemsAsync(skip, take);
        return (List<ToDoListGetDto>)res.Select(tdl => _mapper.Map<ToDoListGetDto>(tdl)).ToList();
    }

    public async Task<ICollection<ToDoListGetDto>> SelectByDueDateAsync(DateTime selectedDate)
    {
        var lists = await _toDoListRepositoryAdoNet.SelectByDueDateAsync(selectedDate);

        var res = lists.Select(x => _mapper.Map<ToDoListGetDto>(x)).ToList();
        return res;
    }

    public async Task<ICollection<ToDoListGetDto>> SelectCompletedAsync(int skip, int take)
    {
        var res = await _toDoListRepositoryAdoNet.SelectCompletedAsync(skip, take);
        return (List<ToDoListGetDto>)res.Select(tdl => _mapper.Map<ToDoListGetDto>(tdl)).ToList();
    }

    public async Task<ICollection<ToDoListGetDto>> SelectIncompleteAsync(int skip, int take)
    {
        var res = await _toDoListRepositoryAdoNet.SelectIncompleteAsync(skip, take);
        return (List<ToDoListGetDto>)res.Select(tdl => _mapper.Map<ToDoListGetDto>(tdl)).ToList();
    }

    public async Task<ToDoListGetDto> SelectToDoItemByIdAsync(long toDoItemId)
    {
        return _mapper.Map<ToDoListGetDto>(await _toDoListRepositoryAdoNet.SelectToDoItemByIdAsync(toDoItemId));
    }

    public async Task UpdateToDoItemAsync(ToDoListGetDto toDoItem)
    {
        var resOfValidator = _getDtoValidator.Validate(toDoItem);
        if (!resOfValidator.IsValid)
        {
            throw new Exception("ToDoList is not valid");
        }
        var todolistEntity = _mapper.Map<ToDoItem>(toDoItem);
        await _toDoListRepositoryAdoNet.UpdateToDoItemAsync(todolistEntity);
    }
}
