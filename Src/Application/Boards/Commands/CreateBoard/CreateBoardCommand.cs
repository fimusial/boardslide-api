using BoardSlide.API.Application.Boards.Responses;
using MediatR;

namespace BoardSlide.API.Application.Boards.Commands.CreateBoard
{
    public class CreateBoardCommand : IRequest<BoardInfoResponse>
    {
        public string Name { get; set; }
    }
}