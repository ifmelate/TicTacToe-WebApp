﻿using TicTacToe.Models.Entity;

namespace TicTacToe.Repositories.Interfaces
{
    public interface IPlayerRepository : IGenericRepository<Player>
    {
        Player FindByUser(string ip);
        Player GetComputerPlayer(int gameSideId);
        Player GetWithGameSide(int playerId);
        Player FindByUserId(int userId);
    }
}