using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using BoardSlide.API.Domain.Entities;
using BoardSlide.API.Infrastructure.Persistence;

namespace BoardSlide.API.Infrastructure.Services.Repositories
{
    public class BoardsRepository : Repository<Board>, IBoardsRepository
    {
        protected ApplicationDbContext Context => base.BaseContext as ApplicationDbContext;

        public BoardsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Board> GetBoardAsync(int id, bool includeCardLists = false, bool includeCards = false)
        {
            IQueryable<Board> boards = Context.Boards.AsQueryable();

            if (includeCardLists)
            {
                var includable = boards.Include(board => board.CardLists);
                boards = includeCards ? (IQueryable<Board>)includable.ThenInclude(list => list.Cards) : includable;
            }
            
            return await boards.SingleOrDefaultAsync(board => board.Id == id);
        }
    }
}