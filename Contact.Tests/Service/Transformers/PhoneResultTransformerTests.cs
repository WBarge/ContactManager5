using System.Collections.Generic;
using System.Linq;
using Contact.Glue.Interfaces.DTOs;
using Contact.Service.Models.Result;
using Contact.Service.Transformers;
using Contact.Tests.TestDataFactories;
using FluentAssertions;
using NUnit.Framework;

namespace Contact.Tests.Service.Transformers
{
    [TestFixture, Description("PhoneResultTransformer tests")]
    public class PhoneResultTransformerTests
    {
        [Test, Description("Tests converting an IPhone to an PhoneResult")]
        public void Transform_ConvertsSingleObject_Success()
        {
            TestContext.Out.WriteLine("Setting up Test");
            IPhone obj = PhoneFactory.GeneratePhoneData();

            TestContext.Out.WriteLine("Executing Test");
            PhoneResult result = PhoneResultTransformer.Transform(obj);

            TestContext.Out.WriteLine("Examining results");
            result.Should().NotBeNull();
            result.Id.Should().Be(obj.PhoneId);
            result.AreaCode.Should().BeEquivalentTo(obj.AreaCode);
            result.CountryCode.Should().BeEquivalentTo(obj.CountryCode);
            result.Number.Should().BeEquivalentTo(obj.Number);
            result.PhoneType.Should().BeEquivalentTo(obj.PhoneType);
            result.Deleted.Should().Be(obj.Deleted);
        }

        [Test, Description("Test converting a collection of IEmails")]
        public void Transform_ConvertsACollection_Success()
        {
            TestContext.Out.WriteLine("Setting up Test");
            List<IPhone> testData = new() { PhoneFactory.GeneratePhoneData(), PhoneFactory.GeneratePhoneData() };

            TestContext.Out.WriteLine("Executing Test");
            IEnumerable<PhoneResult> result = PhoneResultTransformer.Transform(testData);

            TestContext.Out.WriteLine("Examining results");
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
            result.First().Id.Should().Be(testData[0].PhoneId);
            result.First().AreaCode.Should().BeEquivalentTo(testData[0].AreaCode);
        }
    }
}