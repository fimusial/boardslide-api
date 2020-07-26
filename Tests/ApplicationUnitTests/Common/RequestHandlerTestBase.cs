using System;
using Microsoft.Extensions.Options;
using AutoMapper;
using BoardSlide.API.Application.Common.Interfaces.Identity;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using BoardSlide.API.Application.Common.Mappings;
using BoardSlide.API.Application.Common.Settings;
using BoardSlide.API.Infrastructure.Services.Repositories;
using Moq;

namespace BoardSlide.API.ApplicationUnitTests.Common
{
    public class RequestHandlerTestBase : IDisposable
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public RequestHandlerTestBase()
        {
            _unitOfWork = new UnitOfWork(ApplicationDbContextFactory.Create());

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EntityToResponseProfile>();
                cfg.AddProfile<RequestToEntityProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        protected ICurrentUserService GetCurrentUserMock(string userId)
        {
            var currentUserMock = new Mock<ICurrentUserService>();
            currentUserMock
                .Setup(x => x.UserId)
                .Returns(userId);

            return currentUserMock.Object;
        }

        protected IOptions<ApplicationSettings> GetApplicationSettingsMock(int cardListsPerBoardLimit, int cardsPerCardListLimit)
        {
            var applicationSettingsMock = new Mock<IOptions<ApplicationSettings>>();
            applicationSettingsMock
                .Setup(mock => mock.Value)
                .Returns(new ApplicationSettings()
                {
                    CardListsPerBoardLimit = cardListsPerBoardLimit,
                    CardsPerCardListLimit = cardsPerCardListLimit
                });
            
            return applicationSettingsMock.Object;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}