using PresentationGenerator.Core.CommandHandlers;
using StructureMap;
using Raven.Client;

namespace PresentationGenerator.Core.CommandInvokers
{
    //public class CommandInvoker : ICommandInvoker
    //{
    //    private IContainer container;
    //    private IDocumentSession documentSession;

    //    public CommandInvoker(IContainer container, IDocumentSession documentSession)
    //    {
    //        this.container = container;
    //        this.documentSession = documentSession;
    //    }

    //    public void Execute<T>(T command)
    //    {
    //        var handler = container.GetInstance<ICommandHandler<T>>();
    //        handler.Handle(command);
    //        documentSession.SaveChanges();
    //    }
    //}
    public class CommandInvoker : ICommandInvoker
    {
        private IContainer container;

        public CommandInvoker(IContainer container)
        {
            this.container = container;
        }

        public void Execute<T>(T command)
        {
            var handler = container.GetInstance<ICommandHandler<T>>();
            handler.Handle(command);
        }
    }
}
