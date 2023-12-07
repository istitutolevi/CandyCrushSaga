using System.Drawing;
using System.Drawing.Drawing2D;

namespace CandyCrushSaga.Utilities
{
    public sealed class Design
    {
        public static GraphicsPath RoundRect(Rectangle rectangle, int curveRadius)
        {
            var p = new GraphicsPath();

            if (curveRadius <= 0)
            {
                p.AddRectangle(rectangle);
                return p;
            }

            var arcRectangleWidth = curveRadius * 2;
            p.AddArc(new Rectangle(rectangle.X, rectangle.Y, arcRectangleWidth, arcRectangleWidth), -180, 90);
            p.AddArc(new Rectangle(rectangle.Width - arcRectangleWidth + rectangle.X, rectangle.Y, arcRectangleWidth, arcRectangleWidth), -90, 90);
            p.AddArc(new Rectangle(rectangle.Width - arcRectangleWidth + rectangle.X, rectangle.Height - arcRectangleWidth + rectangle.Y, arcRectangleWidth, arcRectangleWidth), 0, 90);
            p.AddArc(new Rectangle(rectangle.X, rectangle.Height - arcRectangleWidth + rectangle.Y, arcRectangleWidth, arcRectangleWidth), 90, 90);
            p.AddLine(new Point(rectangle.X, rectangle.Height - arcRectangleWidth + rectangle.Y), new Point(rectangle.X, curveRadius + rectangle.Y));
            return p;
        }
        public static GraphicsPath RoundTopRect(Rectangle rectangle, int curveRadius)
        {
            var p = new GraphicsPath();

            if (curveRadius <= 0)
            {
                p.AddRectangle(rectangle);
                return p;
            }

            var arcRectangleWidth = curveRadius * 2;

            p.AddArc(
                new Rectangle(
                    rectangle.X, 
                    rectangle.Y, 
                    arcRectangleWidth, 
                    arcRectangleWidth
                    ), 
                    -180, 90);
            p.AddArc(
                new Rectangle(
                    rectangle.Width - arcRectangleWidth + rectangle.X, 
                    rectangle.Y, 
                    arcRectangleWidth, 
                    arcRectangleWidth
                    ), 
                    -90, 90);

            p.AddRectangle(
                new Rectangle(
                    rectangle.X,
                    rectangle.Y + curveRadius,
                    rectangle.Width,
                    rectangle.Height - curveRadius)
                    );
            p.CloseAllFigures();
            return p;
        }
        public static GraphicsPath RoundBottomRect(Rectangle rectangle, int curveRadius)
        {
            var p = new GraphicsPath();

            if (curveRadius <= 0)
            {
                p.AddRectangle(rectangle);
                return p;
            }

            var arcRectangleWidth = curveRadius * 2;


            p.AddRectangle(
                new Rectangle(
                    rectangle.X,
                    rectangle.Y,
                    rectangle.Width,
                    rectangle.Height - curveRadius)
                    ); 
            p.AddArc(new Rectangle(rectangle.X + rectangle.Width - arcRectangleWidth, rectangle.Y + rectangle.Height - arcRectangleWidth, arcRectangleWidth, arcRectangleWidth), 0, 90);
            p.AddArc(new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - arcRectangleWidth, arcRectangleWidth, arcRectangleWidth), 90, 90);            
            p.CloseAllFigures();
            return p;
        }

        public static PointF ImageLocation(StringFormat stringFormat, SizeF areaSizeF, SizeF imageAreaSizeF)
        {
            var p = new PointF();
            switch (stringFormat.Alignment)
            {
                case StringAlignment.Center:
                    p.X = (areaSizeF.Width - imageAreaSizeF.Width) / 2;
                    p.Y = (areaSizeF.Height - imageAreaSizeF.Height) / 2;
                    break;
                case StringAlignment.Near:
                    p.X = 0;
                    p.Y = (areaSizeF.Height - imageAreaSizeF.Height) / 2;
                    break;
                case StringAlignment.Far:
                    p.X = areaSizeF.Width - imageAreaSizeF.Width;
                    p.Y = areaSizeF.Height - imageAreaSizeF.Height;
                    break;

            }
            return p;
        }
        public static StringFormat GetStringFormat(ContentAlignment contentAlignment)
        {
            var sf = new StringFormat();
            switch (contentAlignment)
            {
                case ContentAlignment.MiddleCenter:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleLeft:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleRight:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.TopCenter:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopLeft:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomCenter:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.BottomRight:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Far;
                    break;
            }
            return sf;
        }
    }
}
