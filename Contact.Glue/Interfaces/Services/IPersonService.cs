// ***********************************************************************
// Assembly         : 
// Author           : Admin
// Created          : 03-28-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="IPersonService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading;
using System.Threading.Tasks;
using Contact.Glue.Interfaces.DTOs;

namespace Contact.Glue.Interfaces.Services
{
    /// <summary>
    /// Interface IPersonService
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Gets a person.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;IPerson&gt;.</returns>
        Task<IPerson> GetAPerson(Guid id, CancellationToken token = default);

        /// <summary>
        /// Ups the sert asynchronous.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;Guid&gt;.</returns>
        Task<Guid> UpSertAsync(IPerson person, CancellationToken token = default);

        /// <summary>
        /// Deletes a person asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task.</returns>
        Task DeleteAPersonAsync(Guid id, CancellationToken token = default);
    }
}