using IF.Core.Cache;
using IF.Core.Localization;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace IF.Persistence.EF.Localization
{
    public class LanguageService : ILanguageService
    {

        private readonly ICacheService cacheService;
        private readonly IRepository repository;

        public LanguageService(IRepository repository, ICacheService cacheService)
            
        {
            this.cacheService = cacheService;
            this.repository = repository;
        }


        public LanguageGridModel GetLanguageGridModel(string LanguageObject)
        {
            LanguageGridModel model = new LanguageGridModel();

            if (!String.IsNullOrWhiteSpace(LanguageObject))
            {
                

                Type entityType = Type.GetType(LanguageObject);


                var obj = typeof(ILanguageService)
                           .GetMethod("GetLanguageObjectList")
                           .MakeGenericMethod(entityType)
                           .Invoke(this, new object[] { });

                IEnumerable<ILanguageableEntity> languages = (IEnumerable<ILanguageableEntity>)obj;

                var properties = entityType.GetProperties();

                var identityColumnName = properties.Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute))).First().Name;


                model.Columns.Add(identityColumnName);

                object[] attrs = entityType.GetCustomAttributes(typeof(LanguageEntityAttribute), true);

                var languageEntityAttribute = attrs.First() as LanguageEntityAttribute;

                var languageableEntityProperties = languageEntityAttribute.Type.GetProperties().Where(p => p.PropertyType == typeof(string));

                foreach (var property in languageableEntityProperties)
                {
                    model.Columns.Add(property.Name);
                }

                foreach (var language in languages)
                {
                    LanguageGridRowModel row = new LanguageGridRowModel();

                    row.Data.Add(language.GetType().GetProperty(identityColumnName).GetValue(language, null).ToString());

                    foreach (var property in languageableEntityProperties)
                    {
                        var value = language.GetType().GetProperty(property.Name).GetValue(language, null);

                        if (value != null)
                        {
                            row.Data.Add(value.ToString());
                        }
                        else
                        {
                            row.Data.Add(String.Empty);
                        }
                    }

                    model.Rows.Add(row);

                }


            }

            return model;
        }
        public List<Type> GetAllLanguageEntities(Assembly[] assemblies)
        {
            List<Type> list = new List<Type>();

            try
            {
                var t = typeof(ILanguageableEntity);

                list = assemblies.SelectMany(x => x.GetTypes())
                     .Where(x => t.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract && x.UnderlyingSystemType != t)
                     .Select(x => x).ToList();
            }
            catch (ReflectionTypeLoadException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
                throw new Exception(errorMessage);
            }

            return list;
        }

        public LanguageFormModel GetLanguageFormModel(Type entityType, object Id)
        {

            

            object[] attrs = entityType.GetCustomAttributes(typeof(LanguageEntityAttribute), true);

            var languageEntityAttribute = attrs.First() as LanguageEntityAttribute;


            var obj = typeof(ILanguageService)
                     .GetMethod("GetLanguageObject")
                     .MakeGenericMethod(languageEntityAttribute.Type)
                     .Invoke(this, new object[] { Id });


            var languageableEntityProperties = languageEntityAttribute.Type.GetProperties().Where(p => p.PropertyType == typeof(string));



            IEnumerable<ILanguageEntity> languageItems = (IEnumerable<ILanguageEntity>)obj;

            LanguageFormModel model = GetLanguageModels(languageItems,entityType);

            model.Id = Id;

            return model;
        }

        private static LanguageViewModel GetLanguageModel(Type languageType, object languageObject, int languageId)
        {
            LanguageViewModel languageModel = new LanguageViewModel();

            if (languageObject == null)
            {
                languageObject = Activator.CreateInstance(languageType);
            }

            var properties = languageType.GetProperties();

            var identityColumnName = properties.Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute))).First().Name;

            foreach (var property in languageType.GetProperties())
            {
                LanguageItemModel languageItemModel = new LanguageItemModel();

                if (property.Name == "ObjectId")
                {
                    continue;
                }

                if (property.Name == "LanguageId")
                {
                    languageModel.LanguageId = languageId;
                    continue;
                }
                else if (property.Name == identityColumnName)
                {
                    object value = property.GetValue(languageObject, null);

                    if (value != null)
                    {
                        languageModel.Id = Convert.ToInt32(property.GetValue(languageObject, null));
                    }
                    else
                    {
                        languageModel.Id = 0;
                    }
                }
                else
                {
                    languageItemModel.Label = property.Name;

                    object value = property.GetValue(languageObject, null);

                    if (value != null)
                    {
                        languageItemModel.Value = value.ToString();
                    }
                    else
                    {
                        languageItemModel.Value = String.Empty;
                    }

                    languageModel.Items.Add(languageItemModel);
                }
            }

            return languageModel;
        }

        private LanguageFormModel GetLanguageModels(IEnumerable<ILanguageEntity> languageItems, Type entityType)
        {

            LanguageFormModel model = new LanguageFormModel();

            List<SystemLanguageDto> languages = new List<SystemLanguageDto>();

            languages.Add(new SystemLanguageDto { Id = 1, Name = "English", Code = "en-US" });
            languages.Add(new SystemLanguageDto { Id = 2, Name = "Türkçe", Code = "tr-TR" });

            for (int i = 0; i < languages.Count; i++)
            {

                var systemLanguage = languages.ElementAt(i);

                if (systemLanguage.Id == 2) continue;

                object languageObject = null;

                if (languageItems != null)
                {
                    languageObject = languageItems.Where(l => l.LanguageId == systemLanguage.Id).SingleOrDefault();
                }


                //Type entityType = GetObjectType(true);

                object[] attrs = entityType.GetCustomAttributes(typeof(LanguageEntityAttribute), true);

                var languageEntityAttribute = attrs.First() as LanguageEntityAttribute;

                LanguageViewModel languageModel = GetLanguageModel(languageEntityAttribute.Type, languageObject, systemLanguage.Id);

                languageModel.Index = i;

                model.Languages.Add(languageModel);

            }

            return model;
        }


        public IEnumerable<L> GetLanguageObjectList<L>() where L : class, ILanguageableEntity
        {
            return this.repository.GetQuery<L>().ToList();
        }

        public T GetLanguageObject<T>(object Id) where T : class, ILanguageEntity
        {
            string primaryKeys = this.repository.GetPrimarykeyName(typeof(T));
            return this.repository.GetQuery<T>().Where($"{primaryKeys} == @0", Id).SingleOrDefault();            
        }

        public void UpdateLanguages(Type entityType,LanguageFormModel model)
        {
            object[] attrs = entityType.GetCustomAttributes(typeof(LanguageEntityAttribute), true);

            var languageEntityAttribute = attrs.First() as LanguageEntityAttribute;


            var result = typeof(ILanguageService)
               .GetMethod("UpdateLanguages")
               .MakeGenericMethod(languageEntityAttribute.Type)
               .Invoke(this, new object[] { model });

        }

        public void UpdateLanguages<L>(LanguageFormModel model) where L : class, ILanguageEntity
        {
            foreach (var language in model.Languages)
            {
                var languageObject = this.GetLanguageObject<L>(language.Id);

                foreach (var languageItem in language.Items)
                {
                    languageObject.GetType().GetProperty(languageItem.Label).SetValue(languageObject, languageItem.Value, null);
                }

                this.repository.Update(languageObject);

            }

            this.repository.UnitOfWork.SaveChanges();
        }
    }
}
