using System;
using NLog;
using Raven.Client;
using PresentationGenerator.Core.Documents;
using PresentationGenerator.Core.Entities;
using PresentationGenerator.Core.Utility;
using StructureMap;

namespace PresentationGenerator.Core.Repositories
{
    public class PresentationRepository : EntityRepository<Presentation, PresentationDocument>, IPresentationRepository
    {

        public PresentationRepository(ILogger logger, IDocumentStore documentStore)
            : base(logger, documentStore)
        {

        }

        public override Presentation Create(PresentationDocument doc)
        {
            return new Presentation(doc);
        }

    }
}