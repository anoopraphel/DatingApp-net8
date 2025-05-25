using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class UserController(DataContext context) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUser()
    {
        var user = await context.Users.ToListAsync();
        return user;
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        var user =await context.Users.FindAsync(id);
        if (user == null) return NotFound();
        return user;
    }
}
