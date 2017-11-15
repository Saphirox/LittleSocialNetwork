using LittleSocialNetwork.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LittleSocialNetwork.DataAccess.Configurations
{
    public class UserProfileConfiguration : BaseConfiguration<UserProfile>
    {
        public override void ConfigureSpecific(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasMany(sm => sm.MessagesFromMe)
                .WithOne(up => up.From)
                .HasForeignKey(sm => sm.FromId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(sm => sm.MessagesToMe)
                .WithOne(up => up.To)
                .HasForeignKey(sm => sm.ToId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}