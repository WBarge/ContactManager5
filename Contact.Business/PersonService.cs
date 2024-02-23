// ***********************************************************************
// Assembly         : Contact.Business
// Author           : Admin
// Created          : 03-28-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="PersonService.cs" company="Contact.Business">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading;
using System.Threading.Tasks;
using Contact.Glue.Exceptions;
using Contact.Glue.Extensions;
using Contact.Glue.Interfaces.DTOs;
using Contact.Glue.Interfaces.Repos;
using Contact.Glue.Interfaces.Services;

namespace Contact.Business
{
    /// <summary>
    /// Class PersonService.
    /// The responsibility of this class to handle simple operation on a person
    /// </summary>
    public class PersonService : IPersonService
    {
        /// <summary>
        /// The person repo
        /// </summary>
        private readonly IPersonRepo _personRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService"/> class.
        /// </summary>
        /// <param name="personRepo">The person repo.</param>
        /// <exception cref="System.ArgumentNullException">personRepo</exception>
        public PersonService(IPersonRepo personRepo)
        {
            _personRepo = personRepo ?? throw new ArgumentNullException(nameof(personRepo) + " is mandatory");
        }

        /// <summary>
        /// Gets a person.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;IPerson&gt;.</returns>
        public Task<IPerson> GetAPerson(Guid id, CancellationToken token = default)
        {
            return _personRepo.GetPersonAsync(id, token);
        }

        /// <summary>
        /// updates or insert a person as an asynchronous operation.
        /// IF the id is empty then it will insert else it will update
        /// </summary>
        /// <param name="person">The person.</param>
        /// <param name="token">The token.</param>
        /// <returns>Guid.</returns>
        public async Task<Guid> UpSertAsync(IPerson person, CancellationToken token = default)
        {
            Guid returnValue = Guid.Empty;
            if (person.PersonId.IsEmpty())
            {
                returnValue = await _personRepo.InsertAsync(person, token);
            }
            else
            {
                await _personRepo.UpdateAsync(person, token);
            }

            return returnValue;
        }

        /// <summary>
        /// delete a person as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="token">The token.</param>
        public async Task DeleteAPersonAsync(Guid id, CancellationToken token = default)
        {
            IPerson person = await _personRepo.GetPersonAsync(id,token);
            if (person == null)
            {
                throw new RequestException($"{id} is not valid");
            }
            await _personRepo.DeleteAsync(person,token);
        }
    }
}