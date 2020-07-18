using System.Threading.Tasks;
using BoardSlide.API.Domain.Entities;

namespace BoardSlide.API.Application.Common.Interfaces.Repositories
{
    public interface IBoardsRepository : IRepository<Board>
    {
        Task<Board> GetBoardAsync(int id, bool includeCardLists = false, bool includeCards = false);
    }
}