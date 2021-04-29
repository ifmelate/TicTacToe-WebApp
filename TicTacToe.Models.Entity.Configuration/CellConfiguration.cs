using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TicTacToe.Models.Entity.Configuration
{
    public class CellConfiguration : IEntityTypeConfiguration<Cell>
    {
        public void Configure(EntityTypeBuilder<Cell> builder)
        {
            builder.HasKey(d => d.Id);
            builder.HasData(
                new List<Cell>
                {
                    new Cell{ Id = 1, Name = "1.1"},
                    new Cell{ Id = 2, Name = "1.2"},
                    new Cell{ Id = 3, Name = "1.3"},
                    new Cell{ Id = 4, Name = "2.1"},
                    new Cell{ Id = 5, Name = "2.2"},
                    new Cell{ Id = 6, Name = "2.3"},
                    new Cell{ Id = 7, Name = "3.1"},
                    new Cell{ Id = 8, Name = "3.2"},
                    new Cell{ Id = 9, Name = "3.3"}
                });
        }
    }
}