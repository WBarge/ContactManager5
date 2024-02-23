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
    [TestFixture, Description("EmailResultTransformer tests")]
    public class EmailResultTransformerTests
    {
        [Test, Description("Tests converting an IEmail to an EmailResult")]
        public void Transform_ConvertsSingleObject_Success()
        {
            TestContext.Out.WriteLine("Setting up Test");
            IEmail obj = EmailFactory.GenerateEmailData();

            TestContext.Out.WriteLine("Executing Test");
            EmailResult result = EmailResultTransformer.Transform(obj);

            TestContext.Out.WriteLine("Examining results");
            result.Should().NotBeNull();
            result.Id.Should().Be(obj.EmailId);
            result.Address.Should().BeEquivalentTo(obj.Address);
        }

        [Test, Description("Test converting a collection of IEmails")]
        public void Transform_ConvertsACollection_Success()
        {
            TestContext.Out.WriteLine("Setting up Test");
            List<IEmail> testData = new() { EmailFactory.GenerateEmailData(), EmailFactory.GenerateEmailData() };

            TestContext.Out.WriteLine("Executing Test");
            IEnumerable<EmailResult> result = EmailResultTransformer.Transform(testData);

            TestContext.Out.WriteLine("Examining results");
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
            result.First().Id.Should().Be(testData[0].EmailId);
            result.First().Address.Should().BeEquivalentTo(testData[0].Address);
        }
    }
}
