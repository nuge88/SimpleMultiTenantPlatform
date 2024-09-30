using DocumentService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentService.Infrastructure.Persistence.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(d => d.Size)
                .IsRequired();

            builder.Property(d => d.StoragePath)
                .IsRequired()
                .HasMaxLength(1000);


            builder.HasIndex(d => d.UserId);
            builder.HasIndex(d => d.OrganizationId);
        }
    }
}