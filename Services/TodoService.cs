using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface ITodoService
    {
        IQueryable<Todo> GetAllTodos();
        Todo GetTodoById(int todoId);
        Todo AddTodo(TodoDTO model);
        Todo UpdateTodo(Todo model);
        void DeleteTodo(Todo model);
        List<TodoDTO> GetUserTodos(int userId);
        List<TodoDTO> GetUnfinishedTodos(int userId);
        List<TodoDTO> GetFinishedTodos(int userId);
        List<ValidationResult> UpdateTodoToFinished(int todoId);
    }

    public class TodoService : ITodoService
    {
        private DataContext _context;

        public TodoService(DataContext context)
        {
            _context = context;
        }

        public Todo AddTodo(TodoDTO model)
        {
            _context.Todos.Add(model);
            _context.SaveChanges();
            return model;
        }

        public void DeleteTodo(Todo model)
        {
            _context.Todos.Remove(model);
            _context.SaveChanges();
        }

        public List<TodoDTO> GetFinishedTodos(int userId)
        {
            throw new NotImplementedException();
        }

        public Todo GetTodoById(int todoId)
        {
            return GetAllTodos().Where(x => x.Id == todoId).FirstOrDefault();
        }

        public IQueryable<Todo> GetAllTodos()
        {
            return _context.Todos;
        }

        public List<TodoDTO> GetUnfinishedTodos(int userId)
        {
            throw new NotImplementedException();
        }

        public List<TodoDTO> GetUserTodos(int userId)
        {
            var res = GetAllTodos().Where(x => x.UserId == userId).ToList();
            var data = res.Select(x => (TodoDTO)x).ToList();
            return data;
        }

        public Todo UpdateTodo(Todo model)
        {
            _context.Todos.Update(model);
            _context.SaveChanges();
            return model;
        }

        public List<ValidationResult> UpdateTodoToFinished(int todoId)
        {
            var errorList = new List<ValidationResult> { };

            try
            {
                var todo = GetTodoById(todoId);
                todo.IsComplete = true;
                UpdateTodo(todo);
            }
            catch(Exception ex)
            {
                errorList.Add(new ValidationResult(ex.Message));
                return errorList;
            }

            return errorList;
        }
    }
}
