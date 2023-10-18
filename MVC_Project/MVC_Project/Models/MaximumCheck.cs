using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models
{
    public class MaximumCheck : ValidationAttribute
    {
        private readonly string minProperty;

        public string GetErrorMessage() =>
        $"Classic movies must have a release year no later than ?.";

        public MaximumCheck(string minProperty)
        {
            this.minProperty = minProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var minPropertyInfo = validationContext.ObjectType.GetProperty(minProperty);
            if (minPropertyInfo == null)
            {
                return new ValidationResult($"Unknown property: {minProperty}");
            }

            var minPropertyValue = (int?)minPropertyInfo.GetValue(validationContext.ObjectInstance);
            var maxAttendee = (int?)value;

            if (minPropertyValue.HasValue && maxAttendee.HasValue && maxAttendee.Value <= minPropertyValue.Value)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
