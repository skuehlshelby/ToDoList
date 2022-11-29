using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDo
{
    public interface IToDo : INotifyPropertyChanged, IValidatableObject
    {
        bool Completed { get; set; }

        Summary Summary { get; set; }

        Details Details { get; set; }

        DateTime Created { get; set; }
    }
}
