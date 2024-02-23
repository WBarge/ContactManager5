using System.Collections.Generic;
using System.Linq;

namespace Contact.Glue.Extensions
{
    /// <summary>
    /// Class CollectionExtensions.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Determines whether the specified object is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns><c>true</c> if the specified object is empty; otherwise, <c>false</c>.</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> obj)
        {
            return ((object)obj).IsEmpty() || !obj.Any();
        }

        /// <summary>
        /// Determines whether [is not empty] [the specified object].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns><c>true</c> if [is not empty] [the specified object]; otherwise, <c>false</c>.</returns>
        public static bool IsNotEmpty<T>(this IEnumerable<T> obj)
        {
            return (!obj.IsEmpty());
        }//Empty

    }
}
