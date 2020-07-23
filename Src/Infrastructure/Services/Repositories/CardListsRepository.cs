using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using BoardSlide.API.Domain.Entities;
using BoardSlide.API.Infrastructure.Persistence;

namespace BoardSlide.API.Infrastructure.Services.Repositories
{
    public class CardListsRepository : Repository<CardList>, ICardListsRepository
    {
        protected ApplicationDbContext Context => base.BaseContext as ApplicationDbContext;

        public CardListsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CardList> GetCardListWithBoardIdAsync(int id, int boardId, bool includeCards = false)
        {
            IQueryable<CardList> cardLists = Context.CardLists.AsQueryable();
            cardLists = includeCards ? cardLists.Include(list => list.Cards) : cardLists;
            return await cardLists
                .Where(list => list.BoardId == boardId)
                .SingleOrDefaultAsync(list => list.Id == id);
        }
    }
}