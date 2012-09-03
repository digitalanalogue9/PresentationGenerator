using System;
using System.Web.Mvc;
using StructureMap;

namespace PresentationGenerator.Core.Web
{
    public class GenericBinderResolver : DefaultModelBinder
    {
        private static readonly Type BinderType = typeof(IModelBinder<>);

        public override object BindModel(ControllerContext controllerContext,
                                         ModelBindingContext bindingContext)
        {
            Type genericBinderType = BinderType.MakeGenericType(bindingContext.ModelType);

            var binder = ObjectFactory.TryGetInstance(genericBinderType) as IModelBinder;
            if (null != binder) return binder.BindModel(controllerContext, bindingContext);

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}