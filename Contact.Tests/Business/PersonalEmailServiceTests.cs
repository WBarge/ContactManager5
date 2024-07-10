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
    [TestFixture, Description("PersonalEmailService tests")]
    public class PersonalEmailServiceTests
    {
        [Test, Description("Test dependency on repo")]
        public void PersonalEmailService_Constructor_Success()
        {
            Mock<IPersonalEmailRepo> repo = new();
            PersonalEmailService sut = new PersonalEmailService(repo.Object);
            sut.Should().NotBeNull();
        }

        [Test, Description("Test dependency on repo")]
        public void PersonalEmailService_Constructor_Fail()
        {
            
            Assert.Throws<ArgumentNullException>(()=>new PersonalEmailService(null));
        }


        [Test, Description("Test to get all emails for a person")]
        public async Task GetAllPeopleAsync_ValidateWrapper_Success()
        {
            Mock<IPersonalEmailRepo> repo = new();
            PersonalEmailService sut = new PersonalEmailService(repo.Object);
            await sut.GetEmailsForAPersonAsync(Guid.NewGuid());

            repo.Verify(m=>m.GetEmailsForAPersonAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),Times.AtLeastOnce);
        }

        [Test, Description("Test to add an email to a person")]
        public async Task AddAddressToAPersonAsync_ValidateWrapper_Success()
        {
            Mock<IPersonalEmailRepo> repo = new();
            PersonalEmailService sut = new PersonalEmailService(repo.Object);
            await sut.AddAddressToAPersonAsync(Guid.NewGuid(),"someone@gmail.com");

            repo.Verify(m=>m.AddAnEmailToAPerson(It.IsAny<Guid>(),It.IsAny<string>(), It.IsAny<CancellationToken>()),Times.AtLeastOnce);
        }

        [Test, Description("Test delete an email")]
        public async Task DeleteEmailAsync_ValidateWrapper_Success()
        {
            Mock<IPersonalEmailRepo> repo = new();
            PersonalEmailService sut = new PersonalEmailService(repo.Object);
            await sut.DeleteEmailAsync(Guid.NewGuid());

            repo.Verify(m=>m.DeleteEmailAsync(It.IsAny<Guid>(),It.IsAny<CancellationToken>()),Times.Once);

        }
    }
}