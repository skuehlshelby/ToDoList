using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ToDoList
{
    internal sealed class Serializer
    {
        public void Serialize(string filepath, ICollection<IToDo> todos)
        {
            XElement todoList = new XElement("To-Do-List");

            foreach (var todo in todos)
            {
                todoList.Add(new XElement("ToDo", 
                    new XElement(nameof(IToDo.Completed), XmlConvert.ToString(todo.Completed)),
                    new XElement(nameof(IToDo.Summary), todo.Summary),
                    new XElement(nameof(IToDo.Details), todo.Details),
                    new XElement(nameof(IToDo.Created), XmlConvert.ToString(todo.Created, XmlDateTimeSerializationMode.Local))));
            }

            todoList.Descendants().Where(d => d.IsEmpty || string.IsNullOrWhiteSpace(d.Value)).Remove();

            DirectoryInfo parent = Directory.GetParent(filepath);

            if (!parent.Exists)
            {
                parent.Create();
            }

            todoList.Save(filepath, SaveOptions.None);
        }
    }
}
