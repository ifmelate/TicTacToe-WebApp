using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicTacToe.Models.Entity.Configuration
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.Property(d => d.Name).HasMaxLength(30);

            builder.HasOne(d => d.GameSide).WithMany(d => d.Players).HasForeignKey(d => d.GameSideId);

            builder.HasOne(d => d.User).WithMany(d => d.Players).HasForeignKey(d => d.UserId);

            builder.HasData(new List<Player>
            {
                new Player {Id = 1, Name = "PC", GameSideId = 1, UserId = null},
                new Player {Id = 2, Name = "PC", GameSideId = 2, UserId = null}
            });

        }
    }
}