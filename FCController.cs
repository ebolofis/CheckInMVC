using FastCheckInModels.Models;
using System;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using FastCheckInHelpers;
using System.ComponentModel.DataAnnotations;
using System.Web.Routing;
using FastCheckInModels.Helpers;

namespace FastCheckInMVC.Controllers
{

    public class FCController : Controller
    {   
        RetrieveModelsHelper retrieveModels = new RetrieveModelsHelper();
        List<CheckedBoxItemModel> modelList = new List<CheckedBoxItemModel>();
        public ActionResult Home()
        {
            FastCheckInModel model = new FastCheckInModel();
            return View("Template1", model);
        }

        public ActionResult Template2()
        {
            FastCheckInModel model = new FastCheckInModel();
            return View("Template2", model);
        }

        public ActionResult ThankYouPageMitsis()
        {
            return View("ThankYouPageMitsis");
        }

        [HttpGet]
        public ActionResult redirect(string url, string group, string hotelname) //string1
        {
            return RedirectToAction("GetSettings", "FC",new { url = url, group = group, hotelname = hotelname });
        }


        [HttpGet]
        public ActionResult GetContactless(string url, string group, string hotelname)
        {
            globalAttributesModel FormatModel = new globalAttributesModel();
            PrivacyPolicyModel PrivacyModel = new PrivacyPolicyModel();
            SettingsModel settings = new SettingsModel() { url = url, grpname = group, hotelname = hotelname };
            BaseConfigModel BaseConfig = new BaseConfigModel();
            ConfigurationFormModel configurationform = new ConfigurationFormModel();
            List<LanguagesMessagesModel> langmessages = new List<LanguagesMessagesModel>();

            langmessages = retrieveModels.GetLangMsgModel();
            modelList = retrieveModels.GetFieldsConfigurationListModel();
            modelList = modelList.OrderByDescending(x => x.OrderField).ToList();
            PrivacyModel = retrieveModels.GetPrivacyModel();
            FormatModel = retrieveModels.GetCurrentFormatsModel();
            ViewBag.Config = settings;
            ViewBag.Format = FormatModel;
            ViewBag.Privacy = PrivacyModel;
            ViewBag.ConfigurationForm = configurationform;
            ViewBag.langmessages = langmessages;

            return View("Contactless");
        }


        [HttpGet]
        public ActionResult GetSettings(string url, string group, string hotelname)
        {

            globalAttributesModel FormatModel = new globalAttributesModel();
            PrivacyPolicyModel PrivacyModel = new PrivacyPolicyModel();
            SettingsModel settings = new SettingsModel() { url = url, grpname = group, hotelname = hotelname };
            BaseConfigModel BaseConfig = new BaseConfigModel();
            ConfigurationFormModel configurationform = new ConfigurationFormModel();

            List<LanguagesMessagesModel> langmessages = new List<LanguagesMessagesModel>();
            langmessages= retrieveModels.GetLangMsgModel();

            modelList = retrieveModels.GetFieldsConfigurationListModel(); 
            modelList= modelList.OrderByDescending(x => x.OrderField).ToList();

            PrivacyModel = retrieveModels.GetPrivacyModel();
            FormatModel = retrieveModels.GetCurrentFormatsModel();
        
            ViewBag.Config = settings;
            ViewBag.Format = FormatModel;
            ViewBag.Privacy = PrivacyModel;
            ViewBag.ConfigurationForm = configurationform;
            ViewBag.langmessages = langmessages;

            if (FormatModel.selectedTemplate == "Template1")
            {
                return View("Template1");
            }
            else if (FormatModel.selectedTemplate == "Template2")
            {
                ViewBag.MainConfig = settings;
                return View("Template2");
            }
            else if (FormatModel.selectedTemplate == "Custom")
            {
                return View("Custom", modelList);
            }
            else
                return View("test");
        }

        private bool LoadConfig(string url)
        {
           
            WebApiClientHelper apiHlp = new WebApiClientHelper();

            int returnCode = 0;
            string ErrorMess = "";
            string res;
            res = apiHlp.GetRequest(url + "/api/Sign/" + "GetConfigListModel", "", null, out returnCode, out ErrorMess);
            if (returnCode == 200)
            {
                modelList = JsonConvert.DeserializeObject<List<CheckedBoxItemModel>>(res);
                modelList = modelList.OrderByDescending(o => o.OrderField).ToList();

                return true;
            }
            else
                return false;
        }
    }
    public class SettingsModel
    {
        public string url { get; set; }
        public string grpname { get; set; }
        public string hotelname { get; set; }
        public string template { get; set; }
    }

    public enum StateEnum
    {
        Normal,
        Contactless,
        DirectCheckIn
    }



}