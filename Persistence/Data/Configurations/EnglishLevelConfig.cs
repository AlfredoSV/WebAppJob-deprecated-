using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class EnglishLevelConfig : IEntityTypeConfiguration<EnglishLevel>
    {
        public void Configure(EntityTypeBuilder<EnglishLevel> builder)
        {
            builder.Property((prop) => prop.NameLevel).IsRequired();

            builder.Property((prop) => prop.IdUserCreated).IsRequired();

            builder.Property((prop) => prop.UpdateDate).IsRequired();

            builder.Property((prop) => prop.CreateDate).IsRequired();

            builder.Property((prop) => prop.IsActive).IsRequired();
        }
    }
}
