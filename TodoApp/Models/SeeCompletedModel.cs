using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Models
{
    public class SeeCompletedModel
    {
        public List<TodoViewModel> TodoModels { get; set; }

        public SeeCompletedModel(List<TodoViewModel> todoModels)
        {
            TodoModels = todoModels;
        }
    }
}