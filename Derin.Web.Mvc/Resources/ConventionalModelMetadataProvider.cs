using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using IF.Core.Resources;

namespace Derin.Core.Resources
{


    public class ConventionalModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {

        private readonly IResourceService resourceService;

        public ConventionalModelMetadataProvider(IResourceService resourceService)
            : base()
        {
            this.resourceService = resourceService;
        }

        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor,
            Type modelType, string propertyName)
        {


            var displayAttribute = attributes.FirstOrDefault(x => x.ToString() == "System.ComponentModel.DataAnnotations.DisplayAttribute");

            if (displayAttribute != null)
            {
                return base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            }

            if (!String.IsNullOrWhiteSpace(propertyName))
            {


                var fullName = (containerType ?? modelType).Name + "." + propertyName;                

                var transalted = resourceService.GetResource(fullName);

                if (transalted == null)
                {
                    transalted = resourceService.GetResource(propertyName);
                }


                if (transalted == null)
                {
                    CultureInfo cultureInfo = new CultureInfo("en-US");

                    transalted = cultureInfo.TextInfo.ToTitleCase(propertyName.ToLower(cultureInfo)).Replace("_", " ");
                }


                if (transalted != null)
                {

                    var newAttribute = new DisplayAttribute
                    {
                        Name = transalted
                    };
                    attributes = new List<Attribute>(attributes);
                    (attributes as List<Attribute>).Add(newAttribute);
                }
            }

            return base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
        }



    }



    
}
