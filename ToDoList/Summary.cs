using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDo
{
    public struct Summary : IValidatableObject, IEquatable<Summary>
    {
        public const int MinLength = 0;
        public const int MaxLength = 4000;

        private readonly string value;

        public Summary()
        {
            value = string.Empty;
        }

        public Summary(string value)
        {
            this.value = value ?? string.Empty;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (value.Length < MinLength)
                yield return new ValidationResult($"Value '{value}' is too short.");

            if (value.Length > MaxLength)
                yield return new ValidationResult($"Value '{value}' is too long.");
        }

        public override bool Equals(object obj) => obj is Summary summary && Equals(summary);

        public bool Equals(Summary other) => value == other.value;

        public override int GetHashCode() => value.GetHashCode();

        public override string ToString() => value;

        public static implicit operator Summary(string value) => new(value);

        public static explicit operator string(Summary summary) => summary.value;

        public static bool operator ==(Summary left, Summary right) => left.Equals(right);

        public static bool operator !=(Summary left, Summary right) => !left.Equals(right);
    }
}
