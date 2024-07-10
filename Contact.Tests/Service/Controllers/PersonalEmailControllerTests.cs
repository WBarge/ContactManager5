using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Contact.Glue.Interfaces.DTOs;
using Contact.Glue.Interfaces.Services;
using Contact.Service.Controllers;
using Contact.Service.Models.Request;
using Contact.Service.Models.Result;
using Contact.Tests.TestDataFactories;
using CrossCutting.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Contact.Tests.Service.Controllers
{
    [TestFixture, Description("PersonalEmailController tests")]
    public class PersonalEmailControllerTests
    {
        [Test, Description("Test the get on the controller to return a list of email addresses for a person")]
        public async Task GetAsync_GetEmails_Success()
        {
            await TestContext.Out.WriteLineAsync("setting up test");
            Mock<IPersonalEmailService> service = new();
            List<IEmail> testData = new() { EmailFactory.GenerateEmailData(), EmailFactory.GenerateEmailData() };
            service.Setup(s => s.GetEmailsForAPersonAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(testData);

            await TestContext.Out.WriteLineAsync("executing test");
            PersonalEmailController sut = new(service.Object);
            IActionResult result = await sut.GetAsync(Guid.NewGuid());

            await TestContext.Out.WriteLineAsync("Examining results");
            result.Should().BeOfType<OkObjectResult>();
            OkObjectResult castedResult = result as OkObjectResult;
            castedResult.Should().NotBeNull();
            castedResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);
            castedResult.Value.Should().BeOfType<List<EmailResult>>();
            IEnumerable<EmailResult> data = (IEnumerable<EmailResult>)castedResult.Value;
            data.Should().NotBeEmpty();
            data.Should().HaveCount(2);
        }

        [Test, Description("Test the post on the controller to insert a new email for a person")]
        public async Task PostAsync_AddEmail_Success()
        {
            await TestContext.Out.WriteLineAsync("setting up test");
            Mock<IPersonalEmailService> service = new();

            await TestContext.Out.WriteLineAsync("executing test");
            PersonalEmailController sut = new(service.Object);
            IActionResult result = await sut.PostAsync(new AddEmailRequest() { PersonId = Guid.NewGuid(), Address = "test@gmail.com" });

            await TestContext.Out.WriteLineAsync("examining results");
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
            OkResult castedResult = result as OkResult;
            castedResult.Should().NotBeNull();
            castedResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);

            service.Verify();
        }

        [Test, Description("Test the post on the controller to see if the required person id is handled")]
        public async Task PostAsync_PersonIdIsRequired_Success()
        {
            await TestContext.Out.WriteLineAsync("setting up test");
            Mock<IPersonalEmailService> service = new();

            await TestContext.Out.WriteLineAsync("executing test");
            PersonalEmailController sut = new(service.Object);
            try
            {
                await sut.PostAsync(new AddEmailRequest(){Address = "some@gmail.com"});
            }
            catch (RequiredObjectException)
            {
                await TestContext.Out.WriteLineAsync("an exception happened as expected");
            }

            await TestContext.Out.WriteLineAsync("validating calls did not happen");
            service.Verify(m => m.AddAddressToAPersonAsync(It.IsAny<Guid>(),It.IsAny<string>(),It.IsAny<CancellationToken>()), Times.Never);

        }

        [Test, Description("Test the post on the controller to see if the required address is handled")]
        public async Task PostAsync_AddressIsRequired_Success()
        {
            await TestContext.Out.WriteLineAsync("setting up test");
            Mock<IPersonalEmailService> service = new();

            await TestContext.Out.WriteLineAsync("executing test");
            PersonalEmailController sut = new(service.Object);
            try
            {
                await sut.PostAsync(new AddEmailRequest(){PersonId = Guid.NewGuid()});
            }
            catch (RequiredObjectException)
            {
                await TestContext.Out.WriteLineAsync("an exception happened as expected");
            }

            await TestContext.Out.WriteLineAsync("validating calls did not happen");
            service.Verify(m => m.AddAddressToAPersonAsync(It.IsAny<Guid>(),It.IsAny<string>(),It.IsAny<CancellationToken>()), Times.Never);

        }

        [Test, Description("Test Delete email")]
        public async Task DeleteAsync_EmailIdIsRequired_Success()
        {
            await TestContext.Out.WriteLineAsync("setting up test");
            Mock<IPersonalEmailService> service = new();

            await TestContext.Out.WriteLineAsync("executing test");
            PersonalEmailController sut = new(service.Object);
            try
            {
                await sut.DeleteAsync(Guid.Empty);
            }
            catch (RequiredObjectException)
            {
                await TestContext.Out.WriteLineAsync("an exception happened as expected");
            }

            await TestContext.Out.WriteLineAsync("validating calls did not happen");
            service.Verify(m => m.DeleteEmailAsync(It.IsAny<Guid>(),It.IsAny<CancellationToken>()), Times.Never);

        }

        [Test, Description("Test Delete email")]
        public async Task DeleteAsync_HappyPath_Success()
        {
            await TestContext.Out.WriteLineAsync("setting up test");
            Mock<IPersonalEmailService> service = new();

            await TestContext.Out.WriteLineAsync("executing test");
            PersonalEmailController sut = new(service.Object);
            try
            {
                await sut.DeleteAsync(Guid.NewGuid());
            }
            catch (RequiredObjectException x)
            {
                await TestContext.Out.WriteLineAsync(x.Message);
                Assert.Fail("An exception happened and it should not have");
            }

            await TestContext.Out.WriteLineAsync("validating calls did happen");
            service.Verify(m => m.DeleteEmailAsync(It.IsAny<Guid>(),It.IsAny<CancellationToken>()), Times.Once);

        }

    }
}