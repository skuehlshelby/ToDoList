using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public interface IToDo
    {
        bool Completed { get; set; }

        Summary Summary { get; set; }

        Details Details { get; set; }

        DateTime Created { get; set; }
    }
}
