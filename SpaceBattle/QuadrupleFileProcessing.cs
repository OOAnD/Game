using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SpaceBattle;

public class QuadrupleFileProcessing
{
    private static int countOfOutput = 0;


    public static Stream Write(List<(int, int, int, int)> collection, string outputPath)
    {
        if (collection == null)
            throw new ArgumentNullException(nameof(collection), "Collection cannot be null");

        if (string.IsNullOrEmpty(outputPath))
            throw new ArgumentException("Output path cannot be null or empty", nameof(outputPath));

        var destinationFilePath = Path.Combine(outputPath, $"output{++countOfOutput}.bin");

        using (var fileStream = File.Create(destinationFilePath))
        using (var writer = new BinaryWriter(fileStream))
        {
            writer.Write(collection.Count);

            foreach (var (item1, item2, item3, item4) in collection)
            {
                writer.Write(item1);
                writer.Write(item2);
                writer.Write(item3);
                writer.Write(item4);
            }
        }

        return new FileStream(destinationFilePath, FileMode.Open);
    }

    public static List<(int, int, int, int)> Read(Stream stream)
    {
        if (stream == null)
            throw new ArgumentNullException(nameof(stream), "Input stream cannot be null");

        var result = new List<(int, int, int, int)>();

        using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
        {
            try
            {
                var count = reader.ReadInt32();
                result.Capacity = count;

                for (var i = 0; i < count; i++)
                {
                    var item1 = reader.ReadInt32();
                    var item2 = reader.ReadInt32();
                    var item3 = reader.ReadInt32();
                    var item4 = reader.ReadInt32();

                    result.Add((item1, item2, item3, item4));
                }
            }
            catch (EndOfStreamException ex)
            {
                throw new InvalidDataException("Unexpected end of file", ex);
            }
            catch (IOException ex)
            {
                throw new InvalidDataException("Error reading file", ex);
            }
        }

        return result;
    }
}
