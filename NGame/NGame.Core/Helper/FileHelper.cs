using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public static class FileHelper
{
    public static long GetLength(string path)
    {
        FileInfo file = new FileInfo(path);
        return file.Length;
    }

    public static Task Writr(string path, int star, byte[] bytes, int offset, int length)
    {
        return Task.Run(async () =>
        {
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, bytes.Length, true))
            {
                stream.Seek(star, SeekOrigin.Begin);
                await stream.WriteAsync(bytes, offset, length);
                return stream.FlushAsync();
            }
        });
    }

    public static Task<int> Read(string path, int star, byte[] bytes, int offset, int length)
    {
        using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, bytes.Length, true))
        {
            stream.Seek(star, SeekOrigin.Begin);
            return stream.ReadAsync(bytes, offset, length);
        }
    }
    public static void WriteAllText(string path, string info)
    {
        File.WriteAllText(path, info);
    }
    public static void WriteAllBytes(string path, byte[] bytes)
    {
        File.WriteAllBytes(path, bytes);
    }

    public static void WriteAllTextLocked<T>(string path, ref T info)
    {
        lock (info)
        {
            File.WriteAllText(path, LitJson.JsonMapper.ToJson(info));
        }
    }
    public static void Delete(string path)
    {
        File.Delete(path);
    }
}