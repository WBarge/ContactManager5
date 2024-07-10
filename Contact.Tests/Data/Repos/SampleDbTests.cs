// ***********************************************************************
// Assembly         : Contact.Tests
// Author           : Bill Barge
// Created          : 02-16-2024
//
// Last Modified By : Bill Barge
// Last Modified On : 02-17-2024
// ***********************************************************************
// <copyright file="SampleDbTests.cs" company="Contact.Tests">
//     Copyright (c) N/A. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using NUnit.Framework;
using System;
using Microsoft.Extensions.DependencyInjection;
using Contact.Data.EF;
using Contact.Data.EF.ConcretePocos;
using Microsoft.EntityFrameworkCore;
// ReSharper disable RedundantToStringCall

namespace Contact.Tests.Data.Repos
{
    /// <summary>
    /// Class SampleDbTests.
    /// This is a place for the dev to validate the db can be changed in memory
    /// </summary>
    public class SampleDbTests
    {
        /// <summary>
        /// The service provider
        /// </summary>
        private IServiceProvider _serviceProvider;

        /// <summary>
        /// Called when [time setup].
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _serviceProvider = TestSetupHelper.GetServiceProvider();
        }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.SeedData();
            }
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.RemoveData();
            }
        }


        /// <summary>
        /// Defines the test method sampleTest.
        /// </summary>
        [Test]
        public void SampleTest()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    PrintOutAllPeople(context);

                    TestContext.Out.WriteLineAsync("Now adding a person");
                    Guid personId= Guid.NewGuid();
                    Person person1 = new()
                    {
                        PersonId = personId,
                        First = "foo",
                        Last = "bar",
                        Deleted = false
                    };

                    context.People.Add(person1);
                    context.SaveChanges();

                    PrintOutAllPeople(context);
                }
            }
        }

        /// <summary>
        /// Prints the out all people.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void PrintOutAllPeople(ContactDbContext context)
        {
            TestContext.Out.WriteLineAsync("All of the people");
            foreach (Person person in context.People.Include(p=>p.Emails).Include(p=>p.Phones))
            {
                TestContext.Out.WriteLineAsync(person.PersonId.ToString());
                TestContext.Out.WriteLineAsync(person.Last);
                TestContext.Out.WriteLineAsync(person.First);
                foreach (Email personEmail in person.Emails)
                {
                    TestContext.Out.WriteLineAsync(personEmail.EmailId.ToString());
                    TestContext.Out.WriteLineAsync(personEmail.Address);
                }

                foreach (Phone personPhone in person.Phones)
                {
                    TestContext.Out.WriteLineAsync(personPhone.PhoneId.ToString());
                    TestContext.Out.WriteLineAsync(personPhone.CountryCode.ToString());
                    TestContext.Out.WriteLineAsync(personPhone.AreaCode.ToString());
                    TestContext.Out.WriteLineAsync(personPhone.Number.ToString());
                }
            }
        }

        /// <summary>
        /// Defines the test method SampleTest2.
        /// </summary>
        [Test]
        public void SampleTest2()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    PrintOutAllPeople(context);
                }
            }
        }
    }
}