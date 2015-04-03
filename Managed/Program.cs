using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managed
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(string.Format("Start: [{0}], End: [{1}]", args[0], modifyString(args[0])));
#if false
            Console.WriteLine(string.Format("The next sparse number of {0:X} is {1:X}",
                                args[0], findNextSparse(uint.Parse(args[0], System.Globalization.NumberStyles.AllowHexSpecifier))));
            Console.WriteLine(args[0] + (canBePalindrome(args[0]) ? " can " : " cannot ") + "be a palindrome");
#endif
        }

        /// <summary>
        /// Modifies string by changing odd words to uppercase and reversing even words
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        static string modifyString(string inputStr)
        {
            bool fEven = false;
            int startIndex = 0;
            string retString = string.Empty;

            while (startIndex < inputStr.Length)
            {
                int endIndex = startIndex;

                // Scan for white space
                while ((endIndex < inputStr.Length) && char.IsWhiteSpace(inputStr[endIndex]))
                    endIndex++;

                // If found, copy to string and advance start
                if (endIndex > startIndex)
                {
                    retString += inputStr.Substring(startIndex, endIndex - startIndex);
                    startIndex = endIndex;
                }

                while ((endIndex < inputStr.Length) && !char.IsWhiteSpace(inputStr[endIndex]))
                    endIndex++;

                if (endIndex > startIndex)
                {
                    if (fEven)
                    {
                        char[] array = inputStr.Substring(startIndex, endIndex - startIndex).ToCharArray();
                        Array.Reverse(array);
                        retString += new string(array);
                    }
                    else
                    {
                        retString += inputStr.Substring(startIndex, endIndex - startIndex).ToUpper();
                    }
                    fEven = !fEven;
                    startIndex = endIndex;
                }
            }

            return retString;
        }

        /// <summary>
        /// Finds the next sparse number given any unsigned integer.  A sparse number is defined to be one in 
        /// which the bit representation doesn't have two successive bits set
        /// </summary>
        /// <param name="num">Input number</param>
        /// <returns>Next sparse number</returns>
        static uint findNextSparse(uint num)
        {
            // Scan for high order bit
            int highBit = sizeof(uint) * 8;
            while (--highBit >= 0)
            {
                if ((((uint)0x01 << highBit) & num) != 0)
                    break;
            }

            // Work backwords until we find either 00 or 11
            bool lastSet = true;
            int indexBit = highBit;
            while (--indexBit >= 0)
            {
                bool bitSet = ((((uint)0x01 << indexBit) & num) != 0);
                if (bitSet == lastSet)
                    break;
                else
                    lastSet = bitSet;
            }

            if ((indexBit < 0) || lastSet)
            {
                // Set next high order bit, return number
                if (highBit == sizeof(uint) * 8)
                    return 0;
                else
                    return ((uint) 0x01) << (highBit + 1);
            }
            else
            {
                // Set bit, turn off all lower order bits
                uint tempReturn = num | ((uint)0x01 << indexBit);

                // Mask off lower bits - just shift left and then right again
                tempReturn = tempReturn >> indexBit;
                tempReturn = tempReturn << indexBit;

                return tempReturn;
            }
        }

        static bool canBePalindrome(string str)
        {
            bool[] usedOdd = new bool[26];

            if (str.Length == 0)
                return false;

            foreach (char ch in str.ToLower())
            {
                if ((ch < 'a') || (ch > 'z'))
                    return false;

                int index = ch - 'a';
                usedOdd[index] = !usedOdd[index];
            }

            bool foundOdd = false;
            for (int index = 0; index < 26; index++)
            {
                if (usedOdd[index])
                {
                    if (foundOdd)
                        return false;
                    foundOdd = true;
                }
            }
            return true;
        }
    }
}
