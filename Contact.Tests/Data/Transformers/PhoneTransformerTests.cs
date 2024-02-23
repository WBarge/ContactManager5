using Contact.Data.EF.ConcretePocos;
using Contact.Data.EF.ConcreteRepos;
using Contact.Glue.Interfaces.DTOs;
using Contact.Tests.TestDataFactories;
using FluentAssertions;
using NUnit.Framework;

namespace Contact.Tests.Data.Transformers
{
    [TestFixture, Description("PhoneTransformer tests")]
    public class PhoneTransformerTests
    {
        [Test, Description("Test TransForm")]
        public void TransForm_CreatePerson_Success()
        {
            IPhone test = PhoneFactory.GeneratePhoneData();
            Phone result = PhoneTransformer.TransForm(test);
            result.PhoneId.Should().Be(test.PhoneId);
            result.AreaCode.Should().Be(test.AreaCode);
            result.CountryCode.Should().Be(test.CountryCode);
            result.Number.Should().Be(test.Number);
            result.PhoneType.Should().Be(test.PhoneType);
            result.Deleted.Should().Be(test.Deleted);
        }
    }
}