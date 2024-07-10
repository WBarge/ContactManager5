using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contact.Glue.Interfaces.DTOs;

namespace Contact.Glue.Interfaces.Repos
{
    /// <summary>
    /// Interface IPersonalEmailRepo
    /// </summary>
    public interface IPersonalEmailRepo
    {
        /// <summary>
        /// get emails for a person as an asynchronous operation.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>IEnumerable&lt;IEmail&gt;.</returns>
        /// <exception cref="CrossCutting.Exceptions.RequestException">Invalid Person Id</exception>
        Task<IEnumerable<IEmail>> GetEmailsForAPersonAsync(Guid personId, CancellationToken cancellationToken);

        /// <summary>
        /// Adds an email to a person.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <param name="address">The address.</param>
        /// <param name="token">The token.</param>
        Task AddAnEmailToAPerson(Guid personId, string address, CancellationToken token);

        /// <summary>
        /// Deletes the email asynchronous.
        /// </summary>
        /// <param name="emailId">The email identifier.</param>
        /// <param name="token">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Task.</returns>
        Task DeleteEmailAsync(Guid emailId, CancellationToken token);
    }
}