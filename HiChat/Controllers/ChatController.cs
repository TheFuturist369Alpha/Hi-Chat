using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.ChatService;
using System;

namespace HiChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatServe _service;
        public ChatController(ChatServe service)
        {
            _service = service;
        }

        [HttpPost("/register")]
        public IActionResult Register(UserDTO user)
        {
            if (user == null)
            {
                return BadRequest("User name can't be null");
            }
            if(_service.AddUser(user.Name))
            return Ok();
            return BadRequest("Name already exists.");

            
            
        }

    }
}
