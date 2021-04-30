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

            builder.Property(d => d.StartDateTime).HasColumnName("StartTime")
                .HasDefaultValueSql("getdate()")
                .ValueGeneratedOnAdd();
            builder.Property(d => d.EndDateTime).HasColumnName("EndTime");
            builder.HasOne(d => d.Player)
                .WithMany(d => d.Games)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.Restrict); 
            builder.Property(s => s.ComputerPlayerId).IsRequired();
            builder.HasOne(d => d.WinnerPlayer).WithMany(d => d.WinnerGames).HasForeignKey(d => d.WinnerPlayerId);

            builder.HasOne(d => d.LoserPlayer).WithMany(d => d.LoserGames).HasForeignKey(d => d.LoserPlayerId);

            builder.HasOne(s => s.Level).WithMany(s => s.Games).HasForeignKey(v => v.LevelId);

        }
    }
}