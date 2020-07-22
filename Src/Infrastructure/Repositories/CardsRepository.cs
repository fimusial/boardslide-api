using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using BoardSlide.API.Domain.Entities;
using BoardSlide.API.Infrastructure.Persistence;

namespace BoardSlide.API.Infrastructure.Repositories
{
    public class CardsRepository : Repository<Card>, ICardsRepository
    {
        protected ApplicationDbContext Context => base.BaseContext as ApplicationDbContext;

        public CardsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Card> GetCardWithCardListIdAndBoardIdAsync(int id, int cardListId, int boardId)
        {
            return await Context.Cards
                .Where(card => card.CardListId == cardListId)
                .Where(card => card.CardList.BoardId == boardId)
                .SingleOrDefaultAsync(card => card.Id == id);
        }
    }
}