using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Platform;
using System.Diagnostics.CodeAnalysis;

namespace Maui.Toolkit.Platforms.Windows.Helpers;


public static class DpiHelper
{
    public static Point LogicalPixelsToDevice(this MauiWinUIWindow window, Point logicalPoint)
    {
        double scalingFactor = 1.0 / window.GetDisplayDensity();
        Matrix transformToDevice = Matrix.Identity;
        transformToDevice.Scale(scalingFactor, scalingFactor);
        return transformToDevice.Transform(logicalPoint);
    }

    public static Point DevicePixelsToLogical(this MauiWinUIWindow window, Point devicePoint)
    {
        double scalingFactor = window.GetDisplayDensity();
        Matrix transformToDip = Matrix.Identity;
        transformToDip.Scale(scalingFactor, scalingFactor);
        return transformToDip.Transform(devicePoint);
    }

    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
    public static Rect LogicalRectToDevice(this MauiWinUIWindow window, Rect logicalRectangle)
    {
        Point point = window.LogicalPixelsToDevice(new Point(logicalRectangle.Left, logicalRectangle.Top));
        Point point2 = window.LogicalPixelsToDevice(new Point(logicalRectangle.Right, logicalRectangle.Bottom));
        return new Rect(point, new Size(Math.Abs(point2.X - point.X), Math.Abs(point2.Y - point.Y)));
    }

    public static Rect DeviceRectToLogical(this MauiWinUIWindow window, Rect deviceRectangle)
    {
        Point point = window.DevicePixelsToLogical(new Point(deviceRectangle.Left, deviceRectangle.Top));
        Point point2 = window.DevicePixelsToLogical(new Point(deviceRectangle.Right, deviceRectangle.Bottom));
        return new Rect(point, new Size(Math.Abs(point2.X - point.X), Math.Abs(point2.Y - point.Y)));
    }

    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
    public static Size LogicalSizeToDevice(this MauiWinUIWindow window, Size logicalSize)
    {
        Point point = window.LogicalPixelsToDevice(new Point(logicalSize.Width, logicalSize.Height));
        return new Size(point.X, point.Y);
    }

    public static Size DeviceSizeToLogical(this MauiWinUIWindow window, Size deviceSize)
    {
        Point point = window.DevicePixelsToLogical(new Point(deviceSize.Width, deviceSize.Height));
        return new Size(point.X, point.Y);
    }

}
