using AutoMapper;
using BL.DTOs;
using BL.EqualityComparers;
using BL.Interfaces;
using DAL.Database;
using DAL.Entities.WeatherHistoryEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntegrationTests.Services
{
    public class CityServiceIntegrationTest : IDisposable
    {
        private readonly IMapper _mapper;
        private readonly ICityService _cityService;
        private readonly IDbContextTransaction _transaction;
        private readonly ApplicationDbContext _context;

        //Setup
        public CityServiceIntegrationTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddDI().AddLogging().BuildServiceProvider();

            _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetService<IMapper>();
            _cityService = serviceProvider.GetService<ICityService>();
            _transaction = _context.Database.BeginTransaction();
        }

        //Teardown
        public async void Dispose()
        {
            await _transaction.RollbackAsync();

            GC.SuppressFinalize(this);
        }

        [Fact]
        public async void CheckAndCreateCitiesAsync_IfCitiesDoesntExist_CreateCity()
        {
            //Arrange
            await _context.Cities.AddAsync(new City { CityName = "Paris" });
            await _context.SaveChangesAsync();

            var cities = await _context.Cities.ToListAsync();
            var expected = _mapper.Map<List<CityDTO>>(cities);

            //Act
            var actualResult = await _cityService.CheckAndCreateCitiesAsync();

            //Assert
            Assert.Equal(expected, actualResult, new CityDTOComparator());
        }
    }
}
