using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IF.Core.Html
{
    public class HtmlFormParser : IHtmlFormParser
    {
        public List<Input> GetInputs(string form)
        {
            string inputPattern = "<\\s*input.*?>";

            Regex reg = new Regex(inputPattern, RegexOptions.Multiline);
            MatchCollection inputMatchs = reg.Matches(form);



            string namePattern = "<\\s*input.*?name\\s*=\\s*\"(?<Name>.*?)\".*?";
            string typePattern = "<\\s*input.*?type\\s*=\\s*\"(?<Type>.*?)\".*?";
            string valuePattern = "<\\s*input.*?value\\s*=\\s*\"(?<Value>.*?)\".*?";

            List<Input> inputs = new List<Input>();

            foreach (Match inputMatch in inputMatchs)
            {
                Input input = new Input();

                input.Name = GetAttribute(inputMatch.Value, namePattern, "Name");
                input.Type = GetAttribute(inputMatch.Value, typePattern, "Type");
                input.Value = GetAttribute(inputMatch.Value, valuePattern, "Value");

                inputs.Add(input);
            }



            return inputs;
        }

        private static string GetAttribute(string inputString, string namePattern, string attributeName)
        {
            Regex reg = new Regex(namePattern, RegexOptions.Multiline);

            MatchCollection nameMatchs = reg.Matches(inputString);

            if (nameMatchs.Count == 0)
            {
                return String.Empty;
            }
            else
            {
                return nameMatchs[0].Groups[attributeName].Value;
            }

        }
    }
}
