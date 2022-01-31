using AutoFixture;
using AutoFixture.AutoMoq;
using BL.Interfaces;
using BL.Validators;
using BL.Validators.CustomExceptions;
using DAL.Entities;
using Xunit;

namespace Tests.Validators
{
    public class ValidatorTest
    {
        private readonly Fixture _fixture;
        private readonly IValidator<Root> _validator;

        public ValidatorTest()
        {
            _fixture = (Fixture)new Fixture().Customize(new AutoMoqCustomization());
            _validator = _fixture.Create<Validator<Root>>();
        }

        [Fact]
        public void Validate_IfEntityIsNull_ValidationIsFailed()
        {
            //Arrange
            Root input = null;

            //Act
            void actualResult() => _validator.Validate(input);

            //Assert
            Assert.Throws<ValidatorException>((actualResult));
        }

        [Fact]
        public void ValidateCityName_IfCityNameIsNullOrWhiteSpace_ValidationIsFailed()
        {
            //Arrange
            string cityName = string.Empty;

            //Act
            void actualResult() => _validator.ValidateCityName(cityName);

            //Assert
            Assert.Throws<ValidatorException>((actualResult));
        }
    }
}
