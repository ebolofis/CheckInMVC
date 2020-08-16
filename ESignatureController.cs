using FastCheckInHelpers;
using FastCheckInModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastCheckInMVC.Controllers
{
    public class ESignatureController : Controller
    {
        RetrieveModelsHelper retrieveModels = new RetrieveModelsHelper();

        // GET: Esignature
        public ActionResult SignatureLayout()
        {
            return View();
        }

        public ActionResult MitsisSignatureLayout()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetSettings()
        {
            globalAttributesModel FormatModel = new globalAttributesModel();
            PrivacyPolicyModel PrivacyModel = new PrivacyPolicyModel();
            BaseConfigModel BaseConfig = new BaseConfigModel();
            ConfigurationFormModel configurationform = new ConfigurationFormModel();
            List<LanguagesMessagesModel> langmessages = new List<LanguagesMessagesModel>();

            langmessages = retrieveModels.GetLangMsgModel();
            PrivacyModel = retrieveModels.GetPrivacyModel();
            FormatModel = retrieveModels.GetCurrentFormatsModel();
            ViewBag.Format = FormatModel;
            ViewBag.Privacy = PrivacyModel;
            ViewBag.ConfigurationForm = configurationform;
            ViewBag.langmessages = langmessages;

            if (FormatModel.selectedTemplate == "Template1")
            {
                return View("SignatureLayout");
            }
            else if (FormatModel.selectedTemplate == "Template2")
            {
                return View("MitsisSignatureLayout");
            }
            else if(FormatModel.selectedTemplate=="Custom")
            {
                return View("CustomSignatureLayout");
            }
            else
            {
                return View("test");
            }
        }
    }
}