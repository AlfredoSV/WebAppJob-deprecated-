using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class AreaConfig : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.Property((prop) => prop.NameArea).IsRequired();

            builder.Property((prop) => prop.DescriptionArea).IsRequired();

            builder.Property((prop) => prop.IdUserCreated).IsRequired();

            builder.Property((prop) => prop.UpdateDate).IsRequired();

            builder.Property((prop) => prop.CreateDate).IsRequired();

            builder.Property((prop) => prop.IsActive).IsRequired();
        }
    }
}
