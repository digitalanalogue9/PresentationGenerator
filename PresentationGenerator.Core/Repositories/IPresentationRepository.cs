using PresentationGenerator.Core.Documents;
using PresentationGenerator.Core.Entities;

namespace PresentationGenerator.Core.Repositories
{
    public interface IPresentationRepository : IEntityRepository<Presentation, PresentationDocument>
    {
    }
}