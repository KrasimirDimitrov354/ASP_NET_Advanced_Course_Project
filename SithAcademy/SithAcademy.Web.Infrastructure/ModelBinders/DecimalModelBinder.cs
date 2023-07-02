namespace SithAcademy.Web.Infrastructure.ModelBinders;

using System.Globalization;

using Microsoft.AspNetCore.Mvc.ModelBinding;

/// <summary>
/// This is a custom model binder that resolves potential issues caused by the regional settings of the end user.
/// </summary>
public class DecimalModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext? bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueResult != ValueProviderResult.None && !string.IsNullOrWhiteSpace(valueResult.FirstValue))
        {
            decimal parsedValue = 0m;
            bool binderSucceeded = false;

            try
            {
                string formDecValue = valueResult.FirstValue;
                formDecValue = formDecValue.Replace(",",CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                formDecValue = formDecValue.Replace(".",CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

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
