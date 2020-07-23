using System;

namespace BoardSlide.API.Server.Contracts
{
    public interface IUriService
    {
        Uri GetBoardUri(int boardId);
        Uri GetCardListUri(int boardId, int cardListId);
        Uri GetCardUri(int boardId, int cardListId, int cardId);
    }
}