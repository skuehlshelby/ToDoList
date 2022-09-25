using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ToDoList
{
    internal class BindableToDo : IBindableToDo, IWrapper<IToDo>
    {
        private readonly IToDo todo;

        public BindableToDo(IToDo todo)
        {
            this.todo=todo;
        }

        public IToDo GetWrappedObject() => todo;

        public bool Completed { get => todo.Completed; set => SetAndNotify(todo, value); }
        public Summary Summary { get => todo.Summary; set => SetAndNotify(todo, value); }
        public Details Details { get => todo.Details; set => SetAndNotify(todo, value); }
        public DateTime Created { get => todo.Created; set => SetAndNotify(todo, value); }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SetAndNotify(IToDo item, object value, [CallerMemberName] string propertyName = null)
        {
            PropertyInfo property = item.GetType().GetProperty(propertyName);

            if (!property.GetValue(item).Equals(value))
            {
                property.SetValue(item, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
