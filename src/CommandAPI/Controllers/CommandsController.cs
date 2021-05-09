using CommandAPI.Data;
using CommandAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CommandAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CommandsController:ControllerBase
  {
    private readonly ICommandAPIRepo _repostiory;

    public CommandsController(ICommandAPIRepo repostiory)
    {
      this._repostiory = repostiory;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Command>> Get()
    {
      var commandItems = _repostiory.GetAllCommands();
      return Ok(commandItems);
    }

    [HttpGet("{id}")]
    public ActionResult<Command> GetCommandById(int id){
      var commandItem = _repostiory.GetCommandById(id);
      if(commandItem == null){
        return NotFound();
      }
      return Ok(commandItem);
    }
  }
}