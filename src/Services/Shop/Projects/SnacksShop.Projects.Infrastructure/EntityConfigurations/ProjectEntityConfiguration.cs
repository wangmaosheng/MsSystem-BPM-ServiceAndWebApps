using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SnacksShop.Projects.Domain.AggregatesModel;

namespace SnacksShop.Projects.Infrastructure.EntityConfigurations
{
    public class ProjectEntityConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("project").HasKey(m => m.Id);

            builder.Ignore(m => m.DomainEvents);
        }
    }
}
