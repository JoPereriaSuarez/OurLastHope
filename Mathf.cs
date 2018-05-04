using System;
using System.Security.Cryptography;

public static class Mathf
{
    public static float Clamp(float value, float min, float max)
    {
        return Math.Min(Math.Max(value, min), max);
    }
    public static int Clamp(int value, int min, int max)
    {
        return Math.Min(Math.Max(value, min), max);
    }

    public static int RandomSeed()
    {
        byte[] r_bytes = new byte[4];
        RandomNumberGenerator.Create().GetBytes(r_bytes);
        return (int)Math.Abs(BitConverter.ToInt32(r_bytes, 0));
    }
    public static bool RandomBool()
    {
        return Convert.ToBoolean(new Random(RandomSeed()).Next(0, 1));
    }
}
