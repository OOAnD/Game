namespace SpaceBattle.Tests
{
    public class QuadrupleFileProcessingTests : IDisposable
    {
        private const string TestFile = "test.bin";
        private const string TempFile = "temp.bin";

        public void Dispose()
        {
            CleanupFile(TestFile);
            CleanupFile(TempFile);
        }

        private static void CleanupFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        [Fact]
        public void Write_ShouldCreateValidFileStructure()
        {
            var data = new List<(int, int, int, int)>
            {
                (1, 2, 3, 4),
                (5, 6, 7, 8)
            };

            QuadrupleFileProcessing.Write(data, TestFile);

            using var reader = new BinaryReader(File.OpenRead(TestFile));
            Assert.Equal(data.Count, reader.ReadInt32());

            foreach (var (a, b, c, d) in data)
            {
                Assert.Equal(a, reader.ReadInt32());
                Assert.Equal(b, reader.ReadInt32());
                Assert.Equal(c, reader.ReadInt32());
                Assert.Equal(d, reader.ReadInt32());
            }

            Assert.True(reader.BaseStream.Position == reader.BaseStream.Length);
        }

        [Fact]
        public void Read_ShouldCorrectlyParseFile()
        {
            var expected = new List<(int, int, int, int)>
            {
                (10, 20, 30, 40),
                (50, 60, 70, 80)
            };

            using (var writer = new BinaryWriter(File.Create(TempFile)))
            {
                writer.Write(expected.Count);
                foreach (var (a, b, c, d) in expected)
                {
                    writer.Write(a);
                    writer.Write(b);
                    writer.Write(c);
                    writer.Write(d);
                }
            }

            var result = QuadrupleFileProcessing.Read(TempFile);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Read_NonExistentFile_ShouldThrowFileNotFound()
        {
            Assert.Throws<FileNotFoundException>(() =>
                QuadrupleFileProcessing.Read("non_existent_file.bin"));
        }
    }
}
