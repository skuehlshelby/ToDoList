using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDo
{
    public struct Details : IValidatableObject, IEquatable<Details>
    {
        public const int MinLength = 0;
        public const int MaxLength = 4000;

        private readonly string value;

        public Details()
        {
            value = string.Empty;
        }

        public Details(string value)
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

        public override bool Equals(object obj) => obj is Details details && Equals(details);

        public bool Equals(Details other) => value == other.value;

        public override int GetHashCode() => value.GetHashCode();

        public override string ToString() => value;

        public static implicit operator Details(string value) => new(value);

        public static explicit operator string(Details details) => details.value;

        public static bool operator ==(Details left, Details right) => left.Equals(right);

        public static bool operator !=(Details left, Details right) => !left.Equals(right);
    }
}