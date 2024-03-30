using System.ComponentModel.DataAnnotations;

namespace Application.Validations
{
    public sealed class NoDefaultInt : RequiredAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is int intValue)
            {
                return intValue != default;
            }
            return false;
        }
    }
}