using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CollabDo.Web.ModelBinders
{
    public class DateTimeBinder : IModelBinder
    {
        protected virtual ModelBindingResult ToResult(long ms)
        {
            DateTime dateTime = FromUnixTimeMilliseconds(ms);
            return ModelBindingResult.Success(dateTime);
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string modelName = bindingContext.ModelName;
            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            string rawValue = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(rawValue))
            {
                return Task.CompletedTask;
            }

            if (!long.TryParse(rawValue, out long unixMilliseconds))
            {
                bindingContext.ModelState.TryAddModelError(modelName, "Invalid Unix timestamp format.");
                return Task.CompletedTask;
            }

            bindingContext.Result = ToResult(unixMilliseconds);

            return Task.CompletedTask;
        }

        protected static DateTime FromUnixTimeMilliseconds(long ms)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(ms).UtcDateTime;
        }
    }
}