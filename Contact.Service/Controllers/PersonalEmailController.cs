// ***********************************************************************
// Assembly         : Contact.Service
// Author           : Admin
// Created          : 03-28-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="PersonalEmailController.cs" company="Contact.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contact.Glue.Interfaces.DTOs;
using Contact.Glue.Interfaces.Services;
using Contact.Service.Models.Request;
using Contact.Service.Models.Result;
using Contact.Service.Transformers;
using CrossCutting.Extensions;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Contact.Service.Controllers
{
    /// <summary>
    /// Class PersonalEmailController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalEmailController : ControllerBase
    {
        /// <summary>
        /// The personal email service
        /// </summary>
        private readonly IPersonalEmailService _personalEmailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalEmailController"/> class.
        /// </summary>
        /// <param name="personalEmailService">The personal email service.</param>
        /// <exception cref="ArgumentNullException">personalEmailService</exception>
        public PersonalEmailController(IPersonalEmailService personalEmailService)
        {
            _personalEmailService = personalEmailService ?? throw new ArgumentNullException(nameof(personalEmailService) + " is mandatory");
        }


        /// <summary>
        /// get the email address for a person as an asynchronous operation.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("{personId}")]
        [ProducesResponseType(typeof(IEnumerable<EmailResult>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageForClient),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageForClient),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(Guid personId)
        {
            IEnumerable<IEmail> emails = await _personalEmailService.GetEmailsForAPersonAsync(personId);
            IEnumerable<EmailResult> returnResults = EmailResultTransformer.Transform(emails);
            return new OkObjectResult(returnResults);
        }

        /// <summary>
        /// insert an email address for a person as an asynchronous operation.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageForClient),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageForClient),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] AddEmailRequest request)
        {
            request.PersonId.Required("Person Id");
            request.Address.Required("email address");
            await _personalEmailService.AddAddressToAPersonAsync(request.PersonId, request.Address);
            return new OkResult();
        }

        /// <summary>
        /// Delete as an asynchronous operation.
        /// </summary>
        /// <param name="emailId">The email identifier.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        [HttpDelete("{emailId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageForClient),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageForClient),StatusCodes.Status500InternalServerError)]
        public async Task DeleteAsync(Guid emailId)
        {
            emailId.Required("email Id");
            await _personalEmailService.DeleteEmailAsync(emailId);
        }
    }
}
