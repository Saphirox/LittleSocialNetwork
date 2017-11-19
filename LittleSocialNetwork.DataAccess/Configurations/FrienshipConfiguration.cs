using LittleSocialNetwork.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LittleSocialNetwork.DataAccess.Configurations
{
    public class FrienshipConfiguration : BaseConfiguration<Friendship>
    {
        public override void ConfigureSpecific(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasOne(f => f.From)
                .WithMany(up => up.FriendsFromMe)
                .HasForeignKey(f => f.FromId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.To)
                .WithMany(up => up.FriendsToMe)
                .HasForeignKey(f => f.ToId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}