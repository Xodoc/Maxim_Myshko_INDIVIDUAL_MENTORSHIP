using AutoFixture;
using AutoFixture.AutoMoq;
using BL.Interfaces;
using BL.Validators;
using BL.Validators.CustomExceptions;
using Shared.Config;
using Shared.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace Tests.Validators
{
    public class ValidatorTest
    {
        private readonly IConfiguration _config;
        private readonly IValidator _validator;
        private readonly Fixture _fixture;

        public ValidatorTest()
        {
            _config = new ConfigurationTest();
            _validator = new Validator(_config);
            _fixture = (Fixture)new Fixture().Customize(new AutoMoqCustomization());
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ValidateCityName_IfCityNameIsNullOrWhiteSpace_ValidationIsFailed(string name)
        {
            //Arrange
            string cityName = name;

            //Act
            void actualResult() => _validator.ValidateCityName(cityName);

            //Assert
            Assert.Throws<ValidatorException>((actualResult));
        }

        [Fact]
        public void ValidateCityName_IfInputDataIsCorrect_ValidationIsSuccessfully()
        {
            //Arrange
            var cityName = "Minsk";

            //Act
            var actualResult = Record.Exception(() => _validator.ValidateCityName(cityName));

            //Assert
            Assert.Null(actualResult);
        }

        [Theory]
        [InlineData("Minsk", 3)]
        public void ValidateModel_IfInputDataIsCorrect_ValidationIsSuccessfully(string cityName, int days)
        {
            //Arrange

            //Act
            var actualResult = Record.Exception(() => _validator.ValidateModel(cityName, days));

            //Assert
            Assert.Null(actualResult);
        }

        [Theory]
        [InlineData("Minsk", 0)]
        [InlineData("", 23)]
        [InlineData(" ", -1)]
        public void ValidateModel_IfInputDataIsIncorrect_ValidationIsFailed(string cityName, int days)
        {
            //Arrange 

            //Act
            void actualResult() => _validator.ValidateModel(cityName, days);

            //Assert
            Assert.Throws<ValidatorException>(actualResult);
        }

        [Fact]
        public void ValidateCityNames_IfInputDataIsCorrect_ValidationIsFailed()
        {
            //Arrange
            var cityNames = _fixture.Create<List<string>>();

            //Act
            var actualResult = Record.Exception(() => _validator.ValidateCityNames(cityNames));

            //Assert
            Assert.Null(actualResult);
        }

        [Fact]
        public void ValidateCityNames_IfInputDataIsIncorrect_ValidationIsFailed()
        {
            //Arrange            
            var cityNames = new List<string>();

            //Act
            void actualResult() => _validator.ValidateCityNames(cityNames);

            //Assert
            Assert.Throws<ValidatorException>(actualResult);
        }
    }
}
