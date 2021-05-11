using CommandAPI.Data;
using CommandAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using CommandAPI.Dtos;

namespace CommandAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CommandsController : ControllerBase
  {
    private readonly ICommandAPIRepo _repostiory;
    private readonly IMapper _mapper;

    public CommandsController(ICommandAPIRepo repostiory, IMapper mapper)
    {
      this._mapper = mapper;
      this._repostiory = repostiory;
    }
    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> Get()
    {
      var commandItems = _repostiory.GetAllCommands();
      return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
    }

    [HttpGet("{id}", Name="GetCommandById")]
    public ActionResult<CommandReadDto> GetCommandById(int id)
    {
      var commandItem = _repostiory.GetCommandById(id);
      if (commandItem == null)
      {
        return NotFound();
      }
      return Ok(_mapper.Map<CommandReadDto>(commandItem));
    }

    [HttpPost]
    public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto){
      var commandModel = _mapper.Map<Command>(commandCreateDto);
      _repostiory.CreateCommand(commandModel);
      _repostiory.SaveChanges();
      var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
      return CreatedAtRoute(nameof(GetCommandById),
      new {Id = commandReadDto.Id}, commandReadDto);
    }
  }
}