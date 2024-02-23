using Contact.Data.EF.ConcretePocos;
using Contact.Data.EF.Transformers;
using Contact.Glue.Interfaces.DTOs;
using Contact.Tests.TestDataFactories;
using FluentAssertions;
using NUnit.Framework;

namespace Contact.Tests.Data.Transformers
{
    [TestFixture, Description("PersonTransformer tests")]
    public class PersonTransformerTests
    {
        [Test, Description("Test TransForm")]
        public void TransForm_CreatePerson_Success()
        {
            IPerson test = PersonFactory.GetPerson();
            Person result = PersonTransformer.TransForm(test);
            result.PersonId.Should().Be(test.PersonId);
            result.First.Should().Be(test.First);
            result.Last.Should().Be(test.Last);
            result.Deleted.Should().Be(test.Deleted);
        }
    }
}