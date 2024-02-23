using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Contact.Glue.Interfaces.Services;
using Contact.Service.Controllers;
using Contact.Service.Models.Result;
using Contact.Tests.TestDataFactories;
using Contact.Tests.TestDTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Contact.Tests.Service.Controllers
{
    [TestFixture, Description("PeopleController tests")]
    public class PeopleControllerTests
    {
        [Test, Description("Tests the get on the controller to return a list of people")]
        public async Task GetAsync_GetPeople_Success()
        {
            await TestContext.Out.WriteLineAsync("setting up test");
            Mock<IPeopleService> service = new();

            List<Person> testData = new(){ PersonFactory.GetPerson(),PersonFactory.GetPerson()};
            service.Setup(s => s.GetAllPeopleAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(testData);

            await TestContext.Out.WriteLineAsync("Executing test");
            PeopleController sut = new PeopleController(service.Object);
            IActionResult result = await sut.GetAsync();

            await TestContext.Out.WriteLineAsync("Examining results");
            result.Should().BeOfType<OkObjectResult>();
            OkObjectResult castedResult = result as OkObjectResult;
            castedResult.Should().NotBeNull();
            castedResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);
            castedResult.Value.Should().BeOfType<List<PersonResult>>();
            IEnumerable<PersonResult> data = (IEnumerable<PersonResult>)castedResult.Value;
            data.Should().NotBeEmpty();
            data.Should().HaveCount(2);
        }
    }
}