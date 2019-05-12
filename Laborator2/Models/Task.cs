using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator2.Models
{
    public enum TaskImportance
    {
        Low,
        Medium,
        Hight
    }

    public enum TaskState
    {
        Open,
        InProgress,
        Closed
    }

    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime Deadline { get; set; }

        [EnumDataType(typeof(TaskImportance))]
        public TaskImportance TaskImportance { get; set; }

        [EnumDataType(typeof(TaskState))]
        public TaskState TaskState { get; set; }

        public DateTime? DateClosed { get; set; }

        public List<Comment> Comments { get; set; }

    }
}
