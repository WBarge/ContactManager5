// ***********************************************************************
// Assembly         : Contact.Data.EF
// Author           : Admin
// Created          : 03-28-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="PersonalEmailRepo.cs" company="Contact.Data.EF">
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
using Contact.Glue.Exceptions;
using Contact.Glue.Extensions;
using Contact.Glue.Interfaces.DTOs;
using Contact.Glue.Interfaces.Repos;
using Microsoft.EntityFrameworkCore;

namespace Contact.Data.EF.ConcreteRepos
{
    /// <summary>
    /// Class PersonalEmailRepo.
    /// This class is responsible for dealing with the persistence of email for a person
    /// This is a different approach as compared to the personRepo but the next layer up does not know anything about how the data is dealt with
    /// </summary>
    public class PersonalEmailRepo : IPersonalEmailRepo
    {
        /// <summary>
        /// The contact database context
        /// </summary>
        private readonly ContactDbContext _contactDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalEmailRepo"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        public PersonalEmailRepo(ContactDbContext context)
        {
            _contactDbContext = context ?? throw new ArgumentNullException(nameof(context) + " is manditory");
        }

        /// <summary>
        /// get emails for a person as an asynchronous operation.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>IEnumerable&lt;IEmail&gt;.</returns>
        /// <exception cref="RequestException">Invalid Person Id</exception>
        public async Task<IEnumerable<IEmail>> GetEmailsForAPersonAsync(Guid personId,
            CancellationToken cancellationToken = default)
        {
            Person? person = await _contactDbContext.People
                .Include(p => p.Emails)
                .FirstOrDefaultAsync(p => p.PersonId == personId && p.Deleted == false, cancellationToken: cancellationToken);

            ThrowIfNull(person);

            return person!.Emails.Where(e=>e.Deleted == false).ToList();
        }

        /// <summary>
        /// Checks for null person.
        /// </summary>
        /// <param name="obj">The person.</param>
        /// <exception cref="RequestException">Invalid Person Id</exception>
        private static void ThrowIfNull<T>(T obj)
        {
            if (obj.IsEmpty())
            {
                throw new RequestException("Invalid Person Id");
            }
        }

        /// <summary>
        /// Adds an email to a person.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="address">The address.</param>
        /// <param name="token">The token.</param>
        public async  Task AddAnEmailToAPerson(Guid personId, string address, CancellationToken token = default)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Person person = await _contactDbContext.People.Include(p=>p.Emails).FirstOrDefaultAsync(p => p.PersonId == personId && p.Deleted == false, cancellationToken: token);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            ThrowIfNull(person);
            Email addressToAdd = new Email
            {
                Address = address
            };
            if (person!.Emails.IsEmpty())
            {
                person.Emails = new List<Email>();
            }
            person.Emails.Add(addressToAdd);
            await _contactDbContext.SaveChangesAsync(token);
        }

        /// <summary>
        /// Delete email as an asynchronous operation.
        /// </summary>
        /// <param name="emailId">The email identifier.</param>
        /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task DeleteEmailAsync(Guid emailId, CancellationToken token = default)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Email email = await _contactDbContext.Emails.FirstOrDefaultAsync(e => e.EmailId == emailId, token);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            ThrowIfNull(email);
            email!.Deleted = true;
            await _contactDbContext.SaveChangesAsync(token);
        }
    }
}