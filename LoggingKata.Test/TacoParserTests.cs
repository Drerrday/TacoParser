using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldDoSomething()
        {
            // Arrange
            TacoParser parser = new TacoParser();
            string cutter = "34.039588,-84.283254,Taco Bell Alpharetta...";
            string expected = "Taco Bell Alpharetta...";
            // Act
            var actual = parser.Parse(cutter);
            // Assert
            Assert.Equal(expected, actual.Name);
        }

        [Theory]
        [InlineData("34.039588,-84.283254,Taco Bell Alpharetta...", -84.283254)]
        [InlineData("33.556383,-86.889051,Taco Bell Birmingha...", -86.889051)]
        [InlineData("34.206722,-86.873404,Taco Bell Cullman...", -86.873404)]
        [InlineData("34.571424,-86.973028,Taco Bell Decatur...", -86.973028)]
        [InlineData("34.113051,-84.56005,Taco Bell Woodstoc...", -84.56005)]
        public void ShouldParse(string testLine, double expected)
        {
            // Arrange
            TacoParser parser = new TacoParser();
            // Act
            var test = parser.Parse(testLine);
            // Assert
            Assert.Equal(expected, test.Location.Longitude);
        }

        [Fact]
        public void ShouldFailParse()
        {
            // Arrange
            TacoParser parser = new TacoParser();
            string tbell1 = "-84.283254,Taco Bell Alpharetta...";
            string tbell2 = "34.206722,-86.873404";
            string tbell3 = "Taco Bell Decatur...";
            string tbell4 = "null ,-84.283254,Taco Bell Alpharetta...";
            string tbell5 = "null, -84.283254 , null";
            string tbell6 = "33.283584, -86.855317, ";

            // Act
            var actual1 = parser.Parse(tbell1);
            var actual2 = parser.Parse(tbell2);
            var actual3 = parser.Parse(tbell3);
            var actual4 = parser.Parse(tbell4);
            var actual5 = parser.Parse(tbell5);
            var actual6 = parser.Parse(tbell6);

            // Assert
            Assert.Equal(null, actual1);
            Assert.Equal(null, actual2);
            Assert.Equal(null, actual3);
            Assert.Equal(null, actual4);
            Assert.Equal(null, actual5);
            Assert.Equal(null, actual6);
        }
    }
}
