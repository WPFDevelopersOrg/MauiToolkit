using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.DesignControl.Graphics;
public class GraphicsDrawable : IDrawable
{
    void IDrawable.Draw(ICanvas canvas, RectF dirtyRect)
    {
        IPattern? pattern = default;
        IBlurrableCanvas blurrableCanvas = new ScalingCanvas(canvas);
        blurrableCanvas.SetBlur(10);

        using var picture = new PictureCanvas(0, 0, 10, 10)
        {
            StrokeColor = Colors.Silver,
        };
        picture.DrawLine(0, 0, 10, 10);
        picture.DrawLine(0, 10, 10, 0);
        pattern = new PicturePattern(picture.Picture, 10, 10);

        var patternPaint = new PatternPaint
        {
            Pattern = pattern,
        };

        canvas.SetFillPaint(patternPaint, RectF.Zero);
        canvas.FillRectangle(10, 10, 250, 250);
    }
}
