using System;
using System.Collections.Generic;
using System.Text;
using Contact.Glue.Extensions;
using Contact.Tests.TestDTOs;
using FluentAssertions;
using NUnit.Framework;

namespace Contact.Tests.Glue.Extensions
{
    [TestFixture, Description("StringExtensions tests")]
    public class StringExtensionsTests
    {
        [Test, Description("Test string returns true")]
        public void IsEmpty_DefaultValue_Success()
        {
            string test = default;
            bool result = test.IsEmpty();
            result.Should().BeTrue();
        }

        [Test, Description("Test string returns true")]
        public void IsEmpty_EmptyString_Success()
        {
            string test = string.Empty;
            bool result = test.IsEmpty();
            result.Should().BeTrue();
        }

        [Test, Description("Test string returns true")]
        public void IsEmpty_WhiteSpaceIsEmptyString_Success()
        {
            string test = "         ";
            bool result = test.IsEmpty();
            result.Should().BeTrue();
        }

        [Test, Description("Test string returns false")]
        public void IsEmpty_NonEmptyString_Success()
        {
            string test = "a";
            bool result = test.IsEmpty();
            result.Should().BeFalse();
        }

        [Test, Description("Test string returns false")]
        public void IsNotEmpty_NonEmptyString_Success()
        {
            string test = "A";
            bool result = test.IsNotEmpty();
            result.Should().BeTrue();
        }

        [Test, Description("Test string returns false")]
        public void IsNotEmpty_EmptyString_Success()
        {
            string test = string.Empty;
            bool result = test.IsNotEmpty();
            result.Should().BeFalse();
        }

        [Test, Description("Test Required")]
        public void Required_NonEmptyString_Success()
        {
            string test = "a";
            try
            {
                test.Required(nameof(test));
                Assert.True(true,"Test Passed");
            }
            catch (Exception)
            {
                Assert.Fail("an exception was thrown");
            }
            
        }
        [Test, Description("Test Required")]
        public void Required_EmptyString_Success()
        {
            string test = string.Empty;
            try
            {
                test.Required(nameof(test));
                Assert.Fail("an exception was not thrown");
            }
            catch (Exception)
            {
                Assert.True(true,"Test Passed");
            }
            
        }

        [Test, Description("Test Convert To Enum")]
        public void ToEnum_ConvertStringToEnum_Success()
        {
            string test = TestEnum.First.ToString();
            TestEnum result = test.ToEnum<TestEnum>(TestEnum.None);
            result.Should().Be(TestEnum.First);
        }

        [Test, Description("Test Convert To Enum")]
        public void ToEnum_ConvertStringToEnum_Fail()
        {
            string test = "a";
            TestEnum result = test.ToEnum<TestEnum>(TestEnum.None);
            result.Should().Be(TestEnum.None);
        }

        [Test, Description("Test Convert to DateTime")]
        public void ToDateTime_ConvertStringToDateTime_Success()
        {
            string test = DateTime.Now.ToString();
            DateTime result = test.ToDateTime(DateTime.MinValue);
            result.Should().NotBe(DateTime.MinValue);
        }

        [Test, Description("Test Convert to DateTime")]
        public void ToDateTime_ConvertStringToDateTime_Fail()
        {
            string test = string.Empty;
            DateTime result = test.ToDateTime(DateTime.MinValue);
            result.Should().Be(DateTime.MinValue);
        }

        [Test, Description("Test see if a string is a number")]
        public void IsNumeric_CheckWithInt_Success()
        {
            string test = 1.ToString();
            test.IsNumeric().Should().BeTrue();
        }

        [Test, Description("Test see if a string is a number")]
        public void IsNumeric_CheckWithNonInt_Success()
        {
            string test = 12342.343535.ToString();
            test.IsNumeric().Should().BeTrue();
        }

        [Test, Description("Test see if a string is a number")]
        public void IsNumeric_CheckWithNonNumeric_Success()
        {
            string test = "as";
            test.IsNumeric().Should().BeFalse();
        }

        [Test, Description("Test see if we can validate an email address")]
        public void IsValidEmail_CheckWithEmail_Success()
        {
            string test = "test@gmail.com";
            test.IsValidEmail().Should().BeTrue();
        }

        [Test, Description("Test see if we can validate an email address")]
        public void IsValidEmail_CheckWithEmail_Fail()
        {
            string test = "test";
            test.IsValidEmail().Should().BeFalse();
        }

        [Test, Description("Test see if we can validate an email address")]
        public void IsNotValidEmail_CheckWithEmail_Success()
        {
            string test = "test";
            test.IsNotValidEmail().Should().BeTrue();
        }

        [Test, Description("Test see if we can validate an email address")]
        public void IsNotValidEmail_CheckWithEmail_Fail()
        {
            string test = "test@gmail.com";
            test.IsNotValidEmail().Should().BeFalse();
        }

        [Test, Description("Test to see if string is in an array of strings")]
        public void In_LookInArray_Success()
        {
            List<string> listToSearch = new() { "a", "b", "c" };
            "b".In(listToSearch).Should().BeTrue();
        }

        [Test, Description("Test to see if string is in an array of strings")]
        public void In_LookInArray_Fail()
        {
            List<string> listToSearch = new() { "a", "b", "c" };
            "d".In(listToSearch).Should().BeFalse();
        }

        [Test,Description("Test a string to a byte array")]
        public void FromArrayToString_ValidValue_Success()
        {
            string testData = "testValue";
            byte[] test = Encoding.Unicode.GetBytes(testData);
            byte[] result = testData.ToByteArray();
            result.Should().BeEquivalentTo(test);
        }
    }
}