using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Dtos
{
    public class TodoDTO
    {
        public int UserId { get; set; }
        public string Todo { get; set; }
        public bool IsComplete { get; set; }
        public string FirstName { get; set; }

        public static implicit operator TodoDTO(Todo model)
        {
            return model == null ? null : new TodoDTO
            {
                IsComplete = model.IsComplete,
                Todo = model.Task,
                UserId = model.UserId,
                FirstName = model.User == null ? "" : model.User.FirstName
            };
        }
    }
}
