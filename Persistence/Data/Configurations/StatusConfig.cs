using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class StatusConfig : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.Property((prop) => prop.NameStatus).IsRequired();

            builder.Property((prop) => prop.IdUserCreated).IsRequired();

            builder.Property((prop) => prop.UpdateDate).IsRequired();

            builder.Property((prop) => prop.CreateDate).IsRequired();

            builder.Property((prop) => prop.IsActive).IsRequired();
        }
    }
}
