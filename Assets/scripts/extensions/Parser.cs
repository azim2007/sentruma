using System.Globalization;

public static class Parser
{
    public static float FloatParse(string value)
    {
        return float.Parse(value, new NumberFormatInfo() { NumberDecimalSeparator = "," });
    }
}
