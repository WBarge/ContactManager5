using System;
using System.Linq;
using System.Threading.Tasks;
using Contact.Data.EF;
using Contact.Data.EF.ConcretePocos;
using Contact.Data.EF.ConcreteRepos;
using Contact.Glue.Interfaces.DTOs;
using Contact.Tests.TestDataFactories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Contact.Tests.Data.Repos
{
    [TestFixture, Description("PhoneRepo tests")]
    public class PhoneRepoTests
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
            Assert.Throws<ArgumentNullException>(() => new PhoneRepo(null));
        }

        [Test, Description("Test required context object")]
        public void Constructor_RequiredContext_Success()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    PhoneRepo sut = new PhoneRepo(context);
                    sut.Should().NotBeNull();
                }
            }
        }

        [Test, Description("Test getting a phone")]
        public async Task GetPhoneAsync_GetPhone_Success()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    Phone phone = context.PhoneNumbers.First();
                    PhoneRepo sut = new PhoneRepo(context);
                    IPhone result = await sut.GetPhoneAsync(phone.PhoneId);
                    phone.Should().NotBeNull();
                }
            }
        }

        [Test, Description("Test adding a phone")]
        public async Task InsertAsync_AddPhone_Success()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    IPhone phone = PhoneFactory.GeneratePhoneData();
                    phone.PhoneId = Guid.Empty;
                    PhoneRepo sut = new PhoneRepo(context);
                    Guid result = await sut.InsertAsync(phone);
                    phone.Should().NotBeNull();

                    context.PhoneNumbers.ToList().Should().HaveCountGreaterOrEqualTo(2);
                }
            }
        }

        [Test, Description("Test updating a phone")]
        public void Update_UpdatePhone_Success()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    string testData = "123";
                    Phone phone = context.PhoneNumbers.First();
                    phone.AreaCode = testData;
                    PhoneRepo sut = new PhoneRepo(context);
                    sut.Update(phone);
                    context.PhoneNumbers.First().AreaCode.Should().BeEquivalentTo(testData);

                }
            }
        }

        [Test, Description("Test Delete a phone")]
        public void Delete_UpdatePhone_Success()
        {
            using (IServiceScope serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (ContactDbContext context = serviceScope.ServiceProvider.GetRequiredService<ContactDbContext>())
                {
                    Phone phone = context.PhoneNumbers.First();
                    PhoneRepo sut = new PhoneRepo(context);
                    sut.Delete(phone);
                    context.PhoneNumbers.First().Deleted.Should().BeTrue();

                }
            }
        }

    }
}