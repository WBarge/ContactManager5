using Contact.Business;
using Contact.Glue.Interfaces.Repos;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;

namespace Contact.Tests.Business
{
    [TestFixture, Description("PersonalPhoneService tests")]
    public class PersonalPhoneServiceTests
    {
        [Test, Description("Test dependency on repo")]
        public void PersonalPhoneService_Constructor_Success()
        {
            Mock<IPersonalPhoneRepo> repo = new();
            PersonalPhoneService sut = new PersonalPhoneService(repo.Object);
            sut.Should().NotBeNull();
        }

        [Test, Description("Test dependency on repo")]
        public void PersonalPhoneService_Constructor_Fail()
        {
            Assert.Throws<ArgumentNullException>(()=>new PersonalEmailService(null));
        }

        [Test, Description("Test Get phone numbers for a person")]
        public async Task GetPhoneNumbersForAPersonAsync_ValidateWrapper_Success()
        {
            Mock<IPersonalPhoneRepo> repo = new();
            PersonalPhoneService sut = new PersonalPhoneService(repo.Object);
            await sut.GetPhoneNumbersForAPersonAsync(Guid.NewGuid());

            repo.Verify(m=>m.GetAPersonsPhoneNumbersAsync(It.IsAny<Guid>(),It.IsAny<CancellationToken>()),Times.AtLeastOnce);
        }

        [Test, Description("Test Add a phone numbers for a person")]
        public async Task AddPhoneNumberToAPersonAsync_Add_Success()
        {
            Mock<IPersonalPhoneRepo> repo = new();
            repo.Setup(m => m.DoesPersonHaveNumberAsync(It.IsAny<Guid>(), It.IsAny<string>(),
                It.IsAny<string>(),It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);


            PersonalPhoneService sut = new PersonalPhoneService(repo.Object);
            await sut.AddPhoneNumberToAPersonAsync(Guid.NewGuid(),string.Empty,string.Empty,string.Empty,string.Empty);

            repo.Verify(m=>
                m.AddPhoneNumberToPerson(It.IsAny<Guid>(), It.IsAny<string>(), 
                    It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(),It.IsAny<CancellationToken>()),
                Times.AtLeastOnce);

            repo.Verify(m => 
                m.DoesPersonHaveNumberAsync(It.IsAny<Guid>(), It.IsAny<string>(),
                It.IsAny<string>(),It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()),
                Times.AtLeastOnce);
        }

        [Test, Description("Test Add a phone numbers for a person")]
        public async Task AddPhoneNumberToAPersonAsync_Add_Fail()
        {
            Mock<IPersonalPhoneRepo> repo = new();
            repo.Setup(m => m.DoesPersonHaveNumberAsync(It.IsAny<Guid>(), It.IsAny<string>(),
                    It.IsAny<string>(),It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);


            PersonalPhoneService sut = new PersonalPhoneService(repo.Object);
            await sut.AddPhoneNumberToAPersonAsync(Guid.NewGuid(),string.Empty,string.Empty,string.Empty,string.Empty);

            repo.Verify(m=>
                    m.AddPhoneNumberToPerson(It.IsAny<Guid>(), It.IsAny<string>(), 
                        It.IsAny<string>(), It.IsAny<string>(),
                        It.IsAny<string>(),It.IsAny<CancellationToken>()),
                Times.Never);

            repo.Verify(m => 
                    m.DoesPersonHaveNumberAsync(It.IsAny<Guid>(), It.IsAny<string>(),
                        It.IsAny<string>(),It.IsAny<string>(), It.IsAny<string>(),
                        It.IsAny<CancellationToken>()),
                Times.AtLeastOnce);
        }

        [Test,Description("Test Setting a phone number as the default number for a person")]
        public async Task TestSetDefaultNumber_Success()
        {
            Mock<IPersonalPhoneRepo> repo = new();


            PersonalPhoneService sut = new PersonalPhoneService(repo.Object);
            await sut.SetDefaultNumberAsync(Guid.NewGuid(),Guid.NewGuid());

            repo.Verify(m=>
                    m.SetDefaultPhoneNumberAsync(It.IsAny<Guid>(),It.IsAny<Guid>(),It.IsAny<CancellationToken>()),
                Times.AtLeastOnce);
        }
    }
}