using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class TodoItemDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; } = null;
        public bool? IsComplete { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
    }

    public class TodoItem : TodoItemDTO
    {
        public string? Secret { get; set; } = string.Empty;
    }
}