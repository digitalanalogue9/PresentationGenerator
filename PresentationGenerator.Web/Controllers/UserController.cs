using System.Web.Mvc;
using System.Web.Security;
using PresentationGenerator.Core.CommandInvokers;
using PresentationGenerator.Core.Commands;
using PresentationGenerator.Core.Security;
using PresentationGenerator.Core.Views.Models.Output;

namespace PresentationGenerator.Web.Controllers
{
    [HandleError]
    public class UserController : Controller
    {
        private ICommandInvoker commandInvoker;
        private IAuthenticationService authenticationService;

        public UserController(ICommandInvoker commandInvoker, IAuthenticationService authenticationService)
        {
            this.commandInvoker = commandInvoker;
            this.authenticationService = authenticationService;

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Register()
        {
            return View(new UserRegisterViewModel());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SignIn()
        {
            return View(new UserSignInViewModel());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SignOut()
        {
            authenticationService.SignOut();
            FormsAuthentication.RedirectToLoginPage();

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                commandInvoker.Execute(new RegisterNewUserCommand(model.Username, model.Password));
                authenticationService.SignIn(model.Username, model.StayLoggedIn);
                return RedirectToAction("Make", "Presentation");
            }
            else
            {
                return View(model);
            }            
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SignIn(UserSignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                authenticationService.SignIn(model.Username, model.StayLoggedIn);
                return RedirectToAction("Make", "Presentation");
            }
            else
            {
                return View(model);
            }          
        }
    }
}
