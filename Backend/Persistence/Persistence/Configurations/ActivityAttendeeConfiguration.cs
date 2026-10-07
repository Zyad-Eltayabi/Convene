using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ActivityAttendeeConfiguration : IEntityTypeConfiguration<ActivityAttendee>
{
    public void Configure(EntityTypeBuilder<ActivityAttendee> builder)
    {
       builder.HasKey(aa => new { aa.UserId, aa.ActivityId });
        builder.Property(aa => aa.IsHost)
            .IsRequired();
        builder.Property(aa => aa.DateJoined)
            .IsRequired();
        builder.HasOne(aa => aa.User)
            .WithMany(u => u.Activities)
            .HasForeignKey(aa => aa.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(aa => aa.Activity)
            .WithMany(a => a.Attendees)
            .HasForeignKey(aa => aa.ActivityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}