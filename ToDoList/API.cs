using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public static class API
    {
        public static IToDo CreateEmptyToDo()
        {
            return new ToDo();
        }

        public static IToDo CreateToDo(Summary summary)
        {
            return new ToDo() { Summary = summary };
        }

        public static ICollection<IToDo> CreateToDos(params Summary[] summaries)
        {
            return summaries.Select(summary => new ToDo() { Summary = summary }).ToArray();
        }

        public static IBindableToDo CreateBindableToDo()
        {
            throw new NotImplementedException();
        }

        public static IValidatableToDo CreateValidatableToDo()
        {
            throw new NotImplementedException();
        }

        public static void Serialize(string filepath, IToDo todo)
        {
            
            throw new NotImplementedException();
        }

        public static void Serialize(string filepath, ICollection<IToDo> todos)
        {
            new Serializer().Serialize(filepath, todos);
        }

        public static ICollection<IToDo> Deserialize(string filepath)
        {
            throw new NotImplementedException();
        }
    }
}
