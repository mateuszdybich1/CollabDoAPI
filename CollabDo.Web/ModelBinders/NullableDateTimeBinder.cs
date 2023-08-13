using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CollabDo.Web.ModelBinders
{
    internal class NullableDateTimeBinder : DateTimeBinder
    {
        protected override ModelBindingResult ToResult(long ms)
        {
            DateTime? dateTime = FromUnixTimeMilliseconds(ms);
            return ModelBindingResult.Success(dateTime);
        }
    }
}