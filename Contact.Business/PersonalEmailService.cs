// ***********************************************************************
// Assembly         : Contact.Business
// Author           : Admin
// Created          : 03-28-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="PersonalEmailService.cs" company="Contact.Business">
//     Copyright (c) . All rights reserved.
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
    /// Class PersonalEmailService.
    /// Responsible for dealing with email for a person
    /// Implements the <see cref="Contact.Glue.Interfaces.Services.IPersonalEmailService" />
    /// </summary>
    /// <seealso cref="Contact.Glue.Interfaces.Services.IPersonalEmailService" />
    public class PersonalEmailService : IPersonalEmailService
    {
        /// <summary>
        /// The personal email repo
        /// </summary>
        private readonly IPersonalEmailRepo _personalEmailRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalEmailService" /> class.
        /// </summary>
        /// <param name="personalEmailRepo">The personal email repo.</param>
        /// <exception cref="ArgumentNullException">personalEmailRepo</exception>
        public PersonalEmailService(IPersonalEmailRepo personalEmailRepo)
        {
            _personalEmailRepo = personalEmailRepo ?? throw new ArgumentNullException(nameof(personalEmailRepo) + " is manditory");
        }

        /// <summary>
        /// Gets the emails for a person.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;IEnumerable&lt;IEmail&gt;&gt;.</returns>
        public Task<IEnumerable<IEmail>> GetEmailsForAPersonAsync(Guid personId,CancellationToken token= default)
        {
            return _personalEmailRepo.GetEmailsForAPersonAsync(personId,token);
        }

        /// <summary>
        /// Adds the address to a person.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="address">The address.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task.</returns>
        public async Task AddAddressToAPersonAsync(Guid personId, string address,CancellationToken token= default)
        {
            await _personalEmailRepo.AddAnEmailToAPerson(personId, address, token);
        }

        public async Task DeleteEmailAsync(Guid emailId, CancellationToken token = default)
        {
            await _personalEmailRepo.DeleteEmailAsync(emailId,token);
        }
    }
}