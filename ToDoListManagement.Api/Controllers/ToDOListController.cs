using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListManagement.Bll.DTOs;
using ToDoListManagement.Bll.Services;

namespace ToDoListManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDOListController : ControllerBase
    {
        private readonly IToDoListService _toDoListService;
        private readonly ILogger<ToDOListController> _logger;
        private readonly TelegramHandler _telegramHandler;

        public ToDOListController(IToDoListService toDoListService, ILogger<ToDOListController> logger, TelegramHandler telegramHandler)
        {
            _toDoListService = toDoListService;
            _logger = logger;
            _telegramHandler = telegramHandler;
        }

        [HttpPost("InsertToDoItem")]
        public async Task<long> InsertToDoItem([FromBody] ToDoListCreateDto toDoItem)
        {
            return await _toDoListService.InsertToDoItem(toDoItem);
        }

        [HttpDelete("DeleteToDoItemAsync")]
        public async Task DeleteToDoItemAsync(long toDoItemId)
        {
            await _toDoListService.DeleteToDoItemAsync(toDoItemId);
        }

        [HttpPut("UpdateToDoItemAsync")]
        public async Task UpdateToDoItemAsync([FromBody] ToDoListGetDto toDoItem)
        {
            await _toDoListService.UpdateToDoItemAsync(toDoItem);
        }

        [HttpGet("SelectAllToDoItemsAsync")]
        public async Task<ICollection<ToDoListGetDto>> SelectAllToDoItemsAsync(int skip, int take)
        {
            await _telegramHandler.LogAsync($"SelectAllToDoItemsAsync method called with skip: {skip}, take: {take}, {DateTime.UtcNow}");
            _logger.LogInformation($"SelectAllToDoItemsAsync method called with skip: {skip}, take: {take}, {DateTime.UtcNow}");
            return await _toDoListService.SelectAllToDoItemsAsync(skip, take);
        }

        [HttpGet("SelectToDoItemByIdAsync")]
        public async Task<ToDoListGetDto> SelectToDoItemByIdAsync(long toDoItemId)
        {
            return await _toDoListService.SelectToDoItemByIdAsync(toDoItemId);
        }

        [HttpGet("SelectByDueDateAsync")]
        public async Task<ICollection<ToDoListGetDto>> SelectByDueDateAsync(DateTime selectedDate)
        {
            return await _toDoListService.SelectByDueDateAsync(selectedDate);
        }

        [HttpGet("SelectCompletedAsync")]
        public async Task<ICollection<ToDoListGetDto>> SelectCompletedAsync(int skip, int take)
        {
            return await _toDoListService.SelectCompletedAsync(skip, take);
        }

        [HttpGet("SelectIncompleteAsync")]
        public async Task<ICollection<ToDoListGetDto>> SelectIncompleteAsync(int skip, int take)
        {
            return await _toDoListService.SelectIncompleteAsync(skip, take);
        }
    }
}
