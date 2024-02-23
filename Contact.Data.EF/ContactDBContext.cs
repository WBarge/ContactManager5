// ***********************************************************************
// Assembly         : Contact.Data.EF
// Author           : Admin
// Created          : 03-27-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="ContactDBContext.cs" company="Contact.Data.EF">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Reflection;
using Contact.Data.EF.ConcretePocos;
using Microsoft.EntityFrameworkCore;

namespace Contact.Data.EF
{
    /// <summary>
    /// Class ContactDbContext.
    /// Implements the <see cref="Microsoft.EntityFrameworkCore.DbContext" />
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class ContactDbContext: DbContext
    {
        //DBSets
        /// <summary>
        /// Gets or sets the people.
        /// </summary>
        /// <value>The people.</value>
        public DbSet<Person> People { get; set; }
        /// <summary>
        /// Gets or sets the emails.
        /// </summary>
        /// <value>The emails.</value>
        public DbSet<Email> Emails { get; set; }
        /// <summary>
        /// Gets or sets the phone numbers.
        /// </summary>
        /// <value>The phone numbers.</value>
        public DbSet<Phone> PhoneNumbers { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ContactDbContext(DbContextOptions<ContactDbContext> options):base(options){}

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.</remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //find all classes that implement IEntityTypeConfiguration and execute their Configure method
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }
    }
}
