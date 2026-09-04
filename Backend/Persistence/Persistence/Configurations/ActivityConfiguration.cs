using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .IsRequired()
            .HasMaxLength(36);

        builder.Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(265);

        builder.Property(a => a.Date)
            .IsRequired();

        builder.Property(a => a.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(a => a.Category)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Venue)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.IsCancelled)
            .IsRequired();

        builder.Property(a => a.Latitude)
            .IsRequired();

        builder.Property(a => a.Longitude)
            .IsRequired();

        builder.ToTable("Activities", table =>
        {
            table.HasCheckConstraint("CK_Activities_Title_MinLength", "LEN([Title]) >= 3");
            table.HasCheckConstraint("CK_Activities_Description_MinLength", "LEN([Description]) >= 10");
            table.HasCheckConstraint("CK_Activities_Category_MinLength", "LEN([Category]) >= 2");
            table.HasCheckConstraint("CK_Activities_City_MinLength", "LEN([City]) >= 2");
            table.HasCheckConstraint("CK_Activities_Venue_MinLength", "LEN([Venue]) >= 2");
        });

        builder.HasIndex(a => new { a.IsCancelled, a.Date });
        builder.HasIndex(a => new { a.Category, a.Date });
    }
}
