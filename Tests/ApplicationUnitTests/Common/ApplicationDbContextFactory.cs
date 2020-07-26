using System;
using Microsoft.EntityFrameworkCore;
using BoardSlide.API.Domain.Entities;
using BoardSlide.API.Infrastructure.Persistence;

namespace BoardSlide.API.ApplicationUnitTests.Common
{
    public class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.EnsureCreated();

            var userId1 = "1";
            var userId2 = "2";

            context.Boards.AddRange
            (
                new Board() { Id = 1, Name = "board1", OwnerId = userId1 },
                new Board() { Id = 2, Name = "board2", OwnerId = userId2 },

                new Board() { Id = 3, Name = "empty", OwnerId = userId2 }
            );

            context.CardLists.AddRange
            (
                new CardList() { Id = 11, Name = "card-list11", BoardId =  1 },
                new CardList() { Id = 12, Name = "card-list12", BoardId =  1 },

                new CardList() { Id = 21, Name = "card-list21", BoardId =  2 },
                new CardList() { Id = 22, Name = "card-list22", BoardId =  2 },

                new CardList() { Id = 23, Name = "empty", BoardId =  2 }
            );

            context.Cards.AddRange
            (
                new Card() {Id = 111, Name= "card111", Description = "desc111", DueDate = DateTime.Now, CardListId = 11 },
                new Card() {Id = 112, Name= "card112", Description = "desc112", DueDate = null, CardListId = 11 },

                new Card() {Id = 121, Name= "card121", Description = "desc121", DueDate = DateTime.Now, CardListId = 12 },
                new Card() {Id = 122, Name= "card122", Description = null, DueDate = null, CardListId = 12 },

                new Card() {Id = 211, Name= "card211", Description = "desc211", DueDate = DateTime.Now, CardListId = 21 },
                new Card() {Id = 212, Name= "card212", Description = "desc212", DueDate = null, CardListId = 21 },

                new Card() {Id = 221, Name= "card221", Description = "desc221", DueDate = DateTime.Now, CardListId = 22 },
                new Card() {Id = 222, Name= "card222", Description = null, DueDate = null, CardListId = 22 }
            );

            context.SaveChanges();

            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}