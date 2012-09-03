using System;
using System.Linq;
using System.Threading;
using PresentationGenerator.Core.Indexes;
using Raven.Client;
using Raven.Client.Document;
using PresentationGenerator.Core.Utility;
using Raven.Client.Embedded;
using StructureMap;
using Raven.Client.Indexes;

namespace PresentationGenerator.Core.DependencyResolution
{
    public static class Bootstrapper
    {
        public static IContainer InMemoryStartup()
        {
            var presentationdocumentStore = new EmbeddableDocumentStore
            {
                RunInMemory = true,
            };

            presentationdocumentStore.Initialize();

            ObjectFactory.Initialize(config =>
            {
                config.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();

                });
                config.AddRegistry(new CoreRegistry(presentationdocumentStore));


            });

            ObjectFactory.AssertConfigurationIsValid();
            ObjectFactory.WhatDoIHave();
            WaitForIndexes(presentationdocumentStore);
            IndexCreation.CreateIndexes(typeof(Users_ByUsername).Assembly, presentationdocumentStore);
            WaitForIndexes(presentationdocumentStore);

            return ObjectFactory.Container;
        }


        public static IContainer Startup(string connectionstringname)
        {
            var presentationdocumentStore = new DocumentStore
                                    {
                                        ConnectionStringName = connectionstringname
                                    };




            presentationdocumentStore.Initialize();

            ObjectFactory.Initialize(config =>
            {
                config.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();

                                    });
                config.AddRegistry(new CoreRegistry(presentationdocumentStore));


            });

            ObjectFactory.AssertConfigurationIsValid();
            ObjectFactory.WhatDoIHave();
            IndexCreation.CreateIndexes(typeof(Users_ByUsername).Assembly, presentationdocumentStore);
            WaitForIndexes(presentationdocumentStore);

            return ObjectFactory.Container;
        }

        private static void WaitForIndexes(IDocumentStore documentStore)
        {
            var logger = ObjectFactory.GetInstance<ILogger>();
            logger.Debug("Stale Indexes");
            logger.Debug(string.Join(Environment.NewLine,
                                     documentStore.DatabaseCommands.GetStatistics().StaleIndexes));
            while (documentStore.DatabaseCommands.GetStatistics().StaleIndexes.Length > 0)
            {
                Thread.Sleep(300);
            }
        }

        public static void Shutdown(bool isWeb)
        {
            if (isWeb)
            {
                ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
            }
            else
            {
                foreach (var pluginType in ObjectFactory.Model.PluginTypes)
                {
                    if (pluginType.PluginType.ContainsGenericParameters)
                        continue;

                    var instances = ObjectFactory.GetAllInstances(pluginType.PluginType);
                    foreach (var instance in instances.OfType<IDisposable>())
                    {
                        (instance).Dispose();
                    }
                }
            }
        }
    }
}
