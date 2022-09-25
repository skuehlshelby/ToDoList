using System.ComponentModel;

namespace ToDoList
{
    public interface IBindableToDo : IToDo, INotifyPropertyChanged
    {
    }
}