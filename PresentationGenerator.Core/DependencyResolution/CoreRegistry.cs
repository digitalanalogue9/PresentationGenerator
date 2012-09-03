using System;
using System.Globalization;
using PresentationGenerator.Core.CommandHandlers;
using PresentationGenerator.Core.Conventions;
using PresentationGenerator.Core.Documents;
using PresentationGenerator.Core.Entities;
using PresentationGenerator.Core.Repositories;
using PresentationGenerator.Core.Security;
using PresentationGenerator.Core.Services;
using PresentationGenerator.Core.Utility;
using PresentationGenerator.Core.Utility.Voodoo;
using PresentationGenerator.Core.Views.Factories;
using PresentationGenerator.Core.Web;
using StructureMap.Configuration.DSL;
using Raven.Client;
using FluentValidation;

namespace PresentationGenerator.Core.DependencyResolution
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry(IDocumentStore presentationdocumentStore)
        {

            For<IFormatProvider>().Use(c => CultureInfo.CurrentCulture);
            Scan(s =>
            {
                s.AssembliesFromApplicationBaseDirectory(x => x.FullName.StartsWith("PresentationGenerator"));
                s.With(new RegisterGenericTypesOfInterface(typeof(IViewFactory<,>)));
                s.With(new RegisterGenericTypesOfInterface(typeof(ICommandHandler<>)));
                s.With(new RegisterGenericTypesOfInterface(typeof(IValidator<>)));
                s.With(new RegisterGenericTypesOfInterface(typeof(IModelBinder<>)));
                s.With(new RegisterGenericTypesOfInterface(typeof(IPagedList<>)));
                //s.With(new RegisterGenericTypesOfInterface(typeof(IPagedSearch<,>)));
                //s.AddAllTypesOf(typeof(ISearchHelper<,>));
                s.ExcludeNamespaceContainingType<IProjectionExpression>();
                s.ExcludeType<ILogger>();
                s.ExcludeType<ISearchHelper>();
                s.ExcludeType<IPresentationHelper>();
                s.ExcludeType<IPresentationRepository>();
                s.With(new RegisterFirstInstanceOfInterface());


            });

            For<ILogEventContext>().Use<LogEventContext>();
            For<ILogger>().Use<NLogLogger>().Ctor<string>().Is(s => s.BuildStack.Root.RequestedType.FullName);
            // First, grab the current stackTrace.
            //var stackTrace = new System.Diagnostics.StackTrace().GetFrames();
            //For<ILogger>().AlwaysUnique().                

            //    Use<NLogLogger>()
            //    .Ctor<string>("currentClassName").Is
            //    (stackTrace.Length >= 2 ?
            //        stackTrace[1].GetMethod().DeclaringType.FullName :
            //        stackTrace[0].GetMethod().DeclaringType.FullName); 

            For<IAuthenticationService>().Use<AuthenticationService>();
            For<IUserService>().Use<UserService>();
            For<IDocumentStore>().Singleton().Use(presentationdocumentStore);
            For<IDocumentStore>().Singleton().Add(presentationdocumentStore).Named(Constants.Presentations);

            For<IPresentationRepository>().Use<PresentationRepository>()
                .Ctor<IDocumentStore>().Is(i => i.GetInstance<IDocumentStore>(Constants.Presentations));

            For<IEntityRepository<Presentation, PresentationDocument>>()
                .Use<PresentationRepository>()
                .Ctor<IDocumentStore>().Is(i => i.GetInstance<IDocumentStore>(Constants.Presentations));


            For<IPresentationHelper>().Use<PresentationSearchHelper>()
                .Ctor<IDocumentStore>().Is(i => i.GetInstance<IDocumentStore>(Constants.Presentations));
            //For<IDocumentSession>()
            //    .HybridHttpOrThreadLocalScoped()
            //    .Use(x =>
            //    {
            //        var store = x.GetInstance<IDocumentStore>();
            //        return store.OpenSession();
            //    });
        }
    }
}
