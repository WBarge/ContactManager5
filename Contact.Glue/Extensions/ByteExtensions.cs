using System.Text;


namespace Contact.Glue.Extensions
{
    /// <summary>
    /// Class ByteExtensions.
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// converts the byte array to a string
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>System.String.</returns>
        public static string FromArrayToString(this byte[] bytes)
        {
            string returnValue = null;
            if (bytes.IsNotEmpty())
            {
                returnValue = Encoding.Unicode.GetString(bytes);
            }
            return returnValue;
        }
    }
}
