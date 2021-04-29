using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicTacToe.Models.Entity.Configuration
{
    public class GameSideConfiguration : IEntityTypeConfiguration<GameSide>
    {
        public void Configure(EntityTypeBuilder<GameSide> builder)
        {
            builder.HasKey(d => d.Id);
            builder.HasData(
                new List<GameSide>
                {
                    new GameSide{ Id = 1, Name = "Crosses"},
                    new GameSide{ Id = 2, Name = "Zeros"},
                });
        }
    }
}