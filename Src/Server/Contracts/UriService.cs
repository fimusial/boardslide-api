using System;

namespace BoardSlide.API.Server.Contracts
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetBoardUri(int boardId)
        {
            return new Uri(_baseUri + ApiRoutes.Boards.GetById.Replace("{boardId}", boardId.ToString()));
        }

        public Uri GetCardListUri(int boardId, int cardListId)
        {
            string route = ApiRoutes.CardLists.GetById
                .Replace("{boardId}", boardId.ToString())
                .Replace("{cardListId}", cardListId.ToString());

            return new Uri(_baseUri + route);
        }

        public Uri GetCardUri(int boardId, int cardListId, int cardId)
        {
            string route = ApiRoutes.Cards.GetById
                .Replace("{boardId}", boardId.ToString())
                .Replace("{cardListId}", cardListId.ToString())
                .Replace("{cardId}", cardId.ToString());

            return new Uri(_baseUri + route);
        }
    }
}