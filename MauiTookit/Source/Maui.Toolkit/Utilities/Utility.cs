using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Maui.Toolkit.Utilities;

public static class Utility
{
    public static bool GuidTryParse(string guidString, out Guid guid)
    {
        guid = default;
        if (string.IsNullOrWhiteSpace(guidString))
            return false;

        try
        {
            guid = new Guid(guidString);
            return true;
        }
        catch (FormatException)
        {
        }
        catch (OverflowException)
        {
        }

        return false;
    }

    public static bool MemCompare(IntPtr left, IntPtr right, long size)
    {
        int num = 0;
        while ((long)num < size - 8L)
        {
            long num2 = Marshal.ReadInt64(left, num);
            long num3 = Marshal.ReadInt64(right, num);
            if (num2 != num3)
                return false;

            num += 8;
        }
        while ((long)num < size)
        {
            byte b = Marshal.ReadByte(left, num);
            byte b2 = Marshal.ReadByte(right, num);
            if (b != b2)
                return false;

            num++;
        }
        return true;
    }

    public static void CopyStream(Stream destination, Stream source)
    {
        destination.Position = 0L;
        if (source.CanSeek)
        {
            source.Position = 0L;
            destination.SetLength(source.Length);
        }
        byte[] array = new byte[4096];
        int num;
        do
        {
            num = source.Read(array, 0, array.Length);
            if (num != 0)
                destination.Write(array, 0, num);
        }
        while (array.Length == num);
        destination.Position = 0L;
    }

    public static bool AreStreamsEqual(Stream left, Stream right)
    {
        if (left == null)
            return right == null;

        if (right == null)
            return false;

        if (!left.CanRead || !right.CanRead)
            throw new NotSupportedException("The streams can't be read for comparison");

        if (left.Length != right.Length)
            return false;

        int num = (int)left.Length;
        left.Position = 0L;
        right.Position = 0L;
        int i = 0;
        int num2 = 0;
        byte[] array = new byte[512];
        byte[] array2 = new byte[512];
        GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
        IntPtr left2 = gchandle.AddrOfPinnedObject();
        GCHandle gchandle2 = GCHandle.Alloc(array2, GCHandleType.Pinned);
        IntPtr right2 = gchandle2.AddrOfPinnedObject();
        bool result;
        try
        {
            while (i < num)
            {
                int num3 = left.Read(array, 0, array.Length);
                int num4 = right.Read(array2, 0, array2.Length);
                if (num3 != num4)
                    return false;

                if (!MemCompare(left2, right2, (long)num3))
                    return false;

                i += num3;
                num2 += num4;
            }
            result = true;
        }
        finally
        {
            gchandle.Free();
            gchandle2.Free();
        }
        return result;
    }

    public static bool IsFlagSet(int value, int mask) => 0 != (value & mask);

    public static bool IsFlagSet(uint value, uint mask) => 0U != (value & mask);

    public static bool IsFlagSet(long value, long mask) => 0L != (value & mask);

    public static bool IsFlagSet(ulong value, ulong mask) => 0UL != (value & mask);

    public static void FreeCoTaskMem(ref IntPtr ptr)
    {
        IntPtr intPtr = ptr;
        ptr = IntPtr.Zero;
        if (IntPtr.Zero != intPtr)
            Marshal.FreeCoTaskMem(intPtr);
    }

