using System;
using System.Threading.Tasks;

namespace BoardSlide.API.Application.Common.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBoardsRepository Boards { get; }
        ICardListsRepository CardLists { get; }
        ICardsRepository Cards { get; }
        
        Task CompleteAsync();
    }
}