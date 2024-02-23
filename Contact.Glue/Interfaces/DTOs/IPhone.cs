// ***********************************************************************
// Assembly         : 
// Author           : Admin
// Created          : 05-11-2019
//
// Last Modified By : Admin
// Last Modified On : 03-27-2021
// ***********************************************************************
// <copyright file="IPhone.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Contact.Glue.Interfaces.DTOs
{
    /// <summary>
    /// Represents a Phone.
    /// NOTE: This interface is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public interface IPhone 
    {
        /// <summary>
        /// Gets or sets the phone identifier.
        /// </summary>
        /// <value>The phone identifier.</value>
        Guid PhoneId { get; set; }

        /// <summary>
        /// Gets or sets the area code.
        /// </summary>
        /// <value>The area code.</value>
        string AreaCode { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>The country code.</value>
        string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        string Number { get; set; }

        /// <summary>
        /// Gets or sets the type of the phone.
        /// </summary>
        /// <value>The type of the phone.</value>
        string PhoneType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IPhone"/> is deleted.
        /// </summary>
        /// <value><c>true</c> if deleted; otherwise, <c>false</c>.</value>
        bool Deleted { get; set; }
    }
}
