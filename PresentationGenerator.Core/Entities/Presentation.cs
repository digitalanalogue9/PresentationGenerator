using System;
using System.Xml.Linq;
using PresentationGenerator.Core.Documents;

namespace PresentationGenerator.Core.Entities
{
    public class Presentation : IEntity<PresentationDocument>
    {
        private PresentationDocument innerDocument;

        public string Id { get { return innerDocument.Id; } }

        public Presentation(PresentationDocument innerMatter)
        {
            this.innerDocument = innerMatter;
        }

        public PresentationDocument GetInnerDocument()
        {
            return innerDocument;
        }

    }
}