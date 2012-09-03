using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PresentationGenerator.Core.CommandInvokers;
using PresentationGenerator.Core.Commands;
using PresentationGenerator.Core.Utility;
using PresentationGenerator.Core.Views.Factories;
using PresentationGenerator.Core.Views.Models.Input;
using PresentationGenerator.Core.Views.Models.Output;

namespace PresentationGenerator.Web.Controllers
{
    [Authorize]
    public class PresentationController : Controller
    {
        private ICommandInvoker commandInvoker;
        private IViewRepository viewRepository;

        public PresentationController(ICommandInvoker commandInvoker, IViewRepository viewRepository)
        {
            this.commandInvoker = commandInvoker;
            this.viewRepository = viewRepository;
        }

        [HttpGet]
        public ActionResult GuidGenerator()
        {
            return Json(new {Id = GuidUtil.GenerateComb()}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Demo()
        {
            return View();
        }

        [HttpGet]
        public ActionResult View(GetPresentationInputModel input)
        {
            var model = viewRepository.Load<GetPresentationInputModel, GetPresentationViewModel>(input);
            return View(model);
        }

        [HttpGet]
        public ActionResult Make(GetPresentationInputModel input)
        {
            var model = viewRepository.Load<GetPresentationInputModel, GetPresentationViewModel>(input);
            return View(model);
        }

        [HttpGet]
        public ActionResult AjaxMake(GetPresentationInputModel input)
        {
            var model = viewRepository.Load<GetPresentationInputModel, GetPresentationViewModel>(input);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Delete(DeletePresentationInputModel input)
        {
            var command = new DeletePresentationCommand(
                IdUtil.CreateId("presentations", input.Id.ToString()));
            commandInvoker.Execute(command);
            return RedirectToAction("Mine");

        }

        [HttpGet]
        public ActionResult Mine()
        {
            var input = new PresentationListInputModel {UserId = this.User.Identity.Name};
            var model = viewRepository.Load<PresentationListInputModel, PresentationListViewModel>(input);
            return View(model);
        }


        [HttpGet]
        public ActionResult GetAllPresentations(PresentationListInputModel input)
        {
            var model = viewRepository.Load<PresentationListInputModel, PresentationListViewModel>(input);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Make(MakePresentationInputModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new MakePresentationCommand(
                      this.User.Identity.Name,
                      IdUtil.CreateId("presentations", model.Id.ToString()),
                      model.Pages,
                      model.Title,
                  DateTime.Now);
                commandInvoker.Execute(command);
            }
            else
            {
                return Json("BAD");
            }
            return Json("OK");


        }

        [HttpPost]
        public ActionResult ReOrderPages(ReorderPagesInputModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new ReOrderPagesCommand(
                      IdUtil.CreateId("presentations", model.Id),model.Positions,DateTime.Now);

                commandInvoker.Execute(command);
            }
            else
            {
                return Content("BAD");
            }
            return Content("OK");


        }
    }
}
