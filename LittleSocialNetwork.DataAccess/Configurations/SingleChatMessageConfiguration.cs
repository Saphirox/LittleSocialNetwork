using LittleSocialNetwork.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LittleSocialNetwork.DataAccess.Configurations
{
    public class SingleChatMessageConfiguration : BaseConfiguration<SingleChatMessage>
    {
        public override void ConfigureSpecific(EntityTypeBuilder<SingleChatMessage> builder)
        {
            builder.HasOne(sm => sm.From)
                .WithMany(up => up.MessagesFromMe)
                .HasForeignKey(sm => sm.FromId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sm => sm.To)
                .WithMany(up => up.MessagesToMe)
                .HasForeignKey(sm => sm.ToId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(sm => sm.PostTime);
        }
    }
}