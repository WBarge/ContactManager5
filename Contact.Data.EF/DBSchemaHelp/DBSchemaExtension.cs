// ***********************************************************************
// Assembly         : Contact.Service
// Author           : Admin
// Created          : 03-27-2021
//
// Last Modified By : Admin
// Last Modified On : 03-27-2021
// ***********************************************************************
// <copyright file="DBSchemaExtension.cs" company="Contact.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Contact.Glue.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Contact.Data.EF.DBSchemaHelp
{
    /// <summary>
    /// Class DBSchemaExtension.
    /// </summary>
    public static class DBSchemaExtension
    {
        /// <summary>
        /// Handles the database schema creation and migrations.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void HandleDBSchema(this IServiceCollection services)
        {
            ServiceProvider provider = services.BuildServiceProvider();
            ContactDbContext dbContext = provider.GetRequiredService<ContactDbContext>();
            dbContext.Required(nameof(dbContext));
            dbContext.Database.EnsureCreated();

        }
    }
}
