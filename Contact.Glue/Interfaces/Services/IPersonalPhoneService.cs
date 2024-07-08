// ***********************************************************************
// Assembly         : Contact.Glue
// Author           : Bill Barge
// Created          : 02-13-2024
//
// Last Modified By : Bill Barge
// Last Modified On : 02-13-2024
// ***********************************************************************
// <copyright file="IPersonalPhoneService.cs" company="Contact.Glue">
//     Copyright (c) N/A. All rights reserved.
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
    /// Interface IPersonalPhoneService
    /// </summary>
    public interface IPersonalPhoneService
    {
        /// <summary>
        /// Adds the phone number to a person asynchronous.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="countryCode">The country code.</param>
        /// <param name="areaCode">The area code.</param>
        /// <param name="number">The number.</param>
        /// <param name="phoneType">Type of the phone.</param>
        /// <param name="token"></param>
        /// <returns>Task.</returns>
        Task AddPhoneNumberToAPersonAsync(Guid personId, string countryCode, string areaCode, string number, string phoneType,CancellationToken token= default);

        /// <summary>
        /// Gets the phone numbers for a person asynchronous.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="token"></param>
        /// <returns>Task&lt;IEnumerable&lt;IPhone&gt;&gt;.</returns>
        Task<IEnumerable<IPhone>> GetPhoneNumbersForAPersonAsync(Guid personId,CancellationToken token= default);

        /// <summary>
        /// Deletes the phone.
        /// </summary>
        /// <param name="phoneId">The phone identifier.</param>
        /// <param name="personId">The person identifier.</param>
        /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task.</returns>
        Task DeletePhoneAsync(Guid phoneId, Guid personId,CancellationToken token= default);

        /// <summary>
        /// Sets the default number asynchronous.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="phoneId">The phone identifier.</param>
        /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task.</returns>
        Task SetDefaultNumberAsync(Guid personId, Guid phoneId,CancellationToken token= default);
    }
}