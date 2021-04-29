using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicTacToe.Models.Entity.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();
            builder.Property(p => p.Id).HasColumnName("UserId");

            builder.Property(d => d.Ip).HasMaxLength(45);

            builder.Property(t => t.EntryDate)
                .ValueGeneratedOnAddOrUpdate();

        }
    }
}