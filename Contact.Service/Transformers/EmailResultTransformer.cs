// ***********************************************************************
// Assembly         : Contact.Service
// Author           : Admin
// Created          : 03-28-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="EmailResultTransformer.cs" company="Contact.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Contact.Glue.Interfaces.DTOs;
using Contact.Service.Models.Result;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Contact.Tests")]
namespace Contact.Service.Transformers
{
    /// <summary>
    /// Class EmailResultTransformer.
    /// Responsible for transforming data between the internal structures to the interface structures
    /// This class could be replaced with a tool like Auto Mapper or Fast Mapper
    /// </summary>
    internal class EmailResultTransformer
    {
        /// <summary>
        /// Transforms the specified emails.
        /// </summary>
        /// <param name="emails">The emails.</param>
        /// <returns>IEnumerable&lt;EmailResult&gt;.</returns>
        internal static IEnumerable<EmailResult> Transform(IEnumerable<IEmail> emails)
        {
            return emails.Select(Transform).ToList();
        }

        /// <summary>
        /// Transforms the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>EmailResult.</returns>
        internal static EmailResult Transform(IEmail email)
        {
            return new EmailResult
            {
                Id = email.EmailId,
                Address = email.Address
            };
        }
    }
}