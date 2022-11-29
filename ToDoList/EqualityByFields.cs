using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ToDo
{
    internal class EqualityByFields : EqualityComparer<IToDo>
    {
        public override bool Equals(IToDo x, IToDo y)
        {
            if (x is null && y is null)
                return true;
            else if (x is null || y is null)
                return false;
            else if (ReferenceEquals(x, y))
                return true;
            else
            {
                return x.Completed == y.Completed &&
                    x.Created == y.Created &&
                    x.Details == y.Details &&
                    x.Summary == y.Summary;
            }
        }

        public override int GetHashCode([DisallowNull] IToDo obj)
        {
            return HashCode.Combine(obj.Completed, obj.Created, obj.Details, obj.Summary);
        }
    }
}
