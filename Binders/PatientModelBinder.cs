using Microsoft.AspNetCore.Mvc.ModelBinding;
using MedMinder_Api.Data;

namespace MedMinder_Api.Binders
{
    public class PatientModelBinder : IModelBinder
    {
        private readonly AppDbContext _context;

        public PatientModelBinder(AppDbContext context)
        {
            _context = context;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                Console.WriteLine("--> Binding Context is Null");
                throw new ArgumentNullException(nameof(bindingContext));
            }
           

            var modelName = bindingContext.ModelName;

            Console.WriteLine($"--> modelName: {modelName}");

            // Try to fetch the value of the argument by name
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                Console.WriteLine("--> ValueProviderResult is none");
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            // Check if the argument value is null or empty
            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            if (!int.TryParse(value, out var id))
            {
                // Non-integer arguments result in model state errors
                bindingContext.ModelState.TryAddModelError(
                    modelName, "Patient Id must be an integer.");

                return Task.CompletedTask;
            }

            // Model will be null if not found, including for
            // out of range id values (0, -3, etc.)
            var model = _context.Patients.Find(id);
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}