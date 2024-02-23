using System;
using Contact.Glue.Interfaces.DTOs;
using Contact.Service.Models.Request;
using Contact.Service.Transformers;
using FluentAssertions;
using NUnit.Framework;

namespace Contact.Tests.Service.Transformers
{
    [TestFixture, Description("PersonRequestTransformer tests")]
    public class PersonRequestTransformerTests
    {
        [Test, Description("Tests converting a PersonRequest to an IPerson")]
        public void Transform_ConvertsSingleObject_Success()
        {
            TestContext.Out.WriteLine("Setting up Test");
            PersonRequest obj =GenerateRequestData();

            TestContext.Out.WriteLine("Executing Test");
            IPerson result = PersonRequestTransformer.TransForm(obj);

            TestContext.Out.WriteLine("Examining results");
            result.Should().NotBeNull();
            result.PersonId.Should().Be(obj.Id);
            result.First.Should().BeEquivalentTo(obj.FirstName);
            result.Last.Should().BeEquivalentTo(obj.LastName);
        }

        private PersonRequest GenerateRequestData()
        {
            return new PersonRequest()
            {
                Id = Guid.NewGuid(),
                FirstName = "foo",
                LastName = "bar"
            };
        }
    }
}