using System.Collections.Generic;
using Contact.Glue.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace Contact.Tests.Glue.Extensions
{
    [TestFixture, Description("CollectionExtensions tests")]
    public class CollectionExtensionsTests
    {
        [Test, Description("Test IEnumerable<T> returns true")]
        public void IsEmpty_NullValue_Success()
        {
            List<string> test = null;
            bool result = test.IsEmpty();
            result.Should().BeTrue();
        }

        [Test, Description("Test IEnumerable<T> returns true")]
        public void IsEmpty_EmptyCollection_Success()
        {
            List<string> test = new();
            bool result = test.IsEmpty();
            result.Should().BeTrue();
        }

        [Test, Description("Test IEnumerable<T> returns false")]
        public void IsEmpty_NonEmptyCollection_Success()
        {
            List<string> test = new(){"fooBar"};
            bool result = test.IsEmpty();
            result.Should().BeFalse();
        }

        [Test, Description("Test IEnumerable<T> returns false")]
        public void IsNotEmpty_NonEmptyCollection_Success()
        {
            List<string> test = new(){"fooBar"};
            bool result = test.IsNotEmpty();
            result.Should().BeTrue();
        }

        [Test, Description("Test IEnumerable<T> returns false")]
        public void IsNotEmpty_EmptyCollection_Success()
        {
            List<string> test = new();
            bool result = test.IsNotEmpty();
            result.Should().BeFalse();
        }
    }
}