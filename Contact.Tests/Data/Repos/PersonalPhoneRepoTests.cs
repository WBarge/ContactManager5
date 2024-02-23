using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contact.Data.EF;
using Contact.Data.EF.ConcretePocos;
using Contact.Data.EF.ConcreteRepos;
using Contact.Glue.Interfaces.DTOs;
using Contact.Tests.TestDataFactories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Contact.Tests.Data.Repos
{
    [TestFixture, Description("PersonalPhoneRepo tests")]
    public class PersonalPhoneRepoTests
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
            Assert.Throws<ArgumentNullException>(() => new PersonalPhoneRepo(null));
        }

        [Test, Description("Test required context object")]
        public void Constructor_RequiredContext_Success()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    PersonalPhoneRepo sut = new PersonalPhoneRepo(context);
                    sut.Should().NotBeNull();
                }
            }
        }

        [Test, Description("Test if a person has a phone number")]
        public async Task DoesPersonHaveNumberAsync_PersonDoesNotExist_Fail()
        {
            using (IServiceScope serviceScope =
                   _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    PersonalPhoneRepo sut = new PersonalPhoneRepo(context);

                    bool result = await sut.DoesPersonHaveNumberAsync(Guid.Empty, string.Empty, string.Empty, string.Empty,
                        string.Empty);
                    result.Should().BeFalse();
                }
            }

        }

        [Test, Description("Test if a person has a phone number")]
        public async Task DoesPersonHaveNumberAsync_PersonDoesHaveANumber_Success()
        {
            using (IServiceScope serviceScope =
                   _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {

                    Person person = await context.People.Include(p => p.Phones).FirstAsync();
                    Phone phone = person.Phones.First();

                    PersonalPhoneRepo sut = new PersonalPhoneRepo(context);

                    bool result = await sut.DoesPersonHaveNumberAsync(person.PersonId, phone.CountryCode,
                        phone.AreaCode, phone.Number, phone.PhoneType);
                    result.Should().BeTrue();
                }
            }

        }

        [Test, Description("Test adding a phone to a person")]
        public async Task AddPhoneNumberToPerson_PersonDoesNotExit_Success()
        {
            using (IServiceScope serviceScope =
                   _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    PersonalPhoneRepo sut = new PersonalPhoneRepo(context);

                     await sut.AddPhoneNumberToPerson(Guid.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                }
            }
        }

        [Test, Description("Test adding a phone to a person")]
        public async Task AddPhoneNumberToPerson_PersonExit_Success()
        {
            using (IServiceScope serviceScope =
                   _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    IPhone phone = PhoneFactory.GeneratePhoneData();

                    Person person = await context.People.FirstAsync();

                    PersonalPhoneRepo sut = new PersonalPhoneRepo(context);

                    await sut.AddPhoneNumberToPerson(person.PersonId, phone.CountryCode, phone.AreaCode, phone.Number, phone.PhoneType);

                    person = await context.People.Include(p => p.Phones).FirstAsync();
                    bool found = person.Phones.Any(personPhone => personPhone.AreaCode == phone.AreaCode && personPhone.Number == phone.Number && personPhone.CountryCode == phone.CountryCode && personPhone.PhoneType == phone.PhoneType);

                    found.Should().BeTrue();
                }
            }
        }

        [Test, Description("Test Get the default phone number")]
        public async Task GetDefaultPhoneNumberAsync_GetTheDefaultNumber_Success()
        {
            using (IServiceScope serviceScope =
                   _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {

                    Person person = await context.People
                        .Include(p => p.MtmPhones)
                        .ThenInclude(mtm => mtm.Phone).Include(person => person.Phones)
                        .FirstAsync();

                    foreach (MtmPhone personMtmPhone in person.MtmPhones)
                    {
                        personMtmPhone.DefaultNumber = false;
                    }

                    MtmPhone mtm = person.MtmPhones.First();
                    mtm.DefaultNumber = true;
                    Phone phone = mtm.Phone;
                    await context.SaveChangesAsync();

                    PersonalPhoneRepo sut = new PersonalPhoneRepo(context);
                    IPhone result = await sut.GetDefaultPhoneNumberAsync(person.PersonId);
                    result.Should().NotBeNull();
                    result.PhoneId.Should().NotBeEmpty();
                    result.PhoneId.Should().Be(phone.PhoneId);
                }
            }
        }

        [Test, Description("Get the phone numbers for a person")]
        public async Task GetAPersonsPhoneNumbersAsync_GetPhoneNumbers_Success()
        {
            using (IServiceScope serviceScope =
                   _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {

                    Person person = await context.People
                        .Include(person => person.Phones)
                        .FirstAsync();

                    PersonalPhoneRepo sut = new PersonalPhoneRepo(context);
                    IEnumerable<IPhone> results = await sut.GetAPersonsPhoneNumbersAsync(person.PersonId);
                    results.Should().NotBeNull();
                    results.Count().Should().Be(1);
                    results.First().Should().NotBeNull();
                    results.First().PhoneId.Should().Be(person.Phones.First().PhoneId);
                }
            }
        }
    }
}