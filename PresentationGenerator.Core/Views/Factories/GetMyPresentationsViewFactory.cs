using System;
using System.Linq;
using PresentationGenerator.Core.Documents;
using PresentationGenerator.Core.Repositories;
using PresentationGenerator.Core.Utility;
using PresentationGenerator.Core.Views.Models.Input;
using PresentationGenerator.Core.Views.Models.Output;
using Raven.Client;
using Raven.Client.Linq;

namespace PresentationGenerator.Core.Views.Factories
{
    public class GetMyPresentationsViewFactory : IViewFactory<PresentationListInputModel, PresentationListViewModel>
    {
        private readonly IDocumentStore _documentstore;
        private readonly IPresentationRepository _presentationRepository;

        public GetMyPresentationsViewFactory(IDocumentStore documentstore, IPresentationRepository presentationRepository)
        {
            _documentstore = documentstore;
            _presentationRepository = presentationRepository;
        }

        public PresentationListViewModel Load(PresentationListInputModel input)
        {
            var model = new PresentationListViewModel();
            
            using (var session = _documentstore.OpenSession())
            {
                var iqueryable = from presentation in session.Query<PresentationDocument>()
                                 select presentation;
                if (input.UserId.IsNotNullOrEmpty())
                {
                    iqueryable = iqueryable.Where(e => e.UserId == input.UserId);
                }
                if (input.From.HasValue)
                {
                    iqueryable = iqueryable.Where(e => e.LastModified >= input.From.Value);
                }
                if (input.To.HasValue)
                {
                    iqueryable = iqueryable.Where(e => e.LastModified <= input.To.Value);
                }

                model.Presentations = iqueryable.Select(e => new PresentationListItem
                                         {
                                             Id = e.Id,
                                             LastModified = DateTime.Now,
                                             Title = e.Title,
                                             TotalPages = e.Pages.Count
                                         }).ToList();

            }
            return model;
        }
        
    }
}