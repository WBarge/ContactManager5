// ***********************************************************************
// Assembly         : Contact.Service
// Author           : Admin
// Created          : 03-28-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="PersonRequestTransformer.cs" company="Contact.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Contact.Glue.Interfaces.DTOs;
using Contact.Service.Models.DTOs;
using Contact.Service.Models.Request;


namespace Contact.Service.Transformers
{
    /// <summary>
    /// Class PersonRequestTransformer.
    /// Responsible for transforming data between the interface structures to the internal structures
    /// This class could be replaced with a tool like Auto Mapper or Fast Mapper
    /// </summary>
    internal static class PersonRequestTransformer
    {
        /// <summary>
        /// Transforms the person request to and IPerson
        /// </summary>
        /// <param name="personRequest">The person request.</param>
        /// <returns>IPerson.</returns>
        internal static IPerson TransForm(PersonRequest personRequest)
        {
            return new Person
            {
                PersonId = personRequest.Id,
                First = personRequest.FirstName,
                Last = personRequest.LastName
            };
        }
    }
}