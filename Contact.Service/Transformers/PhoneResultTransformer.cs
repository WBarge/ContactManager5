// ***********************************************************************
// Assembly         : Contact.Service
// Author           : Bill Barge
// Created          : 02-13-2024
//
// Last Modified By : Bill Barge
// Last Modified On : 02-13-2024
// ***********************************************************************
// <copyright file="PhoneResultTransformer.cs" company="Contact.Service">
//     Copyright (c) N/A. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;
using Contact.Glue.Interfaces.DTOs;
using Contact.Service.Models.Result;

namespace Contact.Service.Transformers
{
    /// <summary>
    /// Class PhoneResultTransformer.
    /// </summary>
    internal class PhoneResultTransformer
    {
        /// <summary>
        /// Transforms the specified phone numbers.
        /// </summary>
        /// <param name="phoneNumbers">The phone numbers.</param>
        /// <returns>IEnumerable&lt;PhoneResult&gt;.</returns>
        internal static IEnumerable<PhoneResult> Transform(IEnumerable<IPhone> phoneNumbers)
        {
            return phoneNumbers.Select(Transform).ToList();
        }

        /// <summary>
        /// Transforms the specified phone.
        /// </summary>
        /// <param name="phone">The phone.</param>
        /// <returns>PhoneResult.</returns>
        internal static PhoneResult Transform(IPhone phone)
        {
            return new PhoneResult
            {
                Id = phone.PhoneId,
                AreaCode = phone.AreaCode,
                CountryCode = phone.CountryCode,
                Number = phone.Number,
                PhoneType = phone.PhoneType,
                Deleted = phone.Deleted
            };
        }
    }
}