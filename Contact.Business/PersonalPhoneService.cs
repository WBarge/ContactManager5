// ***********************************************************************
// Assembly         : Contact.Business
// Author           : Bill Barge
// Created          : 02-13-2024
//
// Last Modified By : Bill Barge
// Last Modified On : 02-13-2024
// ***********************************************************************
// <copyright file="PersonalPhoneService.cs" company="Contact.Business">
//     Copyright (c) N/A. All rights reserved.
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
    /// Class PersonalPhoneService.
    /// Implements the <see cref="IPersonalPhoneService" />
    /// </summary>
    /// <seealso cref="IPersonalPhoneService" />
    public class PersonalPhoneService : IPersonalPhoneService
    {
        /// <summary>
        /// The phone repo
        /// </summary>
        private readonly IPersonalPhoneRepo _phoneRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalPhoneService"/> class.
        /// </summary>
        /// <param name="phoneRepo">The phone repo.</param>
        /// <exception cref="System.ArgumentNullException">phoneRepo</exception>
        public PersonalPhoneService(IPersonalPhoneRepo phoneRepo)
        {
            _phoneRepo = phoneRepo ?? throw new ArgumentNullException(nameof(phoneRepo));
        }

        /// <summary>
        /// Add phone number to a person as an asynchronous operation.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="countryCode">The country code.</param>
        /// <param name="areaCode">The area code.</param>
        /// <param name="number">The number.</param>
        /// <param name="phoneType">Type of the phone.</param>
        /// <param name="token"></param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task AddPhoneNumberToAPersonAsync(Guid personId, string countryCode, string areaCode, string number, string phoneType,CancellationToken token= default)
        {
            if (await _phoneRepo.DoesPersonHaveNumberAsync(personId, countryCode, areaCode, number, phoneType,token))
            {
                return;
            }
            await _phoneRepo.AddPhoneNumberToPerson(personId,countryCode, areaCode, number, phoneType, token);
        }

        /// <summary>
        /// Get phone numbers for a person as an asynchronous operation.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="token"></param>
        /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<IPhone>> GetPhoneNumbersForAPersonAsync(Guid personId,CancellationToken token= default)
        {
            IEnumerable<IPhone> phoneNumbers = await _phoneRepo.GetAPersonsPhoneNumbersAsync(personId,token);
            return phoneNumbers;
        }

        /// <summary>
        /// Delete phone as an asynchronous operation.
        /// </summary>
        /// <param name="phoneId">The phone identifier.</param>
        /// <param name="personId">The person identifier.</param>
        /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task DeletePhoneAsync(Guid phoneId, Guid personId, CancellationToken token = default)
        {
            await _phoneRepo.DeleteAPersonsNumber(phoneId,personId,token);
        }

        /// <summary>
        /// Set default number as an asynchronous operation.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="phoneId">The phone identifier.</param>
        /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SetDefaultNumberAsync(Guid personId, Guid phoneId, CancellationToken token = default)
        {
            await _phoneRepo.SetDefaultPhoneNumberAsync(personId, phoneId,token);
        }
    }
}