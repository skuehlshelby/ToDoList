using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using ToDo;
using ToDoUpdate = System.Collections.Specialized.NotifyCollectionChangedEventArgs;
using ChangeAction = System.Collections.Specialized.NotifyCollectionChangedAction;
using System.ComponentModel;
using Application.Sorting;

namespace Application
{
    public class Application : IApplication
    {
        public event NotifyCollectionChangedEventHandler ToDosChanged;

        private readonly UniqueCollection<IToDo> todos = new();
        private readonly ISet<IComparer<IToDo>> comparers = new HashSet<IComparer<IToDo>>();

        private static string GetDataLocation()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ToDoList\\ToDos.xml");
        }

        public string GetApplicationName() => "To-Do-List";

        public void Startup()
        {
            foreach (var todo in ToDoApi.Deserialize(GetDataLocation()))
            {
                AddToDo(todo);
            }
        }

        public void Shutdown() => ToDoApi.Serialize(GetDataLocation(), todos);

        public void AddToDo() => AddToDo(ToDoApi.CreateToDo());

        public void AddToDo(IToDo item)
        {
            if (todos.Add(item))
            {
                item.PropertyChanged += OnToDoItemChanged;
                ToDosChanged?.Invoke(this, new ToDoUpdate(ChangeAction.Add, item));
                ApplySortingRules();
            }
        }

        public void RemoveToDo(IToDo item)
        {
            if (todos.Remove(item))
            {
                item.PropertyChanged -= OnToDoItemChanged;
                ToDosChanged?.Invoke(this, new ToDoUpdate(ChangeAction.Remove, item));
            }
        }

        public void RemoveCompletedToDos()
        {
            IToDo[] completed = todos.Where(todo => todo.Completed).ToArray();

            if (completed.Any())
            {
                foreach (IToDo todo in completed)
                {
                    todo.PropertyChanged -= OnToDoItemChanged;
                    todos.Remove(todo);
                }

                ToDosChanged?.Invoke(this, new ToDoUpdate(ChangeAction.Reset));
            }
        }

        public ICollection<IToDo> GetToDos() => todos;

        public void SortByCompleted()
        {
            AddSortingRule(ToDoApi.CompletedComparer);
        }

        public void SortByDateCreated()
        {
            AddSortingRule(ToDoApi.DateCreatedComparer);
        }

        public void SortBySummary()
        {
            AddSortingRule(ToDoApi.SummaryComparer);
        }

        private void AddSortingRule(IComparer<IToDo> rule)
        {
            if (comparers.Add(rule))
            {
                ApplySortingRules();
            }
        }

        public void StopSortingByCompleted()
        {
            RemoveSortingRule(ToDoApi.CompletedComparer);
        }

        public void StopSortingByDateCreated()
        {
            RemoveSortingRule(ToDoApi.DateCreatedComparer);
        }

        public void StopSortingBySummary()
        {
            RemoveSortingRule(ToDoApi.SummaryComparer);           
        }

        private void RemoveSortingRule(IComparer<IToDo> rule)
        {
            if (comparers.Remove(rule))
            {
                ApplySortingRules();
            }          
        }

        public bool IsSortingByCompleted() => comparers.Contains(ToDoApi.CompletedComparer);

        public bool IsSortingByDateCreated() => comparers.Contains(ToDoApi.DateCreatedComparer);

        public bool IsSortingBySummary() => comparers.Contains(ToDoApi.SummaryComparer);

        private void OnToDoItemChanged(object sender, PropertyChangedEventArgs e)
        {
            ApplySortingRules();
        }


        private void ApplySortingRules()
        {
            if (comparers.Any() && 1 < todos.Count)
            {
                IComparer<IToDo> comparer = ToDoApi.CompareByMultipleFields(comparers.ToArray());

                todos.Sort(new BubbleSort(), comparer);

                ToDosChanged?.Invoke(this, new ToDoUpdate(ChangeAction.Reset));
            }            
        }
    }
}
