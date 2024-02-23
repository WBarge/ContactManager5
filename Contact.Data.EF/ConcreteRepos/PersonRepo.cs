// ***********************************************************************
// Assembly         : Contact.Data.EF
// Author           : Admin
// Created          : 03-27-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="PersonRepo.cs" company="Contact.Data.EF">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contact.Data.EF.ConcretePocos;
using Contact.Data.EF.Transformers;
using Contact.Glue.Extensions;
using Contact.Glue.Interfaces.DTOs;
using Contact.Glue.Interfaces.Repos;
using Microsoft.EntityFrameworkCore;

namespace Contact.Data.EF.ConcreteRepos
{
    /// <summary>
    /// Class PersonRepo.
    /// Implements the <see cref="BaseEfRepo{Person}" />
    /// Implements the <see cref="IPersonRepo" />
    /// </summary>
    /// <seealso cref="BaseEfRepo{Person}" />
    /// <seealso cref="IPersonRepo" />
    public class PersonRepo:BaseEfRepo<Person>, IPersonRepo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonRepo"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public PersonRepo(ContactDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// get all as an asynchronous operation.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>IEnumerable&lt;IPerson&gt;.</returns>
        public async Task<IEnumerable<IPerson>> GetAllAsync(CancellationToken token = default)
        {
            List<Person>? result = await base.FindByConditionAsync(p => p.Deleted == false, token);
            return result.Cast<IPerson>().ToList();
        }

        /// <summary>
        /// get person as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns>System.Nullable&lt;IPerson&gt;.</returns>
        public async Task<IPerson?> GetPersonAsync(Guid id, CancellationToken token = default)
        {
            List<Person> results = await base.FindByConditionAsync(p => p.PersonId == id, token);
            return results.FirstOrDefault();
        }

        /// <summary>
        /// insert as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Guid.</returns>
        public async Task<Guid> InsertAsync(IPerson entity,CancellationToken cancellationToken = default)
        {
            Person rec = null!;
            if (entity is Person person)
            {
                rec = person;
            }
            else
            {
                rec = PersonTransformer.TransForm(entity);
            }
            await base.InsertAsync(rec);
            await base.SaveAsync(cancellationToken);
            return rec.PersonId;
        }

        /// <summary>
        /// update as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task UpdateAsync(IPerson entity, CancellationToken cancellationToken = default)
        {
            Person? rec = null;
            if (entity is Person)
            {
                rec = (Person)entity;
            }
            else
            {
                rec = PersonTransformer.TransForm(entity);
            }
            base.Update(rec);
            await base.SaveAsync(cancellationToken);
        }

        /// <summary>
        /// delete as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task DeleteAsync(IPerson entity, CancellationToken cancellationToken = default)
        {
            entity.Deleted = true;
            await UpdateAsync(entity, cancellationToken);
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
            Person? person = DbContext.People
                .Include(p => p.MtmPhones)
                .ThenInclude(m => m.Phone)
                .FirstOrDefault(p => p.PersonId == personId);
            if (person != null)
            {
                foreach (MtmPhone personMtmPhone in person.MtmPhones)
                {
                    personMtmPhone.DefaultNumber = personMtmPhone.PhoneId == phoneNumberId;
                }

                await DbContext.SaveChangesAsync(cancellationToken);
                returnValue = true;
            }
            return returnValue;
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>IPerson.</returns>
        public new IPerson Create()
        {
            return base.Create();
        }
    }
}