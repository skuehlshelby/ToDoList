using System.ComponentModel.DataAnnotations;

namespace ToDoList
{
    public interface IValidatableToDo : IToDo, IValidatableObject
    {
    }
}