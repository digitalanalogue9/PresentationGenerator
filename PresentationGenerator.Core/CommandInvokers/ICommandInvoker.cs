namespace PresentationGenerator.Core.CommandInvokers
{
    public interface ICommandInvoker
    {
        void Execute<T>(T command);
    }
}
