//using IF.Core.Localization;

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Text;

//namespace IF.Persistence.EF.Localization
//{
//    public class DictionaryService
//    {


//        public LanguageGridModel GetLanguageGridModel(string LanguageObject)
//        {
//            LanguageGridModel model = new LanguageGridModel();

//            if (!String.IsNullOrWhiteSpace(LanguageObject))
//            {


//                Type dtoType = Type.GetType(LanguageObject);

//                var mapping = this.Mapper.GetMapByLanguageEntity(dtoType);


//                var obj = typeof(ILanguageService)
//                           .GetMethod("GetLanguageObjectList")
//                           .MakeGenericMethod(mapping.Entity)
//                           .Invoke(this, new object[] { });

//                IEnumerable<IEntity> languages = (IEnumerable<IEntity>)obj;

//                var properties = dtoType.GetProperties();

//                var identityColumnName = properties.Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute))).First().Name;

//                model.Columns.Add(identityColumnName);

//                //object[] attrs = entityType.GetCustomAttributes(typeof(LanguageEntityAttribute), true);

//                //var languageEntityAttribute = attrs.First() as LanguageEntityAttribute;

//                var languageableEntityProperties = mapping.Entity.GetProperties().Where(p => p.PropertyType == typeof(string));

//                foreach (var property in languageableEntityProperties)
//                {
//                    model.Columns.Add(property.Name);
//                }

//                foreach (var language in languages)
//                {
//                    LanguageGridRowModel row = new LanguageGridRowModel();

//                    row.Data.Add(language.GetType().GetProperty(identityColumnName).GetValue(language, null).ToString());

//                    foreach (var property in languageableEntityProperties)
//                    {
//                        var value = language.GetType().GetProperty(property.Name).GetValue(language, null);

//                        if (value != null)
//                        {
//                            row.Data.Add(value.ToString());
//                        }
//                        else
//                        {
//                            row.Data.Add(String.Empty);
//                        }
//                    }

//                    model.Rows.Add(row);

//                }


//            }

//            return model;
//        }

//        public List<Type> GetAllLanguageEntities(Assembly[] assemblies)
//        {
//            List<Type> list = new List<Type>();

//            try
//            {
//                var t = typeof(ILanguageEntity);

//                list = assemblies.SelectMany(x => x.GetTypes())
//                     .Where(x => t.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract && x.UnderlyingSystemType != t)
//                     .Select(x => x).ToList();
//            }
//            catch (ReflectionTypeLoadException ex)
//            {
//                StringBuilder sb = new StringBuilder();
//                foreach (Exception exSub in ex.LoaderExceptions)
//                {
//                    sb.AppendLine(exSub.Message);
//                    FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
//                    if (exFileNotFound != null)
//                    {
//                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
//                        {
//                            sb.AppendLine("Fusion Log:");
//                            sb.AppendLine(exFileNotFound.FusionLog);
//                        }
//                    }
//                    sb.AppendLine();
//                }
//                string errorMessage = sb.ToString();
//                throw new Exception(errorMessage);
//            }

//            return list;
//        }
//    }
//}
