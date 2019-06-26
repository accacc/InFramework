using System.ComponentModel.DataAnnotations;

namespace Derin.Core.Mvc.Attributes
{
    public static class DisplayAttributeExtensions
    {
        public static DisplayAttribute Copy(this DisplayAttribute attribute)
        {
            if (attribute == null)
            {
                return null;
            }
            var copy = new DisplayAttribute();

            copy.Name = attribute.Name;
            copy.GroupName = attribute.GroupName;
            copy.Description = attribute.Description;
            copy.ResourceType = attribute.ResourceType;
            copy.ShortName = attribute.ShortName;
            copy.Prompt = attribute.Prompt;

            return copy;
        }

        public static bool CanSupplyDisplayName(this DisplayAttribute attribute)
        {
            return attribute != null
                && attribute.ResourceType != null
                && !string.IsNullOrEmpty(attribute.Name);
        }
    }
}
