using IF.Core.Localization;
using IF.Persistence.EF.Localization;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Reflection;


namespace Gedik.SSO.Dictionary
{
    public class DictionaryController : Controller
    {

        ILanguageService languageService;

      
        public DictionaryController(ILanguageService languageService)
        {
            this.languageService = languageService;
        }

        public ActionResult LanguageObjectIndex(string LanguageObject)
        {
            if (!String.IsNullOrEmpty(LanguageObject))
            {
                SetObjectType(LanguageObject);
                return RedirectToAction("LanguageObjectsList");
            }

            List<Type> list = this.languageService.GetAllLanguageEntities(new Assembly[] { Assembly.Load("Gedik.SSO.Contract") });

            ViewBag.LanguagesObject = list;

            return View("Index");
        }

        public  ActionResult RetrieveLanguageObjectList(string LanguageObject)
        {
            SetObjectType(LanguageObject);

            LanguageGridModel model = this.languageService.GetLanguageGridModel(LanguageObject);

            return PartialView("~/Views/Dictionary/_GridView.cshtml", model);
        }    

        public  ActionResult LanguageObjectUpdate(string Id)
        {
            Type entityType = GetObjectType(true);
            LanguageFormModel model = this.languageService.GetLanguageFormModel(entityType,Id);
            return PartialView("~/Views/Dictionary/Update.cshtml", model);
        }



        [HttpPost]
        public ActionResult LanguageObjectUpdate(LanguageFormModel model,string Id )
        {
            model.Id = Id;

            Type entityType = GetObjectType(true);

            this.languageService.UpdateLanguages(entityType, model);
            
            ModelState.Clear();

            model = this.languageService.GetLanguageFormModel(entityType,model.Id);

            return PartialView("~/Views/Dictionary/Update.cshtml", model);
        }

        private void SetObjectType(string LanguageObject)
        {
            TempData["LanguageObject"] = LanguageObject;
        }

        private Type GetObjectType(bool keepAlive = false)
        {
            string typeString = TempData["LanguageObject"].ToString();
            Type type = Type.GetType(typeString);
            if (keepAlive) SetObjectType(typeString);
            return type;
        }   
    }
}
