using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace HouseRentingSystem.Web.Infrastructure.ModelBinders
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // Order matters!
            // may be null
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            ValueProviderResult valueResult =
                bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueResult != ValueProviderResult.None && !string.IsNullOrWhiteSpace(valueResult.FirstValue)) //we get only first value otherwise it will become too difficult
            {
                decimal parsedValue = 0m;
                bool binderSucceeded = false;

                try
                {
                    string formDecValue = valueResult.FirstValue;
                    // in orderr to make it work in every country
                    formDecValue = formDecValue.Replace(",", 
                        CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    formDecValue = formDecValue.Replace(".", 
                        CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    //We use CONVERT not PARSE, because parse will break the app. 
                    //If we dont work with console, we use convert
                    parsedValue = Convert.ToDecimal(formDecValue);
                    binderSucceeded = true;
                }
                catch (FormatException fe)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);                    
                }

                if (binderSucceeded) 
                {
                    bindingContext.Result = ModelBindingResult.Success(parsedValue);
                }
            }

            return Task.CompletedTask;
        }
    }
}
