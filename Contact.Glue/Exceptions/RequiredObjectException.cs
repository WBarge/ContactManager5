// ***********************************************************************
// Assembly         : Contact.Glue
// Author           : Admin
// Created          : 03-27-2021
//
// Last Modified By : Admin
// Last Modified On : 03-27-2021
// ***********************************************************************
// <copyright file="RequiredObjectException.cs" company="Contact.Glue">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Contact.Glue.Exceptions
{
    /// <summary>
    /// Class RequiredObjectException.
    /// Implements the <see cref="Exception" />
    /// </summary>
    /// <seealso cref="Exception" />
    public class RequiredObjectException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredObjectException"/> class.
        /// </summary>
        public RequiredObjectException() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredObjectException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public RequiredObjectException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredObjectException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public RequiredObjectException(string message, Exception inner) : base(message, inner) { }
    }
}