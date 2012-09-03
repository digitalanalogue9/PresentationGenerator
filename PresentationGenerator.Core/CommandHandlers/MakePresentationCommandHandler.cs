using System.Collections.Generic;
using PresentationGenerator.Core.Commands;
using PresentationGenerator.Core.Documents;
using PresentationGenerator.Core.Repositories;
using PresentationGenerator.Core.Utility;
using PresentationGenerator.Core.Views;

namespace PresentationGenerator.Core.CommandHandlers
{
    public class MakePresentationCommandHandler : ICommandHandler<MakePresentationCommand>
    {
        private readonly ILogger _logger;
        private readonly IPresentationRepository _presentationRepository;

        public MakePresentationCommandHandler(ILogger logger, IPresentationRepository presentationRepository)
        {
            _logger = logger;
            _presentationRepository = presentationRepository;
        }

        public void Handle(MakePresentationCommand command)
        {
            var data = new PresentationDocument
                           {
                               Id = command.Id,
                               UserId = command.UserId,
                               Pages = command.Pages,
                               Title = command.Title,
                               LastModified = command.Created
                           };
            var presentation = _presentationRepository.Create(data);
            _presentationRepository.Add(presentation);
        }
    }
}