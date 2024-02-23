// ***********************************************************************
// Assembly         : Contact.Glue
// Author           : Admin
// Created          : 03-28-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="IPersonalEmailService.cs" company="Contact.Glue">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contact.Glue.Interfaces.DTOs;

namespace Contact.Glue.Interfaces.Services
{
    /// <summary>
    /// Interface IPersonalEmailService
    /// </summary>
    public interface IPersonalEmailService
    {
        /// <summary>
        /// Gets the emails for a person.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;IEnumerable&lt;IEmail&gt;&gt;.</returns>
        Task<IEnumerable<IEmail>> GetEmailsForAPersonAsync(Guid personId,CancellationToken token = default);

        /// <summary>
        /// Adds the email address to a person.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="address">The address.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task.</returns>
        Task AddAddressToAPersonAsync(Guid personId, string address,CancellationToken token = default);

        /// <summary>
        /// Deletes the email asynchronous.
        /// </summary>
        /// <param name="emailId">The email identifier.</param>
        /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task.</returns>
        Task DeleteEmailAsync(Guid emailId , CancellationToken token = default);
    }
}