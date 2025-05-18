using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using SpaceBattle;

namespace SpaceBattle.Tests
{
    public class QuadrupleFileProcessingEssentialTests : IDisposable
    {
        private readonly string _testDir = Path.Combine(Path.GetTempPath(), "QuadTests");

        public QuadrupleFileProcessingEssentialTests()
        {
            Directory.CreateDirectory(_testDir);
        }

        [Fact]
        public void WriteRead_EmptyCollection_ShouldWork()
        {
            // Arrange
            var input = new List<(int, int, int, int)>();

            // Act
            var stream = QuadrupleFileProcessing.Write(input, _testDir);
            var result = QuadrupleFileProcessing.Read(stream);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void WriteRead_SingleQuadruple_ShouldMatch()
        {
            // Arrange
            var input = new List<(int, int, int, int)> { (1, 2, 3, 4) };

            // Act & Assert
            var stream = QuadrupleFileProcessing.Write(input, _testDir);
            Assert.Equal(input, QuadrupleFileProcessing.Read(stream));
        }

        [Fact]
        public void WriteRead_MultipleQuadruples_ShouldPreserveOrder()
        {
            // Arrange
            var input = new List<(int, int, int, int)>
            {
                (10, 20, 30, 40),
                (50, 60, 70, 80),
                (90, 100, 110, 120)
            };

            // Act
            var stream = QuadrupleFileProcessing.Write(input, _testDir);
            var result = QuadrupleFileProcessing.Read(stream);

            // Assert
            Assert.Equal(input, result);
        }

        [Fact]
        public void WriteRead_EdgeCases_ShouldHandleCorrectly()
        {
            // Arrange
            var input = new List<(int, int, int, int)>
            {
                (int.MaxValue, int.MinValue, 0, -1),
                (1, -999999, 2147483647, -2147483648)
            };

            // Act & Assert
            var stream = QuadrupleFileProcessing.Write(input, _testDir);
            Assert.Equal(input, QuadrupleFileProcessing.Read(stream));
        }

        [Fact]
        public void Read_NullStream_ShouldThrow()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                QuadrupleFileProcessing.Read(null));
        }

        public void Dispose()
        {
            if (Directory.Exists(_testDir))
            {
                Directory.Delete(_testDir, true);
            }
        }
    }
}