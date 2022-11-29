using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ToDo
{
    internal sealed class ToDo : IToDo, ICloneable
    {
        public static readonly ICollection<Func<ValidationContext, ValidationResult>> ValidationRules = new LinkedList<Func<ValidationContext, ValidationResult>>();
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly BindableProperty<bool> completed = new(false);
        private readonly BindableProperty<Summary> summary = new(string.Empty);
        private readonly BindableProperty<Details> details = new(string.Empty);
        private readonly BindableProperty<DateTime> created = new(DateTime.Now);

        public ToDo()
        {
            completed.PropertyChanged += OnPropertyChanged;
            summary.PropertyChanged += OnPropertyChanged;
            details.PropertyChanged += OnPropertyChanged;
            created.PropertyChanged += OnPropertyChanged;
        }

        public bool Completed { get => completed.Get(); set => completed.Set(value); }
        public Summary Summary { get => summary.Get(); set => summary.Set(value); }
        public Details Details { get => details.Get(); set => details.Set(value); }
        public DateTime Created { get => created.Get(); set => created.Set(value); }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => ValidationRules.Select(rule => rule.Invoke(validationContext));

        public override string ToString() => $"{(Completed ? "(Completed)" : "(Incomplete)")} {Summary}".Trim();

        public object Clone() => new ToDo() { Completed = Completed, Summary = Summary, Details = Details};
    }
}
