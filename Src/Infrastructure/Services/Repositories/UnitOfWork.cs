using System.Threading.Tasks;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using BoardSlide.API.Infrastructure.Persistence;

namespace BoardSlide.API.Infrastructure.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBoardsRepository Boards { get; private set; }
        public ICardListsRepository CardLists { get; private set; }
        public ICardsRepository Cards { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Boards = new BoardsRepository(context);
            CardLists = new CardListsRepository(context);
            Cards = new CardsRepository(context);
        }

        public Task CompleteAsync() => _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}