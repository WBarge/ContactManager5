using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contact.Data.EF;
using Contact.Data.EF.ConcreteRepos;
using Contact.Glue.Interfaces.DTOs;
using CrossCutting.Exceptions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Contact.Tests.Data.Repos
{
    [TestFixture, Description("PersonalEmailRepo tests")]
    public class PersonalEmailRepoTests
    {
        private IServiceProvider _serviceProvider;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _serviceProvider = TestSetupHelper.GetServiceProvider();
        }

        [SetUp]
        public void Setup()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.SeedData();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.RemoveData();
            }
        }

        [Test, Description("Test required context object")]
        public void Constructor_RequiredContext_Fail()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => new PersonalEmailRepo(null));
        }

        [Test, Description("Test required context object")]
        public void Constructor_RequiredContext_Success()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    PersonalEmailRepo sut = new PersonalEmailRepo(context);
                    sut.Should().NotBeNull();
                }
            }
        }

        [Test, Description("Get emails for a person")]
        public async Task GetEmailsForAPersonAsync_Retrieve_Success()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    Guid personId = context.People.First().PersonId;

                    PersonalEmailRepo sut = new PersonalEmailRepo(context);
                    IEnumerable<IEmail> result = await sut.GetEmailsForAPersonAsync(personId);
                    result.Should().NotBeNull();
                    result.Should().HaveCount(1);
                    foreach (IEmail email in result)
                    {
                        email.PersonId.Should().NotBeEmpty();
                        email.EmailId.Should().NotBeEmpty();
                        email.Address.Should().NotBeEmpty();
                        email.Deleted.Should().BeFalse();
                    }
                }
            }
        }

        [Test, Description("Get emails for a person")]
        public Task GetEmailsForAPersonAsync_Retrieve_Fail()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    Guid personId = Guid.NewGuid();

                    PersonalEmailRepo sut = new PersonalEmailRepo(context);
                    AsyncTestDelegate act = () => sut.GetEmailsForAPersonAsync(personId);

                    // Assert
                    Assert.That(act, Throws.TypeOf<RequestException>());
                }
            }

            return Task.CompletedTask;
        }

        [Test, Description("Add an email to a person")]
        public Task AddAnEmailToAPerson_Add_Fail()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    Guid personId = Guid.NewGuid();

                    PersonalEmailRepo sut = new PersonalEmailRepo(context);
                    AsyncTestDelegate act = () => sut.AddAnEmailToAPerson(personId,string.Empty);

                    // Assert
                    Assert.That(act, Throws.TypeOf<RequestException>());
                }
            }

            return Task.CompletedTask;
        }

        [Test, Description("Add an email to a person")]
        public async Task AddAnEmailToAPerson_Add_Success()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                await using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    Guid personId = context.People.First().PersonId;

                    PersonalEmailRepo sut = new PersonalEmailRepo(context);
                    await sut.AddAnEmailToAPerson(personId,"newAddress@gmail.com");

                    context.People.Include(person => person.Emails).First().Emails.Should().HaveCount(2);

                }
            }
        }

        [Test, Description("Add an email to a person")]
        public async Task DeleteEmailAsync_MarkEmailAsDeleted_Success()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    Guid emailId = context.Emails.First().EmailId;

                    PersonalEmailRepo sut = new PersonalEmailRepo(context);
                    await sut.DeleteEmailAsync(emailId);

                    context.Emails.First().Deleted.Should().BeTrue();

                }
            }
        }

        [Test, Description("Add an email to a person")]
        public Task DeleteEmailAsync_MarkEmailAsDeleted_Fail()
        {
            using (IServiceScope serviceScope =
                   _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    Guid emailId = Guid.Empty;

                    PersonalEmailRepo sut = new PersonalEmailRepo(context);
                    AsyncTestDelegate act = () => sut.DeleteEmailAsync(emailId);

                    // Assert
                    Assert.That(act, Throws.TypeOf<RequestException>());
                }
            }

            return Task.CompletedTask;
        }
    }
}