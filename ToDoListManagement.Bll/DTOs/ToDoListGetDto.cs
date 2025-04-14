namespace ToDoListManagement.Bll.DTOs;

public class ToDoListGetDto
{
    public long ToDoItemId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DueDate { get; set; }
}
