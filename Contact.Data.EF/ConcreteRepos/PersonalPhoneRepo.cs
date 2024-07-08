// ***********************************************************************
// Assembly         : Contact.Data.EF
// Author           : Bill Barge
// Created          : 02-13-2024
//
// Last Modified By : Bill Barge
// Last Modified On : 02-13-2024
// ***********************************************************************
// <copyright file="PersonalPhoneRepo.cs" company="Contact.Data.EF">
//     Copyright (c) N/A. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contact.Data.EF.ConcretePocos;
using Contact.Glue.Extensions;
using Contact.Glue.Interfaces.DTOs;
using Contact.Glue.Interfaces.Repos;
using Microsoft.EntityFrameworkCore;

namespace Contact.Data.EF.ConcreteRepos
{
    /// <summary>
    /// Class PersonalPhoneRepo.
    /// Implements the <see cref="IPersonalPhoneRepo" />
    /// </summary>
    /// <seealso cref="IPersonalPhoneRepo" />
    public class PersonalPhoneRepo : IPersonalPhoneRepo
    {
        /// <summary>
        /// The contact database context
        /// </summary>
        private readonly ContactDbContext _contactDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalEmailRepo" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">context</exception>
        public PersonalPhoneRepo(ContactDbContext context)
        {
            _contactDbContext = context ?? throw new ArgumentNullException(nameof(context) + " is mandatory");
        }

        /// <summary>
        /// Does person have number as an asynchronous operation.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="countryCode">The country code.</param>
        /// <param name="areaCode">The area code.</param>
        /// <param name="number">The number.</param>
        /// <param name="phoneType">Type of the phone.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;System.Boolean&gt; representing the asynchronous operation.</returns>
        public async Task<bool> DoesPersonHaveNumberAsync(Guid personId, string countryCode, string areaCode, string number, string phoneType, CancellationToken cancellationToken = default)
        {

            Person person = await _contactDbContext.People.Include(p => p.Phones)
                .FirstOrDefaultAsync(p => p.PersonId == personId, cancellationToken: cancellationToken);
            if (person == null)
            {
                return false;
            }
            
            bool result = person.Phones.Any(p => p.CountryCode == countryCode &&
                                       p.AreaCode == areaCode &&
                                       p.Number == number);
            return result;
        }

        /// <summary>
        /// Adds the phone number to person.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="countryCode">The country code.</param>
        /// <param name="areaCode">The area code.</param>
        /// <param name="number">The number.</param>
        /// <param name="phoneType">Type of the phone.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task AddPhoneNumberToPerson(Guid personId, string countryCode, string areaCode, string number, string phoneType, CancellationToken cancellationToken = default)
        {
            if (!await DoesPersonHaveNumberAsync(personId, countryCode, areaCode, number, phoneType, cancellationToken))
            {
                Person person = await _contactDbContext.People.Include(p=>p.Phones).FirstOrDefaultAsync(p => p.PersonId == personId, cancellationToken: cancellationToken);
                if (person == null)
                {
                    return;
                }
                Phone phoneToAdd = new()
                {
                    AreaCode = areaCode,
                    CountryCode = countryCode,
                    Number = number,
                    PhoneType = phoneType
                };
                // if (!DoesNumberExist(phoneToAdd,out Guid? phoneId))
                // {
                //     _contactDbContext.PhoneNumbers.Add(phoneToAdd);
                //     await _contactDbContext.SaveChangesAsync(cancellationToken);
                // }
                person.Phones.Add(phoneToAdd);
                await _contactDbContext.SaveChangesAsync(cancellationToken);
            }
        }

        /// <summary>
        /// Does the number exist.
        /// </summary>
        /// <param name="phoneToAdd">The phone to add.</param>
        /// <param name="phoneId">The phone identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool DoesNumberExist(IPhone phoneToAdd,out Guid? phoneId)
        {
            phoneId = Guid.Empty;
            Phone phone = _contactDbContext.PhoneNumbers.FirstOrDefault(p => p.CountryCode == phoneToAdd.CountryCode &&
                                                                             p.AreaCode == phoneToAdd.AreaCode &&
                                                                             p.Number == phoneToAdd.Number &&
                                                                             p.PhoneType == phoneToAdd.PhoneType);
            phoneId = phone?.PhoneId;
            return phone.IsNotEmpty();
        }

        /// <summary>
        /// get default phone number as an asynchronous operation.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>IPhone.</returns>
        public async Task<IPhone> GetDefaultPhoneNumberAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            Phone phoneNumber = null;
            await Task.Run(async () =>
            {
                phoneNumber = await _contactDbContext.PhoneNumbers
                    .Include(m => m.MtmPhones)
                    .FirstOrDefaultAsync(
                        phone => phone.MtmPhones.Any(m => m.PersonId == personId && m.DefaultNumber == true), cancellationToken: cancellationToken);
            }, cancellationToken).ConfigureAwait(false);
            return phoneNumber;
        }

        /// <summary>
        /// set default phone number as an asynchronous operation.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="phoneNumberId">The phone number identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public async Task<bool> SetDefaultPhoneNumberAsync(Guid personId, Guid phoneNumberId,CancellationToken cancellationToken = default)
        {
            personId.Required(nameof(personId));
            phoneNumberId.Required(nameof(phoneNumberId));

            bool returnValue = false;
            Person person = _contactDbContext.People
                .Include(p => p.MtmPhones)
                .FirstOrDefault(p => p.PersonId == personId);
            if (person != null)
            {
                foreach (MtmPhone personMtmPhone in person.MtmPhones)
                {
                    personMtmPhone.DefaultNumber = personMtmPhone.PhoneId == phoneNumberId;
                }

                await _contactDbContext.SaveChangesAsync(cancellationToken);
                returnValue = true;
            }
            return returnValue;
        }


        /// <summary>
        /// Get a persons phone numbers as an asynchronous operation.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<IPhone>> GetAPersonsPhoneNumbersAsync(Guid personId, CancellationToken cancellationToken = default)
        {
            List<Phone> phoneNumbers = null;
            await Task.Run(async () =>
            {
                Person person = await _contactDbContext.People.Include(p => p.Phones)
                    .FirstOrDefaultAsync(p => p.PersonId == personId, cancellationToken: cancellationToken);
                phoneNumbers = person?.Phones.Where(p=>p.Deleted == false).ToList();
            }, cancellationToken).ConfigureAwait(false);
            return phoneNumbers;
        }

        public async Task DeleteAPersonsNumber(Guid phoneId, Guid personId, CancellationToken token)
        {
            Person person = await _contactDbContext.People.Include(p => p.Phones)
                .FirstOrDefaultAsync(p => p.PersonId == personId, cancellationToken: token);
            if (person.IsNotEmpty())
            {
                Phone phone = person.Phones.FirstOrDefault(p => p.PhoneId == phoneId);
                if (phone.IsNotEmpty())
                {
                    phone!.Deleted = true;
                    await _contactDbContext.SaveChangesAsync(token);
                }
            }
        }
    }
}