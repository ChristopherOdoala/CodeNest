using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodosController : ControllerBase
    {
        private ITodoService _todoService;

        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost("AddTodo")]
        public IActionResult AddTodo([FromBody]TodoDTO model)
        {
            if (model is null)
                return Ok("Model cannot be null");
            if (model.UserId == 0)
                return Ok("User id cannot be null");
            if (string.IsNullOrEmpty(model.Todo))
                return Ok("Please enter Task Name");

            try
            {
                var res = _todoService.AddTodo(model);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }

            return Ok("Task Successfully Added");
        }

        [HttpGet("GetAllTodos")]
        public IActionResult GetAllTodos()
        {
            var res = _todoService.GetAllTodos();
            return Ok(res);
        }

        [HttpGet("GetTodoById")]
        public IActionResult GetTodoById(int todoId)
        {
            if (todoId == 0)
                return Ok("Todo Id cannot be null");
            var res = _todoService.GetTodoById(todoId);
            return Ok(res);
        }

        [HttpGet("GetUserTodos")]
        public IActionResult GetUserTodos(int userId)
        {
            if (userId == 0)
                return Ok("User Id cannot be null");
            var res = _todoService.GetUserTodos(userId);
            return Ok(res);
        }

        [HttpPost("UpdateTodoToFinished")]
        public IActionResult UpdateTodoToFinished(int todoId)
        {
            if (todoId == 0)
                Ok("Todo Id cannot be null");
            var res = _todoService.UpdateTodoToFinished(todoId);
            if (res != null)
                return Ok(res);
            return Ok("Updated Successfully");
        }
    }
}