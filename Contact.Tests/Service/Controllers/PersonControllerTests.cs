using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Contact.Glue.Interfaces.DTOs;
using Contact.Glue.Interfaces.Services;
using Contact.Service.Controllers;
using Contact.Service.Models.Request;
using Contact.Service.Models.Result;
using Contact.Tests.TestDataFactories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Contact.Tests.Service.Controllers
{
    [TestFixture, Description("PersonController tests")]
    public class PersonControllerTests
    {
        [Test, Description("Test the get on the controller to return a person")]
        public async Task GetAsync_GetEmails_Success()
        {
            await TestContext.Out.WriteLineAsync("setting up test");
            Mock<IPersonService> service = new();
            IPerson testData = PersonFactory.GetPerson();
            service.Setup(s => s.GetAPerson(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(testData);

            await TestContext.Out.WriteLineAsync("executing test");
            PersonController sut = new(service.Object);
            IActionResult result = await sut.GetAsync(Guid.NewGuid());

            await TestContext.Out.WriteLineAsync("Examining results");
            result.Should().BeOfType<OkObjectResult>();
            OkObjectResult castedResult = result as OkObjectResult;
            castedResult.Should().NotBeNull();
            castedResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);
            castedResult.Value.Should().BeOfType<PersonResult>();
            PersonResult data = (PersonResult)castedResult.Value;
            data.Should().NotBeNull();
            data.Id.Should().Be(testData.PersonId);
            data.FirstName.Should().Be(testData.First);
            data.LastName.Should().Be(testData.Last);
            data.Deleted.Should().Be(testData.Deleted);
        }
        [Test, Description("Test the post on the controller to insert a person")]
        public async Task PostAsync_AddPerson_Success()
        {
            await TestContext.Out.WriteLineAsync("setting up test");
            Mock<IPersonService> service = new();

            await TestContext.Out.WriteLineAsync("executing test");
            PersonController sut = new(service.Object);
            IActionResult result = await sut.PostAsync(new PersonRequest() { Id = Guid.NewGuid(), FirstName = "foo",LastName = "bar"});

            await TestContext.Out.WriteLineAsync("examining results");
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            OkObjectResult castedResult = result as OkObjectResult;
            castedResult.Should().NotBeNull();
            castedResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);
            castedResult.Value.Should().NotBeNull();
            castedResult.Value.Should().BeOfType<Guid>();

            service.Verify();
        }

        [Test, Description("Test the post on the controller to delete a person")]
        public async Task PostAsync_DeletePerson_Success()
        {
            await TestContext.Out.WriteLineAsync("setting up test");
            Mock<IPersonService> service = new();

            await TestContext.Out.WriteLineAsync("executing test");
            PersonController sut = new(service.Object);
            IActionResult result = await sut.Delete(Guid.NewGuid());

            await TestContext.Out.WriteLineAsync("examining results");
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();

            service.Verify();
        }
    }

}