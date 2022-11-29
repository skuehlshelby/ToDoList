using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ToDo.Comparers;

namespace ToDo
{
    public class ToDoApi : IToDoApi
    {
        private readonly ICloneable prototype;

        public ToDoApi(Action<IToDo> defaultToDoConfig = null)
        {
            if (defaultToDoConfig != null)
            {
                ToDo todo = new();
                defaultToDoConfig.Invoke(todo);
                prototype = todo;
            }
            else
            {
                prototype = new ToDo();
            }
        }

        IToDo IToDoApi.CreateToDo() => (IToDo)prototype.Clone();

        IToDo IToDoApi.CreateToDo(Summary summary)
        {
            IToDo todo = (IToDo)prototype.Clone();
            todo.Summary = summary;
            return todo;
        }

        IComparer<IToDo> IToDoApi.CompletedComparer => new CompletedComparer();
        IComparer<IToDo> IToDoApi.DateCreatedComparer => new DateCreatedComparer();
        IComparer<IToDo> IToDoApi.SummaryComparer => new SummaryComparer();

        IComparer<IToDo> IToDoApi.CompareByMultipleFields(params IComparer<IToDo>[] comparers) => new MultiFieldComparer(comparers);

        IEqualityComparer<IToDo> IToDoApi.GetRecordStyleEqualityComparer() => new EqualityByFields();

        void IToDoApi.AddValidationRule(Func<ValidationContext, ValidationResult> rule) => ToDo.ValidationRules.Add(rule);

        void IToDoApi.Serialize(string filepath, IToDo todo) => Serializer.Serialize(filepath, new IToDo[] { todo });

        void IToDoApi.Serialize(string filepath, ICollection<IToDo> todos) => Serializer.Serialize(filepath, todos);

        ICollection<IToDo> IToDoApi.Deserialize(string filepath)
        {
            return Serializer.Deserialize(filepath)
                .OfType<IToDo>()
                .ToList();
        }

        private static readonly IToDoApi api = new ToDoApi(td => td.Summary = "Edit Me!");

        public static IToDo CreateToDo() => api.CreateToDo();

        public static IToDo CreateToDo(Summary summary) => api.CreateToDo(summary);

        public static void Serialize(string filepath, ICollection<IToDo> todos) => api.Serialize(filepath, todos);

        public static ICollection<IToDo> Deserialize(string filepath) => api.Deserialize(filepath);

        public static IComparer<IToDo> CompletedComparer => api.CompletedComparer;
        public static IComparer<IToDo> DateCreatedComparer => api.DateCreatedComparer;
        public static IComparer<IToDo> SummaryComparer => api.SummaryComparer;

        public static IComparer<IToDo> CompareByMultipleFields(params IComparer<IToDo>[] comparers) => api.CompareByMultipleFields(comparers);
    }
}
