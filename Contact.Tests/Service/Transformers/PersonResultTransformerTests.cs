using Contact.Glue.Interfaces.DTOs;
using Contact.Service.Models.Result;
using Contact.Service.Transformers;
using Contact.Tests.TestDataFactories;
using FluentAssertions;
using NUnit.Framework;

namespace Contact.Tests.Service.Transformers
{
    [TestFixture, Description("PersonResultTransformer tests")]
    public class PersonResultTransformerTests
    {
        [Test, Description("Tests converting an IPerson to a PersonResult")]
        public void Transform_ConvertsSingleObject_Success()
        {
            TestContext.Out.WriteLine("Setting up Test");
            IPerson obj = PersonFactory.GetPerson();

            TestContext.Out.WriteLine("Executing Test");
            PersonResult result = PersonResultTransformer.TransForm(obj);

            TestContext.Out.WriteLine("Examining results");
            result.Should().NotBeNull();
            result.Id.Should().Be(obj.PersonId);
            result.FirstName.Should().BeEquivalentTo(obj.First);
            result.LastName.Should().BeEquivalentTo(obj.Last);
            result.Deleted.Should().Be(obj.Deleted);
        }
    }
    
}