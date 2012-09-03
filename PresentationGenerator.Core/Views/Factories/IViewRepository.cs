namespace PresentationGenerator.Core.Views.Factories
{
    public interface IViewRepository
    {
        TOutput Load<TInput, TOutput>(TInput input);
    }
}
