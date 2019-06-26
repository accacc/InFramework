using System;

namespace Derin.Core.Localization
{

    public interface LanguageDTO : ILanguageProperty
    {
        //int Id { get; set; }
    }

    public class LanguageDTO<T> : LanguageDTO where T : ILanguageProperty, new()
    {
        public LanguageDTO()
        {
            this.Current = new T();
        }


        public T Current { get; set; }
    }


    

    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)]
    public class LanguagePropertyAttribute : Attribute
    {
        public Type Type { get; set; }
        public string PropertyName { get; set; }
        public string Id { get; set; }


        public LanguagePropertyAttribute(Type type,string propertyName,string Id)
        {
            this.Type = type;
            this.PropertyName = propertyName;
            this.Id = Id;
        }
    }

       
   
    [System.AttributeUsage(System.AttributeTargets.Property , AllowMultiple = true)]
    public class ConstantLanguagePropertyAttribute : Attribute
    {
        public string Id { get; set; }

        public ConstantLanguagePropertyAttribute(string Id)
        {
            this.Id = Id;
        }

    }
}
