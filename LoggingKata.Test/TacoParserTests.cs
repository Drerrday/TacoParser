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
            string a1 = "-84.283254,Taco Bell Alpharetta...";
            string a2 = "34.206722,-86.873404";
            string a3 = "Taco Bell Decatur...";
            string a4 = "null ,-84.283254,Taco Bell Alpharetta...";
            string a5 = "null, -84.283254 , null";
            string a6 = "33.283584, -86.855317, ";

            // Act
            var actual1 = parser.Parse(a1);
            var actual2 = parser.Parse(a2);
            var actual3 = parser.Parse(a3);
            var actual4 = parser.Parse(a4);
            var actual5 = parser.Parse(a5);
            var actual6 = parser.Parse(a6);

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
