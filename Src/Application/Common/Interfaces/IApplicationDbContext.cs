using BoardSlide.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BoardSlide.API.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoItem> TodoItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}