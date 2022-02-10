﻿using AutoFixture;
using AutoFixture.AutoMoq;
using BL.Interfaces;
using BL.Validators;
using BL.Validators.CustomExceptions;
using Xunit;

namespace Tests.Validators
{
    public class ValidatorTest
    {
        private readonly Fixture _fixture;
        private readonly IValidator _validator;

        public ValidatorTest()
        {
            _fixture = (Fixture)new Fixture().Customize(new AutoMoqCustomization());
            _validator = _fixture.Create<Validator>();
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
    }
}
