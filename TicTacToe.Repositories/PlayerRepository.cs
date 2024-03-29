﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Data.EF;
using TicTacToe.Models.Entity;
using TicTacToe.Repositories.Interfaces;

namespace TicTacToe.Repositories
{
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        private readonly DbSet<Player> _dbSet;

        public PlayerRepository(DataContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext.Set<Player>();
        }


        public Player FindByUser(string ip)
        {
            return _dbSet.Include(d=>d.GameSide)
                .FirstOrDefault(d => d.User.Ip == ip);
        }

        public Player GetComputerPlayer(int gameSideId)
        {
            return _dbSet.Include(d => d.GameSide)
                .FirstOrDefault(d => d.UserId == null && d.GameSideId == gameSideId);
        }

        public Player GetWithGameSide(int playerId)
        {
            return _dbSet.Include(d => d.GameSide).FirstOrDefault(s => s.Id == playerId);
        }

        public Player FindByUserId(int userId)
        {
            return _dbSet.Include(d => d.GameSide).FirstOrDefault(s => s.User.Id == userId);
        }
    }
}