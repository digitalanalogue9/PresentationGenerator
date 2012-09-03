using PresentationGenerator.Core.Repositories;
using PresentationGenerator.Core.Utility;
using PresentationGenerator.Core.Views.Models.Input;
using PresentationGenerator.Core.Views.Models.Output;
using Raven.Client;

namespace PresentationGenerator.Core.Views.Factories
{
    public class GetPresentationViewFactory : IViewFactory<GetPresentationInputModel, GetPresentationViewModel>
    {
        private readonly IDocumentStore _documentstore;
        private readonly IPresentationRepository _presentationRepository;

        public GetPresentationViewFactory(IDocumentStore documentstore, IPresentationRepository presentationRepository)
        {
            _documentstore = documentstore;
            _presentationRepository = presentationRepository;
        }

        public GetPresentationViewModel Load(GetPresentationInputModel input)
        {
            var model = new GetPresentationViewModel();
            if (input.Id.HasValue)
            {
                var presentation = _presentationRepository.Load(IdUtil.CreateId("presentations", input.Id.Value.ToString()));
                model.Id = presentation.Id.Replace("presentations/", string.Empty);
                model.Title = presentation.GetInnerDocument().Title;
                model.Pages = presentation.GetInnerDocument().Pages;
                
            }

            return model;
        }
    }
}