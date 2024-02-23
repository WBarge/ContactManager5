// ***********************************************************************
// Assembly         : Contact.Service
// Author           : Admin
// Created          : 03-28-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="PersonController.cs" company="Contact.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Contact.Glue.Interfaces.DTOs;
using Contact.Glue.Interfaces.Services;
using Contact.Service.Models.Request;
using Contact.Service.Models.Result;
using Contact.Service.Transformers;
using Microsoft.AspNetCore.Http;

namespace Contact.Service.Controllers
{
    /// <summary>
    /// Class PersonController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        /// <summary>
        /// The person service
        /// </summary>
        private readonly IPersonService _personService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonController"/> class.
        /// </summary>
        /// <param name="personService">The person service.</param>
        /// <exception cref="System.ArgumentNullException">personService</exception>
        public PersonController(IPersonService personService)
        {
            _personService = personService ?? throw new ArgumentNullException(nameof(personService) + " is manditory");
        }

        /// <summary>
        /// gets a person as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PersonResult),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageForClient),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageForClient),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            IPerson result = await _personService.GetAPerson(id);
            PersonResult person = PersonResultTransformer.TransForm(result);
            return new OkObjectResult(person);
        }

        /// <summary>
        /// adds or updates a person as an asynchronous operation.
        /// </summary>
        /// <param name="personRequest">The person request.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageForClient),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageForClient),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(PersonRequest personRequest)
        {
            IPerson person = PersonRequestTransformer.TransForm(personRequest);
            Guid id = await _personService.UpSertAsync(person);
            return new OkObjectResult(id);
        }

        /// <summary>
        /// Deletes the person as specified by the id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IActionResult.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageForClient),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageForClient),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _personService.DeleteAPersonAsync(id);
            return new OkResult();
        }
    }

}
