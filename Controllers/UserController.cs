using api.Interfaces;
using api.Dtos;
using api.Helpers;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.Dtos.User;


namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] UserQueryObject query)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var users = await _userRepository.GetAllAsync(query);

        var UserDto = users.Select(p => p.ToUserDto());

        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userRepository.GetByIdAsync(id);

        if (user == null) 
            return NotFound();

        return Ok(user.ToUserDto());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserRequestDto userRequestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userModel = userRequestDto.ToUserFromCreateDto();
        await _userRepository.CreateAsync(userModel);
        return CreatedAtAction(nameof(GetById), new { id = userModel.Id }, userModel.ToUserDto());
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserRequestDto userRequestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userModel = await _userRepository.UpdateAsync(id, userRequestDto.ToUserFromUpdateDto());

        if (userModel == null)
            return NotFound();

        return Ok(userModel.ToUserDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var userModel = await _userRepository.DeleteAsync(id);

        if (userModel == null)
            return NotFound();

        return NoContent();
    }
}