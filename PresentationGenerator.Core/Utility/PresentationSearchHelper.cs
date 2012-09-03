using Raven.Client;

namespace PresentationGenerator.Core.Utility
{
    public class PresentationSearchHelper : SearchHelper, IPresentationHelper
    {
        public PresentationSearchHelper(IDocumentStore documentstore)
            : base(documentstore)
        {
        }
    }
}