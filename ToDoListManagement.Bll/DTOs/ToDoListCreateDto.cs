namespace ToDoListManagement.Bll.DTOs;

public class ToDoListCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
}
