using System.Collections;
using System.Collections.Specialized;
using System;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Derin.Core.Extensions
{
    public static class StringExtensions
    {
        public static string SplitUpperCaseToString(this string source)
        {
            if (source == null)
            {
                return null;
            }
            return string.Join(" ", source.SplitUpperCase());
        }

        public  static string ConvertToCamelCase(this string text)
        {
            text = text.ToLower();
            string[] splittedPhrase = text.Split(' ', '-', '.','_');
            var sb = new StringBuilder();
            //sb.Append(splittedPhrase[0].ToLower());
            //splittedPhrase[0] = string.Empty;

            foreach (String s in splittedPhrase)
            {
                char[] splittedPhraseChars = s.ToCharArray();
                if (splittedPhraseChars.Length > 0)
                {
                    splittedPhraseChars[0] = ((new String(splittedPhraseChars[0], 1)).ToUpper().ToCharArray())[0];
                }
                sb.Append(new String(splittedPhraseChars));
            }
            return sb.ToString();
        }

        public static string ToPascalCase(this string the_string)
        {
            // If there are 0 or 1 characters, just return the string.
            if (the_string == null) return the_string;
            if (the_string.Length < 2) return the_string.ToUpper();

            // Split the string into words.
            string[] words = the_string.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            string result = "";
            foreach (string word in words)
            {
                result +=
                    word.Substring(0, 1).ToUpper() +
                    word.Substring(1);
            }

            return result;
        }

        public static string ToPascalCase2(string text)
        {
            var resultBuilder = new System.Text.StringBuilder();

            if (!string.IsNullOrEmpty(text))
            {
                foreach (var c in text)
                {
                    if (!Char.IsLetterOrDigit(c))
                        resultBuilder.Append(" ");
                    else
                        resultBuilder.Append(c);
                }
            }

            var result = resultBuilder.ToString();

            result = result.ToLower(CultureInfo.InvariantCulture);

            var textInfo = new CultureInfo("en-US", false).TextInfo;

            result = textInfo.ToTitleCase(result).Replace(" ", String.Empty);

            return result;
        }

        public static string ToCamelCase(this string the_string)
        {
            // If there are 0 or 1 characters, just return the string.
            if (the_string == null || the_string.Length < 2)
                return the_string;

            // Split the string into words.
            string[] words = the_string.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            string result = words[0].ToLower();
            for (int i = 1; i < words.Length; i++)
            {
                result +=
                    words[i].Substring(0, 1).ToUpper() +
                    words[i].Substring(1);
            }

            return result;
        }

        public static string ToProperCase(this string the_string)
        {
            // If there are 0 or 1 characters, just return the string.
            if (the_string == null) return the_string;
            if (the_string.Length < 2) return the_string.ToUpper();

            // Start with the first character.
            string result = the_string.Substring(0, 1).ToUpper();

            // Add the remaining characters.
            for (int i = 1; i < the_string.Length; i++)
            {
                if (char.IsUpper(the_string[i])) result += " ";
                result += the_string[i];
            }

            return result;
        }

        public static string RemoveEmojiChars(this string source)
        {
            return Regex.Replace(source, @"\p{Cs}", "");
        }

        public static string[] SplitUpperCase(this string source)
        {
            if (source == null)
                return new string[] { };

            if (source.Length == 0)
                return new string[] { "" };

            StringCollection words = new StringCollection();
            int wordStartIndex = 0;

            char[] letters = source.ToCharArray();
            char previousChar = char.MinValue;

            for (int i = 1; i < letters.Length; i++)
            {
                if (char.IsUpper(letters[i]) && !char.IsWhiteSpace(previousChar))
                {
                    words.Add(new String(letters, wordStartIndex, i - wordStartIndex));
                    wordStartIndex = i;
                }
                previousChar = letters[i];
            }
            words.Add(new String(letters, wordStartIndex, letters.Length - wordStartIndex));
            string[] wordArray = new string[words.Count];
            words.CopyTo(wordArray, 0);
            return wordArray;
        }
    }
}
