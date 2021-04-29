using System;
using System.Threading.Tasks;

namespace TicTacToe.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        void Commit();
        Task CommitAsync();
        void Rollback();

        IPlayerRepository Players { get; }

    }
}