using IF.Core.Cache;
using IF.Core.Localization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace IF.Persistence.EF.Localization
{
    public class TranslatorService : ITranslatorService
    {
        private readonly ICacheService cacheService;
        private readonly IRepository repository;
        private readonly ILanguageService languageService;
        private readonly LanguageMapper languageMapper;




        public TranslatorService(IRepository repository, ICacheService cacheService,ILanguageService languageService, LanguageMapper languageMapper)
        {
            this.cacheService = cacheService;
            this.repository = repository;
            this.languageService = languageService;
            this.languageMapper = languageMapper;
        }


        public L GetObjectCurrentLanguage<L>(int objectId) where L : class, ILanguageEntity
        {
            return this.repository.GetQuery<L>(cl => cl.ObjectId == objectId && cl.LanguageId == this.languageService.CurrentCulture.LCID).SingleOrDefault();
        }

        public L GetObjectCurrentLanguageCache<L>(int objectId) where L : class, ILanguageEntity
        {
            string cacheKey = GetLanguageKeyName<L>(objectId);

            return this.cacheService.Get<L>(cacheKey, () => this.GetObjectCurrentLanguage<L>(objectId));
        }
                     
        private string GetLanguageKeyName<L>(int objectId) where L : class, ILanguageEntity
        {
            return typeof(L).Name + this.languageService.CurrentCulture.LCID.ToString() + objectId.ToString();
        }


        public void Translate<T>(IEnumerable<T> dto) where T : LanguageDto
        {

            var currentMap = this.languageMapper.LanguageMaps.SingleOrDefault(l => l.Dto == typeof(T));

            var properties = currentMap.Language.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var item in dto)
            {

                var languageObject = typeof(ITranslatorService)
                                    .GetMethod("GetObjectCurrentLanguageCache")
                                    .MakeGenericMethod(currentMap.Language)
                                    .Invoke(this, new object[] { item.Id });

                foreach (var property in properties)
                {
                    if (property.Name == "Id" || property.Name == "ObjectId" || property.Name == "LanguageId") continue;

                    currentMap.Dto
                                   .GetProperty(property.Name)
                                   .SetValue(item, currentMap.Language
                                                   .GetProperty(property.Name)
                                                   .GetValue(languageObject, null)
                                                   , null);

                }
            }
        }



        //public void Translate<LD>(IEnumerable<LD> languageModelList)
        //        where LD : class
        //{
        //    if (this.languageService.IsDefaultLanguage())
        //    {
        //        return;
        //    }


        //    var properties = typeof(LD).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //    foreach (var languageModel in languageModelList)
        //    {

        //        foreach (PropertyInfo property in properties)
        //        {
        //            LanguagePropertyAttribute languageProperty = property.GetCustomAttributes(typeof(LanguagePropertyAttribute), false).FirstOrDefault() as LanguagePropertyAttribute;

        //            {
        //                if (null != languageProperty)
        //                {
        //                    string name = languageProperty.PropertyName;

        //                    int Id = Convert.ToInt32(languageModel.GetType().GetProperty(languageProperty.Id).GetValue(languageModel, null));

        //                    var languageObject = typeof(ITranslatorService)
        //                            .GetMethod("GetObjectCurrentLanguageCache")
        //                            .MakeGenericMethod(languageProperty.Type)
        //                            .Invoke(this, new object[] { Id });

        //                    if (languageObject != null)
        //                    {

        //                        ILanguageEntity languageEntity = (ILanguageEntity)languageObject;

        //                        languageModel.GetType()
        //                            .GetProperty(property.Name)
        //                            .SetValue(languageModel, languageEntity
        //                                            .GetType()
        //                                            .GetProperty(languageProperty.PropertyName)
        //                                            .GetValue(languageEntity, null)
        //                                            , null);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}


        //public void TranslateListCurrent<LD>(IEnumerable<LD> languageModelList)
        //  where LD : LanguageDto
        //{
        //    if (this.IsDefaultLanguage())
        //    {
        //        var properties = typeof(LD).GetProperty("Current").PropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //        foreach (var languageModel in languageModelList)
        //        {

        //            foreach (PropertyInfo property in properties)
        //            {
        //                LanguagePropertyAttribute languageProperty = property.GetCustomAttributes(typeof(LanguagePropertyAttribute), false).FirstOrDefault() as LanguagePropertyAttribute;

        //                {
        //                    if (null != languageProperty)
        //                    {
        //                        string name = languageProperty.PropertyName;

        //                        languageModel.GetType().GetProperty("Current").PropertyType
        //                             .GetProperty(property.Name)
        //                             .SetValue(languageModel
        //                                             .GetType()
        //                                             .GetProperty("Current")
        //                                             .GetValue(languageModel, null),
        //                                       languageModel
        //                                             .GetType()
        //                                             .GetProperty(property.Name)
        //                                             .GetValue(languageModel, null),

        //                                        null);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {


        //        var properties = typeof(LD).GetProperty("Current").PropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //        foreach (var languageModel in languageModelList)
        //        {

        //            foreach (PropertyInfo property in properties)
        //            {
        //                LanguagePropertyAttribute languageProperty = property.GetCustomAttributes(typeof(LanguagePropertyAttribute), false).FirstOrDefault() as LanguagePropertyAttribute;

        //                {
        //                    if (null != languageProperty)
        //                    {
        //                        string name = languageProperty.PropertyName;

        //                        int Id = Convert.ToInt32(languageModel.GetType().GetProperty(languageProperty.Id).GetValue(languageModel, null));

        //                        var languageObject = typeof(ITranslatorService)
        //                                .GetMethod("GetObjectCurrentLanguageCache")
        //                                .MakeGenericMethod(languageProperty.Type)
        //                                .Invoke(this, new object[] { Id });

        //                        if (languageObject != null)
        //                        {
        //                            var current = languageModel.GetType().GetProperty("Current");
        //                            var currentInstance = current.GetValue(languageModel, null);
        //                            var propertyValue = languageObject.GetType().GetProperty(languageProperty.PropertyName).GetValue(languageObject, null);
        //                            current.PropertyType.GetProperty(property.Name).SetValue(currentInstance, propertyValue, null);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}


        //public IEnumerable<CONSTANT> GetAllConstantCached()
        //{
        //    return this.cacheService.Get<IEnumerable<CONSTANT>>(typeof(CONSTANT).Name, () => this.GetQuery<CONSTANT>().AsEnumerable());
        //}

        //public string TranslateStringCached(string key)
        //{
        //    return this.cacheService.Get<String>(key + this.CurrentCulture.LCID.ToString(), () => this.TranslateString(key));
        //}


        //private string TranslateString(string key)
        //{
        //    var result = this.GetQuery<GLOBAL_RESOURCE>(l => l.RESOURCENAME == key && l.LANGUAGE_ID == this.CurrentCulture.LCID).FirstOrDefault();

        //    if (result == null)
        //    {
        //        result = this.GetQuery<GLOBAL_RESOURCE>(l => l.RESOURCENAME == key && l.LANGUAGE_ID == this.DefaultCulture.LCID).FirstOrDefault();

        //        if (result == null)
        //        {
        //            return key;
        //        }
        //        else
        //        {
        //            return result.RESOURCEVALUE;

        //        }
        //    }
        //    else
        //    {
        //        return result.RESOURCEVALUE;

        //    }




        //}


        //public void TranslateConstant<LD>(IEnumerable<LD> languageModelList) where LD : LanguageDTO
        //{

        //    var properties = typeof(LD).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //    foreach (var languageModel in languageModelList)
        //    {

        //        foreach (PropertyInfo property in properties)
        //        {
        //            ConstantLanguagePropertyAttribute languageProperty = property.GetCustomAttributes(typeof(ConstantLanguagePropertyAttribute), false).FirstOrDefault() as ConstantLanguagePropertyAttribute;

        //            if (languageProperty != null)
        //            {
        //                int Id = Convert.ToInt32(languageModel.GetType().GetProperty(languageProperty.Id).GetValue(languageModel, null));

        //                if (this.IsDefaultLanguage())
        //                {

        //                    var constant = this.GetAllConstantCached().Where(c => c.CONSTANT_ID == Id).SingleOrDefault();

        //                    if (constant != null)
        //                    {
        //                        languageModel.GetType()
        //                          .GetProperty(property.Name)
        //                          .SetValue(languageModel, constant.CONSTANTDISPLAY);
        //                    }



        //                }
        //                else
        //                {

        //                    var languageObject = typeof(ITranslatorService)
        //                           .GetMethod("GetObjectCurrentLanguageCache")
        //                           .MakeGenericMethod(typeof(CONSTANT_LANGUAGE))
        //                           .Invoke(this, new object[] { Id });

        //                    ILanguageEntity languageEntity = (ILanguageEntity)languageObject;

        //                    languageModel.GetType()
        //                        .GetProperty(property.Name)
        //                        .SetValue(languageModel, languageEntity
        //                                        .GetType()
        //                                        .GetProperty("CONSTANTDISPLAY")
        //                                        .GetValue(languageEntity, null)
        //                                        , null);

        //                }
        //            }

        //        }

        //    }

        //}
    }
}
