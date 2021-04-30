using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicTacToe.Models.Entity.Configuration
{
    public class LevelConfiguration : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            builder.HasKey(d => d.Id);
            builder.HasData(
                new List<Level>
                {
                    new Level{ Id = 1, Name = "Easy"},
                    new Level{ Id = 2, Name = "Hard"},
                });
        }
    }
}