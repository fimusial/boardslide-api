using System.Threading.Tasks;
using BoardSlide.API.Domain.Entities;

namespace BoardSlide.API.Application.Common.Interfaces.Repositories
{
    public interface ICardListsRepository : IRepository<CardList>
    {
        Task<CardList> GetCardListWithBoardIdAsync(int id, int boardId, bool includeCards = false);
    }
}