// ***********************************************************************
// Assembly         : Contact.Data.EF
// Author           : Admin
// Created          : 03-28-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="PhoneRepo.cs" company="Contact.Data.EF">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contact.Data.EF.ConcretePocos;
using Contact.Data.EF.Transformers;
using Contact.Glue.Interfaces.DTOs;
using Contact.Glue.Interfaces.Repos;

/// <summary>
/// The ConcreteRepos namespace.
/// </summary>
namespace Contact.Data.EF.ConcreteRepos
{
    /// <summary>
    /// Class PhoneRepo.
    /// Implements the <see cref="BaseEfRepo{Phone}" />
    /// </summary>
    /// <seealso cref="BaseEfRepo{Phone}" />
    public class PhoneRepo:BaseEfRepo<Phone>, IPhoneRepo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneRepo" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public PhoneRepo(ContactDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// get phone as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns>IPhone.</returns>
        public async Task<IPhone> GetPhoneAsync(Guid id, CancellationToken token = default)
        {
            IEnumerable<Phone> results = await base.FindByConditionAsync(p => p.PhoneId == id, token);
            return results.FirstOrDefault();
        }

        /// <summary>
        /// insert as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Guid.</returns>
        public async Task<Guid> InsertAsync(IPhone entity, CancellationToken cancellationToken = default)
        {
            Phone rec = null!;
            if (entity is Phone phone)
            {
                rec = phone;
            }
            else
            {
                rec = PhoneTransformer.TransForm(entity);
            }

            await base.InsertAsync(rec);
            await SaveAsync(cancellationToken);
            return rec.PhoneId;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(IPhone entity)
        {
            Phone rec = null!;
            if (entity is Phone phone)
            {
                rec = phone;
            }
            else
            {
                rec = PhoneTransformer.TransForm(entity);
            }
            base.Update(rec);
            DbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(IPhone entity)
        {
            entity.Deleted = true;
            Update(entity);
        }

    }
}