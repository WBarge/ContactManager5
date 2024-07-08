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
    [TestFixture, Description("PersonRepo tests")]
    public class PersonRepoTests
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
            Assert.Throws<ArgumentNullException>(() => new PersonRepo(null));
        }

        [Test, Description("Test required context object")]
        public void Constructor_RequiredContext_Success()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    PersonRepo sut = new PersonRepo(context);
                    sut.Should().NotBeNull();
                }
            }
        }

        [Test, Description("Test getting all people in the system")]
        public async Task GetAllAsync_GetEveryone_Success()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    PersonRepo sut = new PersonRepo(context);
                    IEnumerable<IPerson> result = await sut.GetAllAsync();
                    result.Should().NotBeNull();
                    result.Should().HaveCountGreaterOrEqualTo(1);
                }
            }
        }

        [Test, Description("Test getting a person from the system")]
        public async Task GetPersonAsync_GetAPerson_Fail()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    PersonRepo sut = new PersonRepo(context);
                    IPerson result = await sut.GetPersonAsync(Guid.Empty);
                    result.Should().BeNull();
                }
            }
        }

        [Test, Description("Test getting a person from the system")]
        public async Task GetPersonAsync_GetAPerson_Success()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    Person person = context.People.First();
                    PersonRepo sut = new PersonRepo(context);
                    IPerson result = await sut.GetPersonAsync(person.PersonId);
                    result.Should().NotBeNull();
                    result.PersonId.Should().Be(person.PersonId);
                }
            }
        }

        [Test, Description("Test adding a person")]
        public async Task InsertAsync_AddAPerson_Success()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    IPerson newPerson = PersonFactory.GetPerson();
                    newPerson.PersonId = Guid.Empty;
                    PersonRepo sut = new PersonRepo(context);

                    Guid result = await sut.InsertAsync(newPerson);
                    result.Should().NotBeEmpty();
                    context.People.Should().HaveCountGreaterOrEqualTo(2);
                }
            }
        }

        [Test, Description("Test updating a person")]
        public async Task UpdateAsync_ChangeAPerson_Success()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    string testName = "newName";
                    IPerson person = context.People.First();
                    person.First = testName;
                    PersonRepo sut = new PersonRepo(context);

                    await sut.UpdateAsync(person);

                    context.People.First().First.Should().BeEquivalentTo(testName);
                }
            }
        }

        [Test,Description("Test marking a person as deleted")]
        public async Task DeleteAsync_MarkAPersonAsDeleted_Success()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    IPerson person = context.People.First();
                    PersonRepo sut = new PersonRepo(context);

                    await sut.DeleteAsync(person);
                    context.People.First().Deleted.Should().BeTrue();
                }
            }
        }

        

        [Test, Description("Test creating a person object")]
        public void Create_ObjIsCreated_Success()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    PersonRepo sut = new PersonRepo(context);
                    IPerson result = sut.Create();
                    result.Should().NotBeNull();

                }
            }
        }

    }
}