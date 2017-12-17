using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Models
{
    public class AddNewModel
    {
        public string Labels { get; set; }
        public DateTime DateDue { get; set; }

        [Required]
        public string Text { get; set; }
    }
}