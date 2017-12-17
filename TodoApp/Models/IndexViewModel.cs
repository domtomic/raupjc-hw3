using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Models
{
    public class IndexViewModel
    {
        public List<TodoModel> TodoModels { get; set; }

        public IndexViewModel(List<TodoModel> todoModels)
        {
            TodoModels = todoModels;
        }
    }
}
