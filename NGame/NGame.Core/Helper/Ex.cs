using NGame.Component;
using NGame.Entity;
using System;
using System.Text;

public static class Ex
{
    public static void Log(object o)
    {
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}] {o}");
        if (AppSettings.LogEvent == null)
        {
 
            return;
        }
        AppSettings.LogEvent(o);
    }
    public static string ToHex(this byte[] msg)
    {
        StringBuilder strBuider = new StringBuilder();
        for (int index = 0; index < msg.Length; index++)
        {
            strBuider.Append(((int)msg[index]).ToString("X2"));
        }
        return strBuider.ToString();
    }
    public static byte[] HexToString(this string str)
    {
        int count = str.Length / 2;
        byte[] bs = new byte[count];
        for (int i = 0; i < count; i++)
        {
            string item = str.Substring(i * 2, 2);
            bs[i] = Convert.ToByte(item, 16);
        }
        return bs;
    }
    public static string GetFieldTypeName(this Type type)
    {
        switch (Type.GetTypeCode(type))
        {
            case TypeCode.Boolean:
                return "bit";
            case TypeCode.Byte:
                return "tinyint";
            case TypeCode.Int16:
                return "smallint";
            case TypeCode.UInt16:
                return "smallint";
            case TypeCode.Int32:
                return "int";
            case TypeCode.UInt32:
                return "int";
            case TypeCode.Int64:
                return "bigint";
            case TypeCode.UInt64:
                return "bigint";
            case TypeCode.Single:
                return "float";
            case TypeCode.Double:
                return "float";
            case TypeCode.String:
                return "text";
        }
        return type.Name;
    }
    public static int GetTypeLength(this Type type)
    {
        switch (Type.GetTypeCode(type))
        {
            case TypeCode.Boolean:
                return 1;
            case TypeCode.Byte:
                return 3;
            case TypeCode.Int16:
                return 5;
            case TypeCode.UInt16:
                return 5;
            case TypeCode.Int32:
                return 11;
            case TypeCode.UInt32:
                return 10;
            case TypeCode.Int64:
                return 21;
            case TypeCode.UInt64:
                return 20;
            case TypeCode.Single:
                return 12;
            case TypeCode.Double:
                return 10;
            case TypeCode.String:
                return 255;
        }
        return 0;
    }
}
