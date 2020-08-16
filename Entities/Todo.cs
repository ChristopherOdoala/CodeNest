using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Entities
{
    public class Todo
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public bool IsComplete { get; set; }

        //User
        public int UserId { get; set; }
        public User User { get; set; }

        public static implicit operator Todo(TodoDTO model)
        {
            return model == null ? null : new Todo
            {
                UserId = model.UserId,
                Task = model.Todo,
                IsComplete = model.IsComplete
            };
        }
    }
}
