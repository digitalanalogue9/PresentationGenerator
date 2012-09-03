using System;
using System.Linq;
using PresentationGenerator.Core.Commands;
using PresentationGenerator.Core.Repositories;
using PresentationGenerator.Core.Utility;

namespace PresentationGenerator.Core.CommandHandlers
{
    public class ReOrderPagesCommandHandler : ICommandHandler<ReOrderPagesCommand>
    {
        private readonly ILogger _logger;
        private readonly IPresentationRepository _presentationRepository;

        public ReOrderPagesCommandHandler(ILogger logger, IPresentationRepository presentationRepository)
        {
            _logger = logger;
            _presentationRepository = presentationRepository;
        }

        public void Handle(ReOrderPagesCommand command)
        {
            var presentation = _presentationRepository.Load(command.Id);
            var doc = presentation.GetInnerDocument();
            for (var index = 0; index < command.Positions.Count; index++)
            {
                var id = command.Positions[index].Id;
                var page = doc.Pages.Where(e => e.Id == command.Positions[index].Id).SingleOrDefault();
                if (page == null)
                {
                    throw new ApplicationException(string.Format("can't find page {0} in presentation {1}.", id, presentation.Id));
                }
                //page.Position = command.Positions[index].Order;
            }
            _presentationRepository.Add(presentation);
        }
    }
}