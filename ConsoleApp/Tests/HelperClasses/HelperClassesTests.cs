using ConsoleApp.Commands.HelperClasses;
using Xunit;

namespace Tests.HelperClasses
{
    public class HelperClassesTests
    {
        [Theory]
        [InlineData("Minsk, Brest, Grodno")]
        [InlineData("Minsk, Brest,, Grodno")]
        [InlineData("Minsk,, Brest,Grodno")]
        [InlineData("Minsk, , Brest,Grodno")]
        public void SplitNames_IfInputDataIsCorrect_SuccessfullySplitting(string cityNames)
        {
            //Arrange
            var expected = new string[] { "Minsk", "Brest", "Grodno" };
            var stringSplit = new StringSplitter().SplitNames(cityNames);

            //Act
            var actualResult = stringSplit;

            //Assert
            Assert.Equal(expected, actualResult);
        }
    }
}
