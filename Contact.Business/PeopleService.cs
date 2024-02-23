// ***********************************************************************
// Assembly         : Contact.Business
// Author           : Admin
// Created          : 03-28-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="PeopleService.cs" company="Contact.Business">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contact.Glue.Interfaces.DTOs;
using Contact.Glue.Interfaces.Repos;
using Contact.Glue.Interfaces.Services;

namespace Contact.Business
{
    /// <summary>
    /// Class PeopleService.
    /// Implements the <see cref="Contact.Glue.Interfaces.Services.IPeopleService" />
    /// </summary>
    /// <seealso cref="Contact.Glue.Interfaces.Services.IPeopleService" />
    public class PeopleService : IPeopleService
    {
        /// <summary>
        /// The person repo
        /// </summary>
        private readonly IPersonRepo _personRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleService"/> class.
        /// </summary>
        /// <param name="personRepo">The person repo.</param>
        /// <exception cref="ArgumentNullException">personRepo</exception>
        public PeopleService(IPersonRepo personRepo)
        {
            _personRepo = personRepo ?? throw new ArgumentNullException(nameof(personRepo) + " is mandatory");
        }

        /// <summary>
        /// Gets all people asynchronous.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;IEnumerable&lt;IPerson&gt;&gt;.</returns>
        public Task<IEnumerable<IPerson>> GetAllPeopleAsync(CancellationToken token = default)
        {
            return _personRepo.GetAllAsync(token);
        }
    }
}
