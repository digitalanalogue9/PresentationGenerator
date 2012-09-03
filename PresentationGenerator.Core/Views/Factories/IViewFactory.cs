namespace PresentationGenerator.Core.Views.Factories
{
    public interface IViewFactory<TInput, TOutput>
    {
        TOutput Load(TInput input);
    }
}
