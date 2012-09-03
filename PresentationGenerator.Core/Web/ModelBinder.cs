using System.Web.Mvc;

namespace PresentationGenerator.Core.Web
{
    public abstract class ModelBinder<T> : IModelBinder<T>
    {
        public abstract T BindModel(ControllerContext controllerContext,
                                    ModelBindingContext bindingContext);

        object IModelBinder.BindModel(ControllerContext controllerContext,
                                      ModelBindingContext bindingContext)
        {
            return BindModel(controllerContext, bindingContext);
        }
    }
}