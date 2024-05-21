using System.ComponentModel.DataAnnotations;

namespace WebAPI.Validation {
    public class UpdateAttribute : ValidationAttribute {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            var httpContext = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));
            var isUpdate = httpContext.HttpContext.Request.Path.Value.Contains("update", StringComparison.OrdinalIgnoreCase);
            if (isUpdate && value == null) {
                return new ValidationResult(ErrorMessage ?? "Field is required for update operations.");
            }

            return ValidationResult.Success;
        }
    }
}
