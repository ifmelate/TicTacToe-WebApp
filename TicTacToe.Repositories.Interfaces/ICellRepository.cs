using System.Collections;
using System.Collections.Generic;
using TicTacToe.Models.Entity;

namespace TicTacToe.Repositories.Interfaces
{
    public interface ICellRepository : IGenericRepository<Cell>
    {
        IEnumerable<Cell> GetAllWithMoves();
    }
}