    public static string GenerateToString<T>(T @object) where T : struct
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (PropertyInfo propertyInfo in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (stringBuilder.Length != 0)
                stringBuilder.Append(", ");

            object? value = propertyInfo.GetValue(@object, null);

            string format = (value == null) ? "{0}: <null>" : "{0}: \"{1}\"";
            stringBuilder.AppendFormat(format, propertyInfo.Name, value);
        }
        return stringBuilder.ToString();
    }

    public static string? UrlDecode(string url)
    {
        if (url == null)
            return null;

        _UrlDecoder urlDecoder = new _UrlDecoder(url.Length, Encoding.UTF8);
        int length = url.Length;
        for (int i = 0; i < length; i++)
        {
            char c = url[i];
            if (c == '+')
                urlDecoder.AddByte(32);
            else
            {
                if (c == '%' && i < length - 2)
                {
                    if (url[i + 1] == 'u' && i < length - 5)
                    {
                        int num = HexToInt(url[i + 2]);
                        int num2 = HexToInt(url[i + 3]);
                        int num3 = HexToInt(url[i + 4]);
                        int num4 = HexToInt(url[i + 5]);
                        if (num >= 0 && num2 >= 0 && num3 >= 0 && num4 >= 0)
                        {
                            urlDecoder.AddChar((char)(num << 12 | num2 << 8 | num3 << 4 | num4));
                            i += 5;
                            goto IL_12D;
                        }
                    }
                    else
                    {
                        int num5 = HexToInt(url[i + 1]);
                        int num6 = HexToInt(url[i + 2]);
                        if (num5 >= 0 && num6 >= 0)
                        {
                            urlDecoder.AddByte((byte)(num5 << 4 | num6));
                            i += 2;
                            goto IL_12D;
                        }
                    }
                }

                if ((c & 'ﾀ') == '\0')
                    urlDecoder.AddByte((byte)c);
                else
                    urlDecoder.AddChar(c);
            }
        IL_12D:;
        }

        return urlDecoder.GetString();
    }

    public static string? UrlEncode(string url)
    {
        if (url == null)
            return null;

        byte[] array = Encoding.UTF8.GetBytes(url);
        bool flag = false;
        int num = 0;
        foreach (byte b in array)
        {
            if (b == 32)
                flag = true;
            else if (!UrlEncodeIsSafe(b))
            {
                num++;
                flag = true;
            }
        }
        if (flag)
        {
            byte[] array3 = new byte[array.Length + num * 2];
            int num2 = 0;
            foreach (byte b2 in array)
            {
                if (UrlEncodeIsSafe(b2))
                    array3[num2++] = b2;
                else if (b2 == 32)
                    array3[num2++] = 43;
                else
                {
                    array3[num2++] = 37;
                    array3[num2++] = IntToHex(b2 >> 4 & 15);
                    array3[num2++] = IntToHex((int)(b2 & 15));
                }
            }
            array = array3;
        }
        return Encoding.ASCII.GetString(array);
    }

    public static bool UrlEncodeIsSafe(byte b)
    {
        if (IsAsciiAlphaNumeric(b))
            return true;

        if (b != 33)
        {
            switch (b)
            {
                case 39:
                case 40:
                case 41:
                case 42:
                case 45:
                case 46:
                    return true;
                case 43:
                case 44:
                    break;
                default:
                    if (b == 95)
                        return true;
                    break;
            }
            return false;
        }
        return true;
    }

    private static bool IsAsciiAlphaNumeric(byte b) => (b >= 97 && b <= 122) || (b >= 65 && b <= 90) || (b >= 48 && b <= 57);

    public static byte IntToHex(int n)
    {
        if (n <= 9)
            return (byte)(n + 48);

        return (byte)(n - 10 + 65);
    }

    public static int HexToInt(char h)
    {
        if (h >= '0' && h <= '9')
            return (int)(h - '0');

        if (h >= 'a' && h <= 'f')
            return (int)(h - 'a' + '\n');

        if (h >= 'A' && h <= 'F')
            return (int)(h - 'A' + '\n');

        return -1;
    }

    private class _UrlDecoder
    {
        public _UrlDecoder(int size, Encoding encoding)
        {
            _encoding = encoding;
            _charBuffer = new char[size];
            _byteBuffer = new byte[size];
        }

        public void AddByte(byte b)
        {
            _byteBuffer[_byteCount++] = b;
        }

        public void AddChar(char ch)
        {
            _FlushBytes();
            _charBuffer[_charCount++] = ch;
        }

        private void _FlushBytes()
        {
            if (_byteCount > 0)
            {
                _charCount += _encoding.GetChars(_byteBuffer, 0, _byteCount, _charBuffer, _charCount);
                _byteCount = 0;
            }
        }

        public string GetString()
        {
            _FlushBytes();
            if (_charCount > 0)
                return new string(_charBuffer, 0, _charCount);

            return "";
        }

        private readonly Encoding _encoding;

        private readonly char[] _charBuffer;

        private readonly byte[] _byteBuffer;

        private int _byteCount;

        private int _charCount;
    }

}
