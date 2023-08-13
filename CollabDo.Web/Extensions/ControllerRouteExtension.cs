using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CollabDo.Web.Extensions
{
    public class ControllerRouteExtension : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var selectorModel = controller.Selectors.FirstOrDefault();
            if (selectorModel != null)
            {
                var controllerName = controller.ControllerName;
                var actionName = selectorModel.AttributeRouteModel.Template;
                selectorModel.AttributeRouteModel.Template = $"{actionName}/{controllerName}";
            }
        }
    }
}
