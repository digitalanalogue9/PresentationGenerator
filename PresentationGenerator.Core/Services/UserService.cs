using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;
using PresentationGenerator.Core.Documents;
using PresentationGenerator.Core.Utility;
using PresentationGenerator.Core.Indexes;
using RavenGallery.Core.Documents;

namespace PresentationGenerator.Core.Services
{
    public class UserService : IUserService
    {
        public IDocumentStore _documentSession;

        public UserService(IDocumentStore documentstore)
        {
            _documentSession = documentstore;
        }

        public bool DoesUserExistWithUsername(string username)
        {
            using (var documentSession = _documentSession.OpenSession())
            {
                return documentSession.Query<UserDocument>()
                    .Where(x => x.Username == username)
                    .Any();
            }
        }

        public bool DoesUserExistWithUsernameAndPassword(string username, string password)
        {
            using (var documentSession = _documentSession.OpenSession())
            {
                String hashedPass = HashUtil.HashPassword(password);
                return documentSession.Query<UserDocument>()
                    .Where(x => x.Username == username && x.PasswordHash == hashedPass)
                    .Any();
            }
        } 
    }
}
