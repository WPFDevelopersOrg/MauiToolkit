namespace Maui.Toolkit.Utilities;


public static class DoubleUtilities
{
    public static bool AreClose(double value1, double value2)
    {
        if (value1 == value2)
        {
            return true;
        }
        double num = value1 - value2;
        return num < 1.53E-06 && num > -1.53E-06;
    }

    public static bool LessThan(double value1, double value2) => value1 < value2 && !DoubleUtilities.AreClose(value1, value2);

    public static bool GreaterThan(double value1, double value2) => value1 > value2 && !DoubleUtilities.AreClose(value1, value2);

    public static bool LessThanOrClose(double value1, double value2) => value1 < value2 || DoubleUtilities.AreClose(value1, value2);

    public static bool GreaterThanOrClose(double value1, double value2) => value1 > value2 || DoubleUtilities.AreClose(value1, value2);

    public static bool IsFinite(double value) => !double.IsNaN(value) && !double.IsInfinity(value);

    public static bool IsValidSize(double value) => DoubleUtilities.IsFinite(value) && DoubleUtilities.GreaterThanOrClose(value, 0.0);

    public const double Epsilon = 1.53E-06;
}
