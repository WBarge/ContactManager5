using System;
using System.Threading;
using System.Threading.Tasks;
using Contact.Business;
using Contact.Glue.Interfaces.DTOs;
using Contact.Glue.Interfaces.Repos;
using Contact.Tests.TestDataFactories;
using CrossCutting.Exceptions;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Contact.Tests.Business
{
    [TestFixture, Description("PersonService tests")]
    public class PersonServiceTests
    {
        [Test, Description("Test dependency on repo")]
        public void PersonService_Constructor_Success()
        {
            Mock<IPersonRepo> repo = new();
            PersonService sut = new PersonService(repo.Object);
            sut.Should().NotBeNull();
        }

        [Test, Description("Test dependency on repo")]
        public void PersonService_Constructor_Fail()
        {
            Assert.Throws<ArgumentNullException>(()=>new PersonService(null));
        }

        [Test, Description("Test Getting a person")]
        public async Task GetAPerson_WrapperTest_Success()
        {
            Mock<IPersonRepo> repo = new();
            PersonService sut = new PersonService(repo.Object);
            await sut.GetAPerson(Guid.NewGuid());

            repo.Verify(m=>m.GetPersonAsync(It.IsAny<Guid>(),It.IsAny<CancellationToken>()),Times.Once);
        }

        [Test, Description("Test adding a person")]
        public async Task UpSertAsync_AddingAPerson_Success()
        {
            Mock<IPersonRepo> repo = new();

            IPerson person = PersonFactory.GetPerson();
            person.PersonId = Guid.Empty;

            PersonService sut = new PersonService(repo.Object);
            await sut.UpSertAsync(person);

            repo.Verify(m=>m.InsertAsync(It.IsAny<IPerson>(),It.IsAny<CancellationToken>()),Times.Once);
        }

        [Test, Description("Test updating a person")]
        public async Task UpSertAsync_UpdatingAPerson_Success()
        {
            Mock<IPersonRepo> repo = new();

            IPerson person = PersonFactory.GetPerson();

            PersonService sut = new PersonService(repo.Object);
            await sut.UpSertAsync(person);

            repo.Verify(m=>m.UpdateAsync(It.IsAny<IPerson>(),It.IsAny<CancellationToken>()),Times.Once);
        }

        [Test, Description("Test Deleting a person")]
        public async Task DeleteAPersonAsync_DeletingAPerson_Success()
        {
            Mock<IPersonRepo> repo = new();

            IPerson person = PersonFactory.GetPerson();
            repo.Setup(m => m.GetPersonAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(person);

            PersonService sut = new PersonService(repo.Object);
            await sut.DeleteAPersonAsync(Guid.NewGuid());

            repo.Verify(m=>m.GetPersonAsync(It.IsAny<Guid>(),It.IsAny<CancellationToken>()),Times.Once);
            repo.Verify(m=>m.DeleteAsync(person,It.IsAny<CancellationToken>()),Times.Once);
        }

        [Test, Description("Test Deleting a person")]
        public void DeleteAPersonAsync_DeletingAPerson_Fail()
        {
            Mock<IPersonRepo> repo = new();

            PersonService sut = new PersonService(repo.Object);
            AsyncTestDelegate act = () => sut.DeleteAPersonAsync(Guid.NewGuid());

            // Assert
            Assert.That(act, Throws.TypeOf<RequestException>());

            repo.Verify(m=>m.GetPersonAsync(It.IsAny<Guid>(),It.IsAny<CancellationToken>()),Times.Once);
            repo.Verify(m=>m.DeleteAsync(It.IsAny<IPerson>(),It.IsAny<CancellationToken>()),Times.Never);
        }
    }
}