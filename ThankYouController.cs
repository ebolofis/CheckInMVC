using FastCheckInHelpers;
using FastCheckInModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastCheckInMVC.Controllers
{
    public class ThankYouController : Controller
    {
        RetrieveModelsHelper retrieveModels = new RetrieveModelsHelper();
        public ActionResult ThankYouPageMitsis()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetSettings()
        {
            globalAttributesModel FormatModel = retrieveModels.GetCurrentFormatsModel();

            BaseConfigModel BaseConfig = new BaseConfigModel();
            ConfigurationFormModel configurationform = new ConfigurationFormModel();



            
            FormatModel = retrieveModels.GetCurrentFormatsModel();
            ViewBag.Format = FormatModel;
            
            ViewBag.ConfigurationForm = configurationform;
            
            if (FormatModel.selectedTemplate == "Template1")
            {
                return View("ThankYouPageTemplate1");
            }
            else if (FormatModel.selectedTemplate == "Template2")
            {
                return View("ThankYouPageMitsis");
            }
            else if(FormatModel.selectedTemplate =="Custom")
            {
                return View("ThankYouPageCustom");
            }
            else
            {
                return View("test");
            }
        }
    }
}