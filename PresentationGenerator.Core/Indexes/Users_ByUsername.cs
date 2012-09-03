using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client.Document;
using Raven.Client.Indexes;
using RavenGallery.Core.Documents;

namespace PresentationGenerator.Core.Indexes
{
    public class Users_ByUsername : AbstractIndexCreationTask<UserDocument>
    {
        public Users_ByUsername()
        {
            Map = docs => from doc in docs select new { Username = doc.Username.ToLower() };
        }
    }
}
