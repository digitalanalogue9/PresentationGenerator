using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using PresentationGenerator.Core.Utility;

namespace PresentationGenerator.Core.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        public void SignIn(string username, bool persistent)
        {
            FormsAuthentication.SetAuthCookie(IdUtil.CreateId("users", username), persistent);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
