using System;
using StructureMap;

namespace PresentationGenerator.Core.DependencyResolution
{
    //public class StructureMapModelBinderProvider : IModelBinderProvider
    //{
    //    private readonly IContainer container;

    //    public StructureMapModelBinderProvider(IContainer container)
    //    {
    //        this.container = container;
    //    }

    //    public IModelBinder GetBinder(Type modelType)
    //    {
    //        var mappings = container.GetInstance<ModelBinderMappingDictionary>();
    //        if (mappings != null && mappings.ContainsKey(modelType))
    //            return container.GetInstance(mappings[modelType]) as IModelBinder;

    //        return null;
    //    }
    //}
}