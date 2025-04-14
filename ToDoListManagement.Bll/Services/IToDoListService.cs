using ToDoListManagement.Bll.DTOs;
using ToDoListManagement.Dal.Entity;

namespace ToDoListManagement.Bll.Services;

public interface IToDoListService
{
    public Task<long> InsertToDoItem(ToDoListCreateDto toDoItem);
    public Task DeleteToDoItemAsync(long toDoItemId);
    public Task UpdateToDoItemAsync(ToDoListGetDto toDoItem);
    public Task<ICollection<ToDoListGetDto>> SelectAllToDoItemsAsync(int skip, int take);
    public Task<ToDoListGetDto> SelectToDoItemByIdAsync(long toDoItemId);
    public Task<ICollection<ToDoListGetDto>> SelectByDueDateAsync(DateTime selectedDate);
    public Task<ICollection<ToDoListGetDto>> SelectCompletedAsync(int skip, int take);
    public Task<ICollection<ToDoListGetDto>> SelectIncompleteAsync(int skip, int take);
}