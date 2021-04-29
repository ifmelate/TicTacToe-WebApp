using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicTacToe.Models.Entity.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.Property(d => d.StartTime).ValueGeneratedOnAdd();

            builder.HasOne(d => d.WinnerPlayer).WithMany(d => d.WinnerGames).HasForeignKey(d => d.WinnerPlayerId);

            builder.HasOne(d => d.LoserPlayer).WithMany(d => d.LoserGames).HasForeignKey(d => d.LoserPlayerId);

        }
    }
}