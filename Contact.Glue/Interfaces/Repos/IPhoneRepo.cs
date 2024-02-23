using System;
using System.Threading;
using System.Threading.Tasks;
using Contact.Glue.Interfaces.DTOs;

namespace Contact.Glue.Interfaces.Repos
{
    /// <summary>
    /// Interface IPhoneRepo
    /// </summary>
    public interface IPhoneRepo
    {
        /// <summary>
        /// get phone as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns>IPhone.</returns>
        Task<IPhone> GetPhoneAsync(Guid id, CancellationToken token = default);

        /// <summary>
        /// insert as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Guid.</returns>
        Task<Guid> InsertAsync(IPhone entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(IPhone entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(IPhone entity);
    }
}