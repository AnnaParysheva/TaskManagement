using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.DAL
{
    public class Tasks
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
    }
}
