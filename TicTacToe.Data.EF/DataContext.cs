using Microsoft.EntityFrameworkCore;
using TicTacToe.Models.Entity.Configuration;

namespace TicTacToe.Data.EF
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CellConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new MoveConfiguration());
            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new GameSideConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());

        }

    }
}