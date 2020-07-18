using System.Threading.Tasks;
using BoardSlide.API.Domain.Entities;

namespace BoardSlide.API.Application.Common.Interfaces.Repositories
{
    public interface ICardsRepository : IRepository<Card>
    {
        Task<Card> GetCardWithCardListIdAndBoardIdAsync(int id, int cardListId, int boardId);
    }
}