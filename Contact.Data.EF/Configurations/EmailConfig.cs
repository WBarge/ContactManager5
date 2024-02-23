using Contact.Data.EF.ConcretePocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact.Data.EF.Configurations
{
    internal class EmailConfig : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.ToTable("Email");
            builder.HasKey(k => k.EmailId);
            builder.Property(p => p.EmailId)
                .IsRequired(true)
                .ValueGeneratedOnAdd();
            builder.Property(p => p.Deleted)
                .IsRequired()
                .HasDefaultValue(false);
            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(255);

            //define 1-many relationship
            builder.HasOne<Person>(email=>email.Person)
                .WithMany(person => person.Emails);
        }
    }
}
