// ***********************************************************************
// Assembly         : 
// Author           : Admin
// Created          : 05-11-2019
//
// Last Modified By : Admin
// Last Modified On : 03-27-2021
// ***********************************************************************
// <copyright file="IMTMPhone.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Contact.Glue.Interfaces.DTOs
{
    /// <summary>
    /// Represents a MTMPhone.
    /// NOTE: This interface is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public interface IMtmPhone 
    {
        /// <summary>
        /// Gets or sets the MTM phone identifier.
        /// </summary>
        /// <value>The MTM phone identifier.</value>
        Guid MtmPhoneId { get; set; }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>The person identifier.</value>
        Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the phone identifier.
        /// </summary>
        /// <value>The phone identifier.</value>
        Guid PhoneId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [default number].
        /// </summary>
        /// <value><c>true</c> if [default number]; otherwise, <c>false</c>.</value>
        bool DefaultNumber { get; set; }
    }
}
