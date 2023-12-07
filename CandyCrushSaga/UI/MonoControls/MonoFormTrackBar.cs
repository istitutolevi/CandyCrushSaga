using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CandyCrushSaga.UI.MonoControls
{
    [DefaultProperty(@"Value")]
    [DefaultEvent(@"ValueChanged")]
    public class MonoTrackBar : Control
    {
        #region  Variables

        private Orientation _orientation = Orientation.Horizontal;
        private double _value = 100f;
        private double _tick = 1f;
        private Color _fillColor = Color.Gray;
        private Color _fillColor2 = Color.Crimson;
        private bool _mouseDown;
        private Point _lastPoint = default(Point);

        #endregion
        #region  Properties

        public double Tick
        {
            get { return _tick; }
            set { _tick = value; }
        }
        public double Value
        {
            get { return _value; }
            set
            {
                var oldval = _value;
                if (value < 0f)
                    _value = 0f;
                else if (value > 100f)
                    _value = 100f;
                else
                    _value = value;

                if (value != oldval)
                    OnValueChanged(EventArgs.Empty);
                Invalidate();
            }
        }
        public Orientation Orientation
        {
            get { return _orientation; }
            set
            {
                if (_orientation != value)
                {
                    _orientation = value;
                    Invalidate();
                }
                else
                    _orientation = value;
            }
        }
        public Color FillColor { get { return _fillColor; } set { _fillColor = value; Invalidate(); } }
        public Color FillColor2 { get { return _fillColor2; } set { _fillColor2 = value; Invalidate(); } }
        
        public event EventHandler Scroll;
        public event EventHandler ValueChanged;

        #endregion
        #region  Events

        protected virtual void OnScroll(EventArgs e)
        {
            if (Scroll != null)
                Scroll.Invoke(this, e);
        }
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged.Invoke(this, e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left) return;

            _mouseDown = true;
            switch (_orientation)
            {
                case Orientation.Horizontal:
                    UpdatePercentScale(e.Location.X);
                    break;
                case Orientation.Vertical:
                    UpdatePercentScale(e.Location.Y);
                    break;
            }
            _lastPoint = e.Location;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button != MouseButtons.Left) return;

            _mouseDown = false;
            switch (_orientation)
            {
                case Orientation.Horizontal:
                    UpdatePercentScale(e.Location.X);
                    break;
                case Orientation.Vertical:
                    UpdatePercentScale(e.Location.Y);
                    break;
            }
            _lastPoint = e.Location;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!_mouseDown) return;

            switch (_orientation)
            {
                case Orientation.Horizontal:
                    UpdatePercentScale(e.Location.X);
                    break;
                case Orientation.Vertical:
                    UpdatePercentScale(e.Location.Y);
                    break;
            }
            _lastPoint = e.Location;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var gfx = e.Graphics;
            gfx.Clear(BackColor);
            gfx.SmoothingMode = SmoothingMode.HighQuality;

            switch (_orientation)
            {
                case Orientation.Horizontal:

                    using (var brush = new SolidBrush(_fillColor))
                    {
                        float w = Width;
                        var h = Utilities.Math.GetPercentage(40f, Height);
                        var x = 0f;
                        var y = (Height / 2f) - (h / 2f);
                        var rect = new RectangleF(x, y, w, h);
                        rect.Inflate(-2, -2);
                        //Draw Center Default Line
                        gfx.FillRectangle(brush, rect);
                    }

                    using (var brush = new SolidBrush(_fillColor2))
                    {
                        var w = Utilities.Math.GetPercentage(_value, Width);
                        var h = Utilities.Math.GetPercentage(40f, Height);
                        var x = 0f;
                        var y = (Height / 2f) - (h / 2f);
                        var rect = new RectangleF(x, y, (float)w, h);
                        rect.Inflate(-2, -2);
                        //Draw Current Selected Scale Line
                        gfx.FillRectangle(brush, rect);


                        x = (float)((x + w) - (Height / 2f));

                        if (x < 0f)
                            x = 0f;
                        else if (x > Width - Height)
                            x = Width - Height + 2f;

                        var cRect = new RectangleF(x, 0f, Height, Height);
                        cRect.Inflate(-1, -1);
                        //Draw Current Selected Scale Circle
                        gfx.FillEllipse(brush, cRect);
                    }
                    break;










                case Orientation.Vertical:

                    using (var brush = new SolidBrush(_fillColor))
                    {
                        float w = Utilities.Math.GetPercentage(40f, Width);
                        var h = Height;

                        var x = (Width / 2f) - (w / 2f);
                        var y = 0f;

                        var rect = new RectangleF(x, y, w, h);
                        rect.Inflate(-2, -2);
                        //Draw Center Default Line
                        gfx.FillRectangle(brush, rect);
                    }

                    using (var brush = new SolidBrush(_fillColor2))
                    {
                        var w = Utilities.Math.GetPercentage(40f, Width);
                        var h = Utilities.Math.GetPercentage(100f - _value, Height);

                        var x = (Width / 2f) - (w / 2f);
                        var y = 0f;
                        y = (float)((y + h) - (Width / 2f));

                        if (y < 0f)
                            y = 0f;
                        else if (y > Height - Width)
                            y = Height - Width + 2f;

                        var cRect = new RectangleF(0f, y, Width, Width);
                        cRect.Inflate(-1, -1);
                        //Draw Current Selected Scale Circle
                        gfx.FillEllipse(brush, cRect);

                        var rect = new RectangleF(x, y, w, Height - y);
                        rect.Inflate(-2, -2);
                        //Draw Current Selected Scale Line
                        gfx.FillRectangle(brush, rect);
                    }
                    break;
            }
        }

        #endregion
        #region  Methods

        private void UpdatePercentScale(double p)
        {
            switch (_orientation)
            {
                case Orientation.Horizontal:
                    if (p >= 0f & p <= Width)
                        Value = Utilities.Math.GetPercent(p, Width);

                    if (Math.Abs(_lastPoint.X - p) > _tick)
                        OnScroll(EventArgs.Empty);
                    break;
                case Orientation.Vertical:
                    if (p >= 0f & p <= Height)
                        Value = Utilities.Math.GetPercent(Height - p, Height);

                    if (Math.Abs(_lastPoint.Y - p) > _tick)
                        OnScroll(EventArgs.Empty);
                    break;
            }
        }

        #endregion
        #region Constructors

        public MonoTrackBar()
        {
            SetStyle((ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw), true);

            FillColor = Color.FromArgb(116, 125, 132);
            FillColor2 = Color.FromArgb(69, 91, 113);
            Value = 50F;
        }

        #endregion
    }
}
