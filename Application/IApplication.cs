using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using ToDo;

namespace Application
{
    public interface IApplication
    {
        void Startup();

        void Shutdown();

        void AddToDo();

        void AddToDo(IToDo item);

        void RemoveToDo(IToDo item);

        void RemoveCompletedToDos();

        void SortByCompleted();

        void SortByDateCreated();

        void SortBySummary();

        void StopSortingByCompleted();

        void StopSortingByDateCreated();

        void StopSortingBySummary();

        bool IsSortingByCompleted();

        bool IsSortingByDateCreated();
        
        bool IsSortingBySummary();
        
        ICollection<IToDo> GetToDos();

        string GetApplicationName();

        event NotifyCollectionChangedEventHandler ToDosChanged;
    }
}