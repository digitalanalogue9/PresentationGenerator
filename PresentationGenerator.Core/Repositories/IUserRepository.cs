using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PresentationGenerator.Core.Entities;
using PresentationGenerator.Core.Documents;
using RavenGallery.Core.Documents;

namespace PresentationGenerator.Core.Repositories
{
    public interface IUserRepository : IEntityRepository<User, UserDocument>
    {

    }
}
