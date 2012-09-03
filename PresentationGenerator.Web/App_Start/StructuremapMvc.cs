using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using PresentationGenerator.Core.DependencyResolution;
using PresentationGenerator.Core.Web;
using StructureMap;
using StructureMap.ServiceLocatorAdapter;

[assembly: WebActivator.PreApplicationStartMethod(typeof(PresentationGenerator.Web.App_Start.StructuremapMvc), "Start")]

namespace PresentationGenerator.Web.App_Start {
    public static class StructuremapMvc {
        public static void Start()
        {
            var container = (IContainer) Bootstrapper.Startup("PresentationDatabase");
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
            ModelBinders.Binders.DefaultBinder = new GenericBinderResolver();



			// this override is needed because WebAPI is not using DependencyResolver to build controllers 
			// eGlobalConfiguration.Configuration.ServiceResolver.SetResolver(new StructureMapServiceLocator(container));
        }
    }
}