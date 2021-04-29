using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicTacToe.Models.Entity.Configuration
{
    public class MoveConfiguration : IEntityTypeConfiguration<Move>
    {
        public void Configure(EntityTypeBuilder<Move> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(d => d.Game).WithMany(d => d.Moves).HasForeignKey(d => d.GameId);

            builder.HasOne(d => d.Cell).WithMany(d => d.Moves).HasForeignKey(d => d.CellId);

            builder.HasOne(d => d.Player).WithMany(d => d.Moves).HasForeignKey(d => d.PlayerId);

        }
    }
}