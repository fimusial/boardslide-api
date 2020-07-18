namespace BoardSlide.API.Server.Contracts
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public static class Boards
        {
            public const string Base = Root + "/boards";
            public const string ResourceId = "/{boardId}";

            public const string GetAll = Base;
            public const string GetById = Base + ResourceId;
            public const string Post = Base;
            public const string Put = Base + ResourceId;
            public const string Delete = Base + ResourceId;
        }

        public static class CardLists
        {
            public const string Base = Boards.Base + Boards.ResourceId + "/card-lists";
            public const string ResourceId = "/{cardListId}";

            public const string GetAll = Base;
            public const string GetById = Base + ResourceId;
            public const string Post = Base;
            public const string Put = Base + ResourceId;
            public const string Delete = Base + ResourceId;
        }

        public static class Cards
        {
            public const string Base = CardLists.Base + CardLists.ResourceId + "/cards";
            public const string ResourceId = "/{cardId}";

            public const string GetAll = Base;
            public const string GetById = Base + ResourceId;
            public const string Post = Base;
            public const string Put = Base + ResourceId;
            public const string Delete = Base + ResourceId;
        }

        public static class Identity
        {
            public const string Register = Root + "/register";
            public const string SignIn = Root + "/sign-in";
            public const string Refresh = Root + "/refresh";
        }
    }
}