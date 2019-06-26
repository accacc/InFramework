using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace IF.Core
{
    public class RandomGenerator : IRandomGenerator
    {

        public string CreateCapcthaString()
        {
            const string validChars = "ABCDEFGHJKLMNPRSTUVXYZ";
            const string validNumbers = "123456789";

            StringBuilder res = new StringBuilder();
            System.Random rnd = new System.Random();

            res.Append(validChars[rnd.Next(validChars.Length)]);
            res.Append(validChars[rnd.Next(validChars.Length)]);
            res.Append(validChars[rnd.Next(validChars.Length)]);
            res.Append(validNumbers[rnd.Next(validNumbers.Length)]);
            res.Append(validNumbers[rnd.Next(validNumbers.Length)]);
            
            return res.ToString();
        }

        public string CreateRandomString(int length)
        {
            const string valid = "abcdefghjkmnprstuvxyzABCDEFGHJKLMNPRSTUVXYZ123456789";
            StringBuilder res = new StringBuilder();
            System.Random rnd = new System.Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public string CreateRandomNumber(int length)
        {
            const string valid = "0123456789";
            StringBuilder res = new StringBuilder();
            System.Random rnd = new System.Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        private  int DEFAULT_MIN_PASSWORD_LENGTH = 8;
        private  int DEFAULT_MAX_PASSWORD_LENGTH = 10;

        private string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";
        //private string PASSWORD_CHARS_LCASE   = "abcdefgijkmnopqrstwxyz";
        private string PASSWORD_CHARS_NUMERIC = "23456789";
        //private string PASSWORD_CHARS_SPECIAL = "*$-+?_&=!%{}/";

       

        public string GeneratePassword()
        {
            return GeneratePassword(DEFAULT_MIN_PASSWORD_LENGTH,
                            DEFAULT_MAX_PASSWORD_LENGTH);
        }

        public string GeneratePassword(int length)
        {
            return GeneratePassword(length, length);
        }

        public string GeneratePassword(int minLength,int maxLength)
        {
            if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
                return null;

            char[][] charGroups = new char[][]
            {
            //PASSWORD_CHARS_LCASE.ToCharArray(),
            PASSWORD_CHARS_UCASE.ToCharArray(),
            PASSWORD_CHARS_NUMERIC.ToCharArray(),
                //PASSWORD_CHARS_SPECIAL.ToCharArray()
            };

            int[] charsLeftInGroup = new int[charGroups.Length];

            for (int i = 0; i < charsLeftInGroup.Length; i++)
                charsLeftInGroup[i] = charGroups[i].Length;

            int[] leftGroupsOrder = new int[charGroups.Length];

            for (int i = 0; i < leftGroupsOrder.Length; i++)
                leftGroupsOrder[i] = i;

            byte[] randomBytes = new byte[4];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            int seed = BitConverter.ToInt32(randomBytes, 0);

            System.Random random = new System.Random(seed);

            char[] password = null;

            if (minLength < maxLength)
                password = new char[random.Next(minLength, maxLength + 1)];
            else
                password = new char[minLength];

            int nextCharIdx;

            int nextGroupIdx;

            int nextLeftGroupsOrderIdx;

            int lastCharIdx;

            int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

            for (int i = 0; i < password.Length; i++)
            {
                if (lastLeftGroupsOrderIdx == 0)
                    nextLeftGroupsOrderIdx = 0;
                else
                    nextLeftGroupsOrderIdx = random.Next(0,
                                                         lastLeftGroupsOrderIdx);

                nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

                lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                if (lastCharIdx == 0)
                    nextCharIdx = 0;
                else
                    nextCharIdx = random.Next(0, lastCharIdx + 1);

                password[i] = charGroups[nextGroupIdx][nextCharIdx];

                if (lastCharIdx == 0)
                    charsLeftInGroup[nextGroupIdx] =
                                              charGroups[nextGroupIdx].Length;
                else
                {
                    if (lastCharIdx != nextCharIdx)
                    {
                        char temp = charGroups[nextGroupIdx][lastCharIdx];
                        charGroups[nextGroupIdx][lastCharIdx] =
                                    charGroups[nextGroupIdx][nextCharIdx];
                        charGroups[nextGroupIdx][nextCharIdx] = temp;
                    }
                    charsLeftInGroup[nextGroupIdx]--;
                }

                if (lastLeftGroupsOrderIdx == 0)
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                else
                {
                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] =
                                    leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }
                    lastLeftGroupsOrderIdx--;
                }
            }

            return new string(password);
        }
    }


}

    

