using api.Interfaces;
using api.Models;
using api.Mappers;
using api.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.Todos;

namespace api.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class TodoController : ControllerBase 
{
    private readonly ITodoRepository _todoRepository;
    private readonly IUserRepository _userRepository;
    public TodoController(ITodoRepository todoRepository, IUserRepository userRepository)
    {
        _todoRepository = todoRepository;
        _userRepository = userRepository;   
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] TodoQueryObject query)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var todos = await _todoRepository.GetAllAsync(query);

        var todoDto = todos.Select(p => p.ToTodoDto());

        return Ok(todoDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var todo = await _todoRepository.GetByIdAsync(id);

        if (todo == null)
            return NotFound();

        return Ok(todo.ToTodoDto());
    }

    [HttpPost]
    [Route("{userId:int}")]
    public async Task<IActionResult> Create([FromRoute] int userId, [FromBody] CreateTodoRequestDto todoRequestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await _userRepository.UserExists(userId))
            return BadRequest("User does not exist!");

        var todoModel = todoRequestDto.ToTodoFromCreate(userId);
        await _todoRepository.CreateAsync(todoModel);
        return CreatedAtAction(nameof(GetById), new { id = todoModel.Id }, todoModel.ToTodoDto());
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTodoRequestDto todoRequestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var todo = await _todoRepository.UpdateAsync(id, todoRequestDto.ToTodoFromUpdate());

        if (todo == null)
            return NotFound();

        return Ok(todo.ToTodoDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var todoModel = await _todoRepository.DeleteAsync(id);

        if (todoModel == null)
            return NotFound();

        return NoContent();
    }
}