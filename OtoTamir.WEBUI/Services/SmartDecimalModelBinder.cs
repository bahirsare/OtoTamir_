using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace OtoTamir.WEBUI.Services
{
    public class SmartDecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None) return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
            var valueAsString = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(valueAsString)) return Task.CompletedTask;

            
            valueAsString = valueAsString.Replace(",", ".");

            if (decimal.TryParse(valueAsString, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            else
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Geçersiz tutar.");
            }

            return Task.CompletedTask;
        }
    }

    public class SmartDecimalModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(decimal) || context.Metadata.ModelType == typeof(decimal?))
            {
                return new SmartDecimalModelBinder();
            }
            return null;
        }
    }
}
