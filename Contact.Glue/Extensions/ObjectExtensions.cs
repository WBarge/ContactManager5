using System;
using Contact.Glue.Exceptions;

namespace Contact.Glue.Extensions
{
    /// <summary>
    /// Class ObjectExtensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Checks if the input variable is null or empty.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>true if the object is null else false.</returns>
        public static bool IsEmpty(this object obj)
        {
            return (obj == null);
        } //Empty

        /// <summary>
        /// Checks if the input variable is not empty.
        /// </summary>
        /// <param name="o">Object to be checked.</param>
        /// <returns>true if the object is not empty, else false.</returns>
        public static bool IsNotEmpty(this object o)
        {
            return !o.IsEmpty();
        } //NotEmpty

        /// <summary>
        /// Convert an object to integer.
        /// </summary>
        /// <param name="o">Object to be converted.</param>
        /// <returns>Integer value of the object or DefaultValues.DEFAULT_INT if object is not a valid int</returns>
        public static int ToInt(this object o)
        {
            return (o.ToInt(int.MinValue));
        }
        
        /// <summary>
        /// Convert an object to integer.
        /// </summary>
        /// <param name="o">Object to be converted.</param>
        /// <param name="defaultValue">the value if object is not a valid int</param>
        /// <returns>Integer value of the object or defaultValue</returns>
        public static int ToInt(this object o, int defaultValue)
        {
            int returnValue = defaultValue;
            if ((o.IsNotEmpty() && o.ToString() != string.Empty))
            {
                try
                {
                    returnValue = Convert.ToInt32(o.ToString());
                }
                catch
                {
                    // ignored
                }
            }
        
            return returnValue;
        }

        /// <summary>
        /// Checks the object to ensure it is not null.  If so an exceptions is thrown
        /// 
        /// </summary>
        /// <param name="o">The object to check</param>
        /// <param name="name">When set, it will cause the message in the exception to {name} is required</param>
        /// <exception cref="Contact.Glue.Exceptions.RequiredObjectException"></exception>
        public static void Required(this object o,string name = "")
        {
            if (o.IsEmpty())
            {
                string msg = name.IsEmpty() ? "" : $"{name} is required";
                throw new RequiredObjectException(msg);
            }
        }

    }
}
