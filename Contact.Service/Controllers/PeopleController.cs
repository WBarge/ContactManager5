// ***********************************************************************
// Assembly         : Contact.Service
// Author           : Admin
// Created          : 03-28-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="PeopleController.cs" company="Contact.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contact.Glue.Interfaces.DTOs;
using Contact.Glue.Interfaces.Services;
using Contact.Service.Models.Result;
using Contact.Service.Transformers;

namespace Contact.Service.Controllers
{
    /// <summary>
    /// Class PeopleController.
    /// It is the responsibility of this controller to handle people aka a collection of person 
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        /// <summary>
        /// The people service
        /// </summary>
        private readonly IPeopleService _peopleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleController"/> class.
        /// </summary>
        /// <param name="peopleService">The people service.</param>
        /// <exception cref="System.ArgumentNullException">peopleService</exception>
        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService ?? throw new ArgumentNullException(nameof(peopleService) + " is mandatory");
        }

        // GET: api/People
        /// <summary>
        /// get all the people in the system as an asynchronous operation.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonResult>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageForClient),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageForClient),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync()
        {
            IEnumerable<IPerson> results = await _peopleService.GetAllPeopleAsync();
            IEnumerable<PersonResult> returnValues = results.Select(PersonResultTransformer.TransForm).ToList();
            return new OkObjectResult(returnValues);
        }




    }
}
