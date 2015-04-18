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
            string arg0;
            string arg1;

            Console.WriteLine("Enter primary string: ");
            arg0 = Console.ReadLine();
            Console.WriteLine("Enter pattern string: ");
            arg1 = Console.ReadLine();

            Console.WriteLine(string.Format("Pattern [{0}] {1} string [{2}]",
                arg1, checkMatch(arg0, arg1) ? "matches" : "doesn't match", arg0
                ));

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
#if false
            Console.WriteLine(string.Format("Start: [{0}], End: [{1}]", args[0], modifyString(args[0])));
            Console.WriteLine(string.Format("The next sparse number of {0:X} is {1:X}",
                                args[0], findNextSparse(uint.Parse(args[0], System.Globalization.NumberStyles.AllowHexSpecifier))));
            Console.WriteLine(args[0] + (canBePalindrome(args[0]) ? " can " : " cannot ") + "be a palindrome");
#endif
        }

        /// <summary>
        /// Checks whether string conforms to regular expression
        ///   Expression can contain:
        ///     '*' - matches one or more of any char
        ///     '.' - matches any char
        /// </summary>
        /// <param name="InputStr"></param>
        /// <param name="Pattern"></param>
        /// <returns></returns>
        static bool checkMatch(string InputStr, string Pattern)
        {
            bool wildcardMode = false;
            
            int baseStrIndex = 0, strIndex = 0;
            int basePatIndex = 0, curPatIndex = 0;

            while ((strIndex < InputStr.Length) && (curPatIndex < Pattern.Length))
            {
                if (Pattern[curPatIndex] == '*')
                {
                    wildcardMode = true;
                    basePatIndex = ++curPatIndex;
                    baseStrIndex = strIndex;
                }
                else if ((Pattern[curPatIndex] == InputStr[strIndex]) ||
                    (Pattern[curPatIndex] == '.'))
                {
                    strIndex++;
                    curPatIndex++;
                }
                else if (wildcardMode)
                {
                    // We didn't match, but we move back and keep trying
                    curPatIndex = basePatIndex;
                    strIndex = baseStrIndex + 1;
                }
                else
                {
                    // Not in wildcard mode and strings don't match - return false
                    return false;
                }
            }

            // Conditions
            // Pattern out and string out - good
            //  Pattern out - last saw * in pattern - good, otherwise bad
            //  string out - only '*' left in pattern good, otherwise bad
            if (curPatIndex >= Pattern.Length)
            {
                if (strIndex >= InputStr.Length)
                    return true;
                else
                    return (Pattern[Pattern.Length - 1] == '*');
            }
            else
            {
                for (int index = curPatIndex; index < Pattern.Length; index++)
                {
                    if (Pattern[index] != '*')
                        return false;
                }
            }
            return true;
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
