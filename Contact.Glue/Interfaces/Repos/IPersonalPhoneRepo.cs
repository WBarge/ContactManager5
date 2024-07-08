// ***********************************************************************
// Assembly         : Contact.Glue
// Author           : Bill Barge
// Created          : 02-13-2024
//
// Last Modified By : Bill Barge
// Last Modified On : 02-13-2024
// ***********************************************************************
// <copyright file="IPersonalPhoneRepo.cs" company="Contact.Glue">
//     Copyright (c) N/A. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contact.Glue.Interfaces.DTOs;

namespace Contact.Glue.Interfaces.Repos
{
    /// <summary>
    /// Interface IPersonalPhoneRepo
    /// </summary>
    public interface IPersonalPhoneRepo
    {
        /// <summary>
        /// Doeses the person have number asynchronous.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="countryCode">The country code.</param>
        /// <param name="areaCode">The area code.</param>
        /// <param name="number">The number.</param>
        /// <param name="phoneType">Type of the phone.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> DoesPersonHaveNumberAsync(Guid personId, string countryCode, string areaCode, string number, string phoneType, CancellationToken cancellationToken = default);
        /// <summary>
        /// Adds the phone number to person.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="countryCode">The country code.</param>
        /// <param name="areaCode">The area code.</param>
        /// <param name="number">The number.</param>
        /// <param name="phoneType">Type of the phone.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task.</returns>
        Task AddPhoneNumberToPerson(Guid personId, string countryCode, string areaCode, string number, string phoneType, CancellationToken cancellationToken = default);
        /// <summary>
        /// Gets the default phone number asynchronous.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;IPhone&gt;.</returns>
        Task<IPhone> GetDefaultPhoneNumberAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a persons phone numbers asynchronous.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;IEnumerable&lt;IPhone&gt;&gt;.</returns>
        Task<IEnumerable<IPhone>> GetAPersonsPhoneNumbersAsync(Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets the default phone number asynchronous.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="phoneNumberId">The phone number identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> SetDefaultPhoneNumberAsync(Guid personId, Guid phoneNumberId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a persons number.
        /// </summary>
        /// <param name="phoneId">The phone identifier.</param>
        /// <param name="personId">The person identifier.</param>
        /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task.</returns>
        Task DeleteAPersonsNumber(Guid phoneId, Guid personId, CancellationToken token);
    }
}