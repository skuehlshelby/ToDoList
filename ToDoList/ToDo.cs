using System;
using System.Text;

namespace ToDoList
{
    internal class ToDo : IToDo
    {
        public bool Completed { get; set; } = false;
        public Summary Summary { get; set; } = new Summary();
        public Details Details { get; set; } = new Details();
        public DateTime Created { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return $"{(Completed ? "(Completed)" : "(Incomplete)")} {Summary}".Trim();
        }
    }
}