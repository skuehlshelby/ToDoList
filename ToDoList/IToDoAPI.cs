using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDo
{
    public interface IToDoApi
    {
        IToDo CreateToDo();
        IToDo CreateToDo(Summary summary);

        IComparer<IToDo> CompletedComparer { get; }
        IComparer<IToDo> DateCreatedComparer { get; }
        IComparer<IToDo> SummaryComparer { get; }
        IComparer<IToDo> CompareByMultipleFields(params IComparer<IToDo>[] comparers);

        IEqualityComparer<IToDo> GetRecordStyleEqualityComparer();

        void AddValidationRule(Func<ValidationContext, ValidationResult> rule);

        void Serialize(string filepath, IToDo todo);
        void Serialize(string filepath, ICollection<IToDo> todos);

        ICollection<IToDo> Deserialize(string filepath);
    }
}
