using ToDoListManagement.Dal.Entity;

namespace ToDoListManagement.Repository.Services;

public interface IToDoListRepositoryAdoNet
{
    public Task<long> InsertToDoItem(ToDoItem toDoItem);
    public Task DeleteToDoItemAsync(long toDoItemId);
    public Task UpdateToDoItemAsync(ToDoItem toDoItem);
    public Task<ICollection<ToDoItem>> SelectAllToDoItemsAsync(int skip, int take);
    public Task<ToDoItem> SelectToDoItemByIdAsync(long toDoItemId);
    public Task<ICollection<ToDoItem>> SelectByDueDateAsync(DateTime selectedDate);
    public Task<ICollection<ToDoItem>> SelectCompletedAsync(int skip, int take);
    public Task<ICollection<ToDoItem>> SelectIncompleteAsync(int skip, int take);
}