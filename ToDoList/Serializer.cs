using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ToDo
{
    internal static class Serializer
    {
        public static void Serialize(string filepath, ICollection<IToDo> todos)
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

            todoList.Descendants()
                    .Where(d => string.IsNullOrWhiteSpace(d.Value))
                    .Remove();

            Directory.CreateDirectory(Path.GetDirectoryName(filepath));

            todoList.Save(filepath, SaveOptions.None);
        }

        public static ICollection<ToDo> Deserialize(string filepath)
        {
            ICollection<ToDo> todos = new LinkedList<ToDo>();

            if (File.Exists(filepath))
            {
                XDocument document = XDocument.Load(filepath);

                foreach (var todo in document.Descendants("ToDo"))
                {
                    ToDo item = new ToDo();

                    foreach (var element in todo.Elements())
                    {
                        switch (element.Name.ToString())
                        {
                            case nameof(IToDo.Completed):
                                item.Completed = XmlConvert.ToBoolean(element.Value);
                                break;
                            case nameof(IToDo.Summary):
                                item.Summary = element.Value;
                                break;
                            case nameof(IToDo.Details):
                                item.Details = element.Value;
                                break;
                            case nameof(IToDo.Created):
                                item.Created = XmlConvert.ToDateTime(element.Value, XmlDateTimeSerializationMode.Local);
                                break;
                            default:
                                break;
                        }
                    }

                    todos.Add(item);
                }
            }            

            return todos;
        }
    }
}
