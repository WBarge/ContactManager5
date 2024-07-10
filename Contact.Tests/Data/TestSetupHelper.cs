using System;
using Contact.Data.EF;
using Contact.Data.EF.ConcretePocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Contact.Tests.Data
{
    public static class TestSetupHelper
    {
        public static IServiceProvider GetServiceProvider()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<ContactDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseInMemoryDatabase("TestDb");
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        public static void SeedData(this IServiceScope serviceScope)
        {
                ContactDbContext context = serviceScope.ServiceProvider.GetService<ContactDbContext>();
                Person person1 = new()
                {
                    First = "foo",
                    Last = "bar",
                    Deleted = false
                };
                Guid.NewGuid();
                Email email = new()
                {
                    Address = "test@gmail.com",
                    Deleted = false,
                    DefaultEmail = true
                };
                person1.Emails = new[] { email };
                Phone phone = new()
                {
                    CountryCode = "1",
                    AreaCode = "213",
                    Number = "5551212",
                    PhoneType = "cell"
                };
                person1.Phones = new[] { phone };
                context.People.Add(person1);
                context.SaveChanges();
        }

        public static void RemoveData(this IServiceScope serviceScope)
        {
            ContactDbContext context = serviceScope.ServiceProvider.GetService<ContactDbContext>();
            foreach (Person person in context.People.Include(p=>p.Emails))
            {
                foreach (Email personEmail in person.Emails)
                {
                    context.Remove(personEmail);
                }
                context.Remove(person);
            }

            context.SaveChanges();

        }
    }
}