using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    public class TaskItemModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public string Status { get; set; } = "Pending";

        [DataType(DataType.DateTime)]
        public DateTime? DueDate { get; set; }
        public string Category { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserModel User { get; set; }
    }
}