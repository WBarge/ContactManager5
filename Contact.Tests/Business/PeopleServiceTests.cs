using System;
using System.Threading;
using System.Threading.Tasks;
using Contact.Business;
using Contact.Glue.Interfaces.Repos;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Contact.Tests.Business
{
    [TestFixture, Description("PeopleService tests")]
    public class PeopleServiceTests
    {

        [Test, Description("Test dependency on repo")]
        public void PeopleService_Constructor_Success()
        {
            Mock<IPersonRepo> repo = new();
            PeopleService sut = new PeopleService(repo.Object);
            sut.Should().NotBeNull();
        }

        [Test, Description("Test dependency on repo")]
        public void PeopleService_Constructor_Fail()
        {
            
            Assert.Throws<ArgumentNullException>(()=>new PeopleService(null));
        }


        [Test, Description("Test to get all people")]
        public async Task GetAllPeopleAsync_ValidateWrapper_Success()
        {
            Mock<IPersonRepo> repo = new();
            PeopleService sut = new PeopleService(repo.Object);
            await sut.GetAllPeopleAsync();

            repo.Verify(m=>m.GetAllAsync(It.IsAny<CancellationToken>()),Times.AtLeastOnce);
        }
    }
}