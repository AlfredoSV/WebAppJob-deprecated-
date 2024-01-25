using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property((prop) => prop.NameCountry).IsRequired();

            builder.Property((prop) => prop.DescriptionCountry).IsRequired();

            builder.Property((prop) => prop.IdUserCreated).IsRequired();

            builder.Property((prop) => prop.UpdateDate).IsRequired();

            builder.Property((prop) => prop.CreateDate).IsRequired();

            builder.Property((prop) => prop.IsActive).IsRequired();
        }
    }
}
