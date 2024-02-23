// ***********************************************************************
// Assembly         : Contact.Service
// Author           : Admin
// Created          : 03-28-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="PersonResultTransformer.cs" company="Contact.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Contact.Glue.Interfaces.DTOs;
using Contact.Service.Models.Result;

namespace Contact.Service.Transformers
{
    /// <summary>
    /// Class PersonResultTransformer.
    /// Responsible for transforming data between the internal structures to the interface structures
    /// This class could be replaced with a tool like Auto Mapper or Fast Mapper
    /// </summary>
    internal class PersonResultTransformer
    {
        /// <summary>
        /// Transforms the IPerson to the person result
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>PersonRequest.</returns>
        internal static PersonResult TransForm(IPerson result)
        {
            return new PersonResult
            {
                Id = result.PersonId,
                FirstName = result.First,
                LastName = result.Last,
                Deleted = result.Deleted
            };
        }
    }
}