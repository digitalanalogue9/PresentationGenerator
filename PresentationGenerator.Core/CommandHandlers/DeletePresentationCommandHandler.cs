using PresentationGenerator.Core.Commands;
using PresentationGenerator.Core.Repositories;
using PresentationGenerator.Core.Utility;

namespace PresentationGenerator.Core.CommandHandlers
{
    public class DeletePresentationCommandHandler : ICommandHandler<DeletePresentationCommand>
    {
        private readonly ILogger _logger;
        private readonly IPresentationRepository _presentationRepository;

        public DeletePresentationCommandHandler(ILogger logger, IPresentationRepository presentationRepository)
        {
            _logger = logger;
            _presentationRepository = presentationRepository;
        }

        public void Handle(DeletePresentationCommand command)
        {
            var presentation = _presentationRepository.Load(command.Id);
            _presentationRepository.Remove(presentation);
        }
    }
}