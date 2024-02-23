using Contact.Glue.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Contact.Glue.Extensions
{
    /// <summary>
    /// Class StringExtensions.
    /// </summary>
    public static class StringExtensions
    {

        /// <summary>
        /// Convert the string to an enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str">The string to convert.</param>
        /// <param name="defaultValue">The default value to set the enum to if the string can not be converted.</param>
        /// <returns>The converted value or the default value if conversion fails</returns>
        public static T ToEnum<T>(this string str, T defaultValue) where T : Enum
        {
            T returnValue = defaultValue;

            if (str.IsEmpty()) return returnValue;
            if (Enum.IsDefined(defaultValue.GetType(), str) == false)
            {
                int val = str.ToInt(int.MinValue);
                if (val != int.MinValue && Enum.IsDefined(defaultValue.GetType(), val))
                {
                    returnValue = (T)Enum.Parse(defaultValue.GetType(), str, false);
                }
            }
            else
            {
                returnValue = (T)Enum.Parse(defaultValue.GetType(), str, true);
            }

            return returnValue;
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="str">The date as a string.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The converted value of the default value if conversion fails</returns>
        public static DateTime ToDateTime(this string str, DateTime defaultValue)
        {
            DateTime returnValue = defaultValue;
            if (DateTime.TryParse(str, out DateTime result))
            {
                returnValue = result;
            }
            return (returnValue);
        }

        /// <summary>
        /// Determines whether the specified string is null or empty.
        /// </summary>
        /// <param name="s">The string to examine.</param>
        /// <returns><c>true</c> if the specified string is empty or null; otherwise, <c>false</c>.</returns>
        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }//Empty

        /// <summary>
        /// Checks if the input string is not empty.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>true if the string is not empty, else false.</returns>
        public static bool IsNotEmpty(this string s)
        {
            return (!s.IsEmpty());
        }//NotEmpty

        /// <summary>
        /// Method to check for all decimal digits in a string
        /// </summary>
        /// <param name="number">the string to check</param>
        /// <returns>true - the string is all numbers
        /// false - the string is not all numbers</returns>
        public static bool IsNumeric(this string number)
        {
            bool returnValue;
            try
            {
                NumberFormatInfo format = new NumberFormatInfo();
                returnValue = double.TryParse(number, NumberStyles.Number, format, out double unused);
            }
            catch { returnValue = false; }
            return returnValue;
        }//end of IsNumeric

        /// <summary>
        /// Method that verifies the email format is correct
        /// </summary>
        /// <param name="email">email address to check</param>
        /// <returns>true - the email is in a valid format
        /// false - the email is not a valid email format</returns>
        public static bool IsValidEmail(this string email)
        {
            bool returnValue = false;
            const string REGEX_LOCAL_PART1 = @"(([-!$&*=^`|~#%'+/?_`{}\w]+)(\.([-!$&*=^`|~#%'+/?_`{}\w])+)*)";
            const string REGEX_LOCAL_PART2 = @"(""([^""\r]|\\[""\r\\])+"")";
            const string REGEX_DOMAIN = @"@([\w]+[-.])*[\w]+[.]([a-zA-Z]{2,9})";
            try
            {
                string trimmedEmail = email.Trim();
                string regex = $@"^({REGEX_LOCAL_PART1}|{REGEX_LOCAL_PART2}){REGEX_DOMAIN}$";
                Regex reg = new Regex(regex, RegexOptions.None);
                returnValue = reg.Match(trimmedEmail).Success;
            }
            catch
            {
                // ignored
            }

            return returnValue;
        }//IsValidEmail

        /// <summary>
        /// Method to see if the email address is not in the correct format
        /// </summary>
        /// <param name="email">email address to check</param>
        /// <returns>true - the email is not a valid email format
        /// false - the email is in a valid format</returns>
        public static bool IsNotValidEmail(this string email)
        {
            return (!email.IsValidEmail());
        }

        /// <summary>
        /// Looks for the string in the specified list
        /// </summary>
        /// <param name="stringToFind">The string to find.</param>
        /// <param name="listToSearch">The list to search.</param>
        /// <returns><c>true</c> if string was found, <c>false</c> otherwise.</returns>
        public static bool In(this string stringToFind, IEnumerable<string> listToSearch)
        {
            return listToSearch.Any(tempStr => tempStr == stringToFind);
        }

        /// <summary>
        /// Converts a string into a byte array using unicode encoding
        /// </summary>
        /// <param name="stringToEncode">The string to encode.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] ToByteArray(this string stringToEncode)
        {
            byte[] returnValue = null;
            if (stringToEncode.IsNotEmpty())
            {
                returnValue = Encoding.Unicode.GetBytes(stringToEncode);
            }
            return returnValue;
        }

        /// <summary>
        /// Checks the string to ensure it is not null.  If so an exceptions is thrown
        /// </summary>
        /// <param name="o"></param>
        /// <param name="name"></param>
        /// <exception cref="RequiredObjectException"></exception>
        public static void Required(this string o,string name = "")
        {
            if (o.IsEmpty())
            {
                string msg = name.IsEmpty() ? "" : $"{name} is required";
                throw new RequiredObjectException(msg);
            }
        }

    }
}
