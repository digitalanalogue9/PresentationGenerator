using PresentationGenerator.Core.Utility;
using RavenGallery.Core.Documents;

namespace PresentationGenerator.Core.Entities
{
    public class User : IEntity<UserDocument>
    {
        private UserDocument innerDocument;

        public string Id { get { return innerDocument.Id; } }

        public User(string username, string password)
        {
            innerDocument = new UserDocument()
            {
                Id = IdUtil.CreateId("users",username),
                PasswordHash = HashUtil.HashPassword(password),
                Username = username
            };
        }

        public User(UserDocument innerUser)
        {
            this.innerDocument = innerUser;
        }

        UserDocument IEntity<UserDocument>.GetInnerDocument()
        {
            return innerDocument;
        }
    }
}
