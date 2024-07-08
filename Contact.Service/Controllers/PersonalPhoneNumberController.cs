// ***********************************************************************
// Assembly         : Contact.Service
// Author           : Bill Barge
// Created          : 02-13-2024
//
// Last Modified By : Bill Barge
// Last Modified On : 02-13-2024
// ***********************************************************************
// <copyright file="PersonalPhoneNumberController.cs" company="Contact.Service">
//     Copyright (c) N/A. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contact.Glue.Extensions;
using Contact.Glue.Interfaces.DTOs;
using Contact.Glue.Interfaces.Services;
using Contact.Service.Models.Request;
using Contact.Service.Models.Result;
using Contact.Service.Transformers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contact.Service.Controllers
{
    /// <summary>
    /// Class PersonalPhoneNumberController.
    /// Implements the <see cref="ControllerBase" />
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalPhoneNumberController : ControllerBase
    {
        /// <summary>
        /// The personal phone service
        /// </summary>
        private readonly IPersonalPhoneService _personalPhoneService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalPhoneNumberController"/> class.
        /// </summary>
        /// <param name="personalPhoneService">The personal phone service.</param>
        /// <exception cref="System.ArgumentNullException">personalPhoneService</exception>
        public PersonalPhoneNumberController(IPersonalPhoneService personalPhoneService)
        {
            _personalPhoneService = personalPhoneService ?? throw new ArgumentNullException(nameof(personalPhoneService) + " is mandatory");
        }

        /// <summary>
        /// Get as an asynchronous operation.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns>A Task&lt;IActionResult&gt; representing the asynchronous operation.</returns>
        [HttpGet("{personId}")]
        [ProducesResponseType(typeof(IEnumerable<PhoneResult>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageForClient),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageForClient),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(Guid personId)
        {
            IEnumerable<IPhone> phoneNumbers = await _personalPhoneService.GetPhoneNumbersForAPersonAsync(personId);
            IEnumerable<PhoneResult> returnResults = PhoneResultTransformer.Transform(phoneNumbers);
            return new OkObjectResult(returnResults);
        }

        /// <summary>
        /// Post as an asynchronous operation.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task&lt;IActionResult&gt; representing the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageForClient),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageForClient),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] PhoneNumberRequest request)
        {
            request.PersonId.Required("Person Id");
            request.AreaCode.Required("Area Code");
            request.Number.Required("Number");
            await _personalPhoneService.AddPhoneNumberToAPersonAsync(request.PersonId,request.CountryCode,request.AreaCode, request.Number,request.PhoneType);
            return new OkResult();
        }

        /// <summary>
        /// Delete as an asynchronous operation.
        /// </summary>
        /// <param name="phoneId">The phone identifier.</param>
        /// <param name="personId">The person identifier.</param>
        /// <returns>A Task&lt;IActionResult&gt; representing the asynchronous operation.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageForClient), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageForClient), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid phoneId, [FromQuery] Guid personId)
        {
            phoneId.Required("Phone Id");
            personId.Required("Person Id");
            await _personalPhoneService.DeletePhoneAsync(phoneId, personId);
            return new OkResult();
        }

        /// <summary>
        /// Sets the default phone number.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="phoneId">The phone identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("{personId}/DefaultNumber/{phoneId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageForClient), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageForClient), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetDefaultNumber([FromQuery] Guid personId, [FromQuery] Guid phoneId)
        {
            phoneId.Required("Phone Id");
            personId.Required("Person Id");
            await _personalPhoneService.SetDefaultNumberAsync(personId, phoneId);
            return new OkResult();
        }
    }
}
