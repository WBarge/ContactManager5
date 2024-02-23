using Contact.Glue.Extensions;
using NUnit.Framework;
using System.Text;
using FluentAssertions;

namespace Contact.Tests.Glue.Extensions
{
    [TestFixture, Description("ByteExtensions tests")]
    public class ByteExtensionsTests
    {
        [Test,Description("Test byte array to a string")]
        public void FromArrayToString_ValidValue_Success()
        {
            string testvalue = "testValue";
            byte[] testData = Encoding.Unicode.GetBytes(testvalue);
            string result = testData.FromArrayToString();
            result.Should().BeEquivalentTo(testvalue);
        }
    }
}