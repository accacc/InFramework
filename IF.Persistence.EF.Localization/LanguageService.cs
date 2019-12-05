using IF.Core.Cache;
using IF.Core.Data;
using IF.Core.Localization;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading;

namespace IF.Persistence.EF.Localization
{
    public class LanguageService : ILanguageService
    {

        private readonly ICacheService cacheService;
        private readonly IRepository repository;
        private readonly Assembly[] assembiles;
        

        public CultureInfo[] Cultures { get; }
        public CultureInfo DefaultCulture { get; }

        public LanguageMapper Mapper { get; }

        public bool IsDefaultLanguage()
        {
            return this.CurrentCulture.LCID == this.DefaultCulture.LCID;
        }

        public CultureInfo CurrentCulture
        {
            get { return Thread.CurrentThread.CurrentCulture; }
        }

        

        public LanguageService(IRepository repository, ICacheService cacheService, LocalizationSettings settings, LanguageMapper languageMapper)            
        {
            this.cacheService = cacheService;
            this.repository = repository;
            this.Mapper = languageMapper;
            this.assembiles = settings.Assemblies;
            this.Cultures = settings.Cultures;
            this.DefaultCulture = this.Cultures.SingleOrDefault(l => l.LCID == settings.DefaultLanguage);            
        }

        public L GetObjectCurrentLanguage<L>(int objectId) where L : class, ILanguageEntity
        {
            return this.repository.GetQuery<L>(cl => cl.ObjectId == objectId && cl.LanguageId == this.CurrentCulture.LCID).SingleOrDefault();
        }

        public L GetObjectCurrentLanguageCache<L>(int objectId) where L : class, ILanguageEntity
        {
            string cacheKey = GetLanguageKeyName<L>(objectId);

            return this.cacheService.Get<L>(cacheKey, () => this.GetObjectCurrentLanguage<L>(objectId));
        }

        private string GetLanguageKeyName<L>(int objectId) where L : class, ILanguageEntity
        {
            return typeof(L).Name + this.CurrentCulture.LCID.ToString() + objectId.ToString();
        }



        public LanguageGridModel GetLanguageGridModel(string LanguageObject)
        {
            LanguageGridModel model = new LanguageGridModel();

            if (!String.IsNullOrWhiteSpace(LanguageObject))
            {
                

                Type dtoType = Type.GetType(LanguageObject);

                var mapping = this.Mapper.GetMapByLanguageEntity(dtoType);


                var obj = typeof(ILanguageService)
                           .GetMethod("GetLanguageObjectList")
                           .MakeGenericMethod(mapping.Entity)
                           .Invoke(this, new object[] { });

                IEnumerable<IEntity> languages = (IEnumerable<IEntity>)obj;

                var properties = dtoType.GetProperties();

                var identityColumnName = properties.Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute))).First().Name;

                model.Columns.Add(identityColumnName);

                //object[] attrs = entityType.GetCustomAttributes(typeof(LanguageEntityAttribute), true);

                //var languageEntityAttribute = attrs.First() as LanguageEntityAttribute;

                var languageableEntityProperties = mapping.Entity.GetProperties().Where(p => p.PropertyType == typeof(string));

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
                var t = typeof(ILanguageEntity);

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

            var obj = typeof(ILanguageService)
                     .GetMethod("GetLanguageObject")
                     .MakeGenericMethod(entityType)
                     .Invoke(this, new object[] { Id });


            var languageableEntityProperties = entityType.GetProperties().Where(p => p.PropertyType == typeof(string));



            ILanguageEntity languageItems = (ILanguageEntity)obj;

            LanguageFormModel model = GetLanguageModels(languageItems,entityType);

            model.Id = Id;

            return model;
        }

        

        private LanguageViewModel GetLanguageModel(Type languageType, object languageObject, int languageId)
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

        private LanguageFormModel GetLanguageModels(ILanguageEntity languageObject, Type entityType)
        {

            LanguageFormModel model = new LanguageFormModel();


            for (int i = 0; i < this.Cultures.Length; i++)
            {

                var systemLanguage = this.Cultures.ElementAt(i);

                if (systemLanguage.LCID == this.DefaultCulture.LCID) continue;             

                

                LanguageViewModel languageModel = GetLanguageModel(entityType, languageObject, Convert.ToInt32(systemLanguage.LCID));

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
            return this.repository.GetQuery<T>().Where($"{primaryKeys} == @0", System.Convert.ChangeType(Id, typeof(int))).SingleOrDefault();            
        }

        

        public void UpdateLanguages(Type entityType,LanguageFormModel model)
        {

            var result = typeof(LanguageService)
               .GetMethod("UpdateLanguagesGeneric")
               .MakeGenericMethod(entityType)
               .Invoke(this, new object[] { model });

        }


        public void UpdateLanguagesGeneric<L>(LanguageFormModel model) where L : class, ILanguageEntity
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
