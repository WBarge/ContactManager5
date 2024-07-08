using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Contact.Glue.Exceptions;
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
    [TestFixture, Description("PersonalPhoneNumberController tests")]
    public class PersonalPhoneNumberControllerTests
    {
        [Test, Description("Test the get on the controller to return a list of phone numbers for a person")]
        public async Task GetAsync_GetEmails_Success()
        {
            await TestContext.Out.WriteLineAsync("setting up test");
            Mock<IPersonalPhoneService> service = new();
            List<IPhone> testData = new() { PhoneFactory.GeneratePhoneData(), PhoneFactory.GeneratePhoneData() };
            service.Setup(s => s.GetPhoneNumbersForAPersonAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(testData);

            await TestContext.Out.WriteLineAsync("executing test");
            PersonalPhoneNumberController sut = new(service.Object);
            IActionResult result = await sut.GetAsync(Guid.NewGuid());

            await TestContext.Out.WriteLineAsync("Examining results");
            result.Should().BeOfType<OkObjectResult>();
            OkObjectResult castedResult = result as OkObjectResult;
            castedResult.Should().NotBeNull();
            castedResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);
            castedResult.Value.Should().BeOfType<List<PhoneResult>>();
            IEnumerable<PhoneResult> data = (IEnumerable<PhoneResult>)castedResult.Value;
            data.Should().NotBeEmpty();
            data.Should().HaveCount(2);
        }

        [Test, Description("Test the post on the controller to insert a phone number for a person")]
        public async Task PostAsync_AddEmail_Success()
        {
            await TestContext.Out.WriteLineAsync("setting up test");
            Mock<IPersonalPhoneService> service = new();

            await TestContext.Out.WriteLineAsync("executing test");
            PersonalPhoneNumberController sut = new(service.Object);
            IActionResult result = await sut.PostAsync(new PhoneNumberRequest() { PersonId = Guid.NewGuid(), AreaCode = "231",Number = "5551212"});

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
            Mock<IPersonalPhoneService> service = new();
        
            await TestContext.Out.WriteLineAsync("executing test");
            PersonalPhoneNumberController sut = new(service.Object);
            try
            {
                await sut.PostAsync(new PhoneNumberRequest(){  AreaCode = "231",Number = "5551212"});
            }
            catch (RequiredObjectException)
            {
                await TestContext.Out.WriteLineAsync("an exception happened as expected");
            }
        
            await TestContext.Out.WriteLineAsync("validating calls did not happen");
            service.Verify(m => 
                m.AddPhoneNumberToAPersonAsync(It.IsAny<Guid>(),It.IsAny<string>(),
                    It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>(),
                    It.IsAny<CancellationToken>()), Times.Never);
        
        }

        [Test, Description("Test the post on the controller to see if the required area code is handled")]
        public async Task PostAsync_AreaCodeIsRequired_Success()
        {
            await TestContext.Out.WriteLineAsync("setting up test");
            Mock<IPersonalPhoneService> service = new();
        
            await TestContext.Out.WriteLineAsync("executing test");
            PersonalPhoneNumberController sut = new(service.Object);
            try
            {
                await sut.PostAsync(new PhoneNumberRequest(){ PersonId = Guid.NewGuid(),Number = "5551212"});
            }
            catch (RequiredObjectException)
            {
                await TestContext.Out.WriteLineAsync("an exception happened as expected");
            }
        
            await TestContext.Out.WriteLineAsync("validating calls did not happen");
            service.Verify(m => 
                m.AddPhoneNumberToAPersonAsync(It.IsAny<Guid>(),It.IsAny<string>(),
                    It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>(),
                    It.IsAny<CancellationToken>()), Times.Never);

        }

        [Test, Description("Test the post on the controller to see if the required number is handled")]
        public async Task PostAsync_NumberIsRequired_Success()
        {
            await TestContext.Out.WriteLineAsync("setting up test");
            Mock<IPersonalPhoneService> service = new();
        
            await TestContext.Out.WriteLineAsync("executing test");
            PersonalPhoneNumberController sut = new(service.Object);
            try
            {
                await sut.PostAsync(new PhoneNumberRequest(){ PersonId = Guid.NewGuid(), AreaCode = "123"});
            }
            catch (RequiredObjectException)
            {
                await TestContext.Out.WriteLineAsync("an exception happened as expected");
            }
        
            await TestContext.Out.WriteLineAsync("validating calls did not happen");
            service.Verify(m => 
                m.AddPhoneNumberToAPersonAsync(It.IsAny<Guid>(),It.IsAny<string>(),
                    It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>(),
                    It.IsAny<CancellationToken>()), Times.Never);

        }

        [Test, Description("Test the a post on the controller to set the default phone number")]
        public async Task PostAsync_SetDefaultNumber_Success()
        {
            await TestContext.Out.WriteLineAsync("setting up test");
            Mock<IPersonalPhoneService> service = new();

            await TestContext.Out.WriteLineAsync("executing test");
            PersonalPhoneNumberController sut = new(service.Object);
            IActionResult result = await sut.SetDefaultNumber(Guid.NewGuid(), Guid.NewGuid());

            await TestContext.Out.WriteLineAsync("examining results");
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
            OkResult castedResult = result as OkResult;
            castedResult.Should().NotBeNull();
            castedResult!.StatusCode.Should().Be((int)HttpStatusCode.OK);

            service.Verify();
        }
    }
}