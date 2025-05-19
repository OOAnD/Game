using System;
using System.Collections.Generic;
using System.IO;

namespace SpaceBattle;


public class QuadrupleFileProcessing
{

    public static void Write(List<(int, int, int, int)> collection, string filePath)
    {
        using var fileStream = File.Create(filePath);
        using var writer = new BinaryWriter(fileStream);
        
        writer.Write(collection.Count);
        
        foreach (var (a, b, c, d) in collection)
        {
            writer.Write(a);
            writer.Write(b);
            writer.Write(c);
            writer.Write(d);
        }
    }


    public static List<(int, int, int, int)> Read(string filePath)
    {
        var result = new List<(int, int, int, int)>();
        
        using var fileStream = File.OpenRead(filePath);
        using var reader = new BinaryReader(fileStream);
        
        var count = reader.ReadInt32();
        result.Capacity = count;
        
        for (var i = 0; i < count; i++)
        {
            result.Add((
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32()
            ));
        }
        
        return result;
    }
}