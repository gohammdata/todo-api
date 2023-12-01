using TodoApi.Models;
using TodoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ToDoService _todoService;

    public TodoController(ToDoService todoService) =>
        _todoService = todoService;

    [HttpGet]
    public async Task<List<TodoItemDTO>> Get() =>
        await _todoService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<TodoItemDTO>> Get(string id)
    {
        var todo = await _todoService.GetAsync(id);

        if (todo is null)
        {
            return NotFound();
        }

        return todo;
    }

    [HttpPost]
    public async Task<IActionResult> Post(TodoItemDTO newToDo)
    {
        await _todoService.CreateAsync(newToDo);

        return CreatedAtAction(nameof(Get), new { id = newToDo.Id }, newToDo);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, TodoItemDTO updatedToDo)
    {
        var todo = await _todoService.GetAsync(id);

        if (todo is null)
        {
            return NotFound();
        }

        updatedToDo.Id = todo.Id;

        await _todoService.UpdateAsync(id, updatedToDo);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var todo = await _todoService.GetAsync(id);

        if (todo is null)
        {
            return NotFound();
        }

        await _todoService.RemoveAsync(id);

        return NoContent();
    }
}