using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PresentationGenerator.Core.Entities;
using PresentationGenerator.Core.Documents;
using PresentationGenerator.Core.Utility;
using Raven.Client;
using RavenGallery.Core.Documents;

namespace PresentationGenerator.Core.Repositories
{
    public class UserRepository : EntityRepository<User, UserDocument>, IUserRepository
    {
        public UserRepository(ILogger logger, IDocumentStore documentStore)
            : base(logger, documentStore)
        {

        }

        public override User Create(UserDocument doc)
        {
            return new User(doc);
        }    }
}
