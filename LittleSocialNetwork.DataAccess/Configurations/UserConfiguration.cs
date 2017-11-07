using LittleSocialNetwork.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LittleSocialNetwork.DataAccess.Configurations
{
    public class UserConfiguration : BaseConfiguration<User>
    {
        public override void ConfigureSpecific(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Email)
                .IsRequired();

            builder.Property(u => u.Password)
                .IsRequired();

            builder.Property(u => u.Role)
                .IsRequired();
        }
    }
}