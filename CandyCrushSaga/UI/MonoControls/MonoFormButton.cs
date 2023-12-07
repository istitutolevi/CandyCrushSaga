using CandyCrushSaga.Utilities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CandyCrushSaga.UI.MonoControls
{
    [DefaultProperty(@"Text")]
    [DefaultEvent(@"Click")]
    public sealed class MonoButton : Control
    {
        #region  Variables

        private int _mouseState;
        private bool _roundCorners;
        private GraphicsPath _shape;
        private LinearGradientBrush _inactiveGbrush;
        private LinearGradientBrush _pressedGbrush;
        private Pen _inactiveBorderPen;
        private Pen _pressedBorderPen;
        private Image _image;
        private Size _imageSize = default(Size);
        private StringAlignment _textAlignment = StringAlignment.Center;
        private Color _fillColor = Color.FromArgb(181, 41, 42);
        private Color _fillColor2 = Color.FromArgb(165, 37, 37);
        private ContentAlignment _imageAlign = ContentAlignment.MiddleLeft;

        #endregion
        #region  Properties

        public bool RoundCorners
        {
            get { return _roundCorners; }
            set {
                _roundCorners = value;
                Invalidate();
            }
        }

        public Color FillColor
        {
            get { return _fillColor; }
            set
            {
                _fillColor = value;
                UpdatePens();
                Invalidate();
            }
        }
        public Color FillColor2
        {
            get { return _fillColor2; }
            set {
                _fillColor2 = value;
                UpdatePens();
                Invalidate();
            }
        }
        public ContentAlignment ImageAlign
        {
            get
            {
                return _imageAlign;
            }
            set
            {
                _imageAlign = value;
                Invalidate();
            }
        }
        public Image Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                Invalidate();
            }
        }
        public Size ImageSize
        {
            get
            {
                return _imageSize;
            }
            set { _imageSize = value; Invalidate(); }
        }
        public StringAlignment TextAlignment
        {
            get
            {
                return _textAlignment;
            }
            set
            {
                _textAlignment = value;
                Invalidate();
            }
        }
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                Invalidate();
            }
        }

        #endregion
        #region  Events

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _mouseState = 0;
            Invalidate();
            base.OnMouseUp(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _mouseState = 1;
            Focus();
            Invalidate();
            base.OnMouseDown(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            _mouseState = 0;
            Invalidate();
            base.OnMouseLeave(e);
        }
        protected override void OnTextChanged(EventArgs e)
        {
            Invalidate();
            base.OnTextChanged(e);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (Width <= 0 || Height <= 0) return;

            Invalidate();
        }
        protected sealed override void OnPaint(PaintEventArgs e)
        {
            _shape = new GraphicsPath();

            UpdatePens();

            if (_roundCorners)
            {
                _shape.AddArc(0, 0, 10, 10, 180, 90);
                _shape.AddArc(Width - 11, 0, 10, 10, -90, 90);
                _shape.AddArc(Width - 11, Height - 11, 10, 10, 0, 90);
                _shape.AddArc(0, Height - 11, 10, 10, 90, 90);
            }
            else
            {
                _shape.AddRectangle(new RectangleF(0, 0, Width, Height));
            }
            _shape.CloseAllFigures();

            var gfx = e.Graphics;

            gfx.SmoothingMode = SmoothingMode.HighQuality;
            var imgpt = Design.ImageLocation(Design.GetStringFormat(ImageAlign), Size, ImageSize);
            
            switch (_mouseState)
            {
                case 0:
                    //Inactive
                    gfx.FillPath(_inactiveGbrush, _shape);
                    gfx.DrawPath(_inactiveBorderPen, _shape);
                    break;
                case 1:
                    //Pressed
                    gfx.FillPath(_pressedGbrush, _shape);
                    gfx.DrawPath(_pressedBorderPen, _shape);
                    break;
            }

            if (Image != null)
            {
                gfx.DrawImage(_image, imgpt.X, imgpt.Y, ImageSize.Width, ImageSize.Height);
                var rightaligned = ImageAlign == ContentAlignment.TopRight | ImageAlign == ContentAlignment.MiddleRight |
                                    ImageAlign == ContentAlignment.BottomRight;
                var centeraligned = ImageAlign == ContentAlignment.TopCenter | ImageAlign == ContentAlignment.MiddleCenter |
                                    ImageAlign == ContentAlignment.BottomCenter;
                gfx.DrawString(Text, Font, new SolidBrush(ForeColor), new RectangleF(
                    (rightaligned | centeraligned) ? 0 : (imgpt.X + ImageSize.Width), 0, (centeraligned) ? Width : (Width - ImageSize.Width), Height),
                    new StringFormat
                    {
                        Alignment = _textAlignment,
                        LineAlignment = StringAlignment.Center
                    });
            }
            else
            {
                gfx.DrawString(Text, Font, new SolidBrush(ForeColor), new RectangleF(0, 0, Width, Height),
                    new StringFormat
                    {
                        Alignment = _textAlignment,
                        LineAlignment = StringAlignment.Center
                    });
            }
        }

        #endregion
        #region Methods

        private void UpdatePens()
        {
            _inactiveBorderPen = new Pen(FillColor);
            _pressedBorderPen = new Pen(FillColor2);

            if (Width > 0 && Height > 0)
            {
                _inactiveGbrush = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), FillColor, FillColor, 90.0F);
                _pressedGbrush = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), FillColor2, FillColor2, 90.0F);
            }
        }

        #endregion
        #region Constructors

        public MonoButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);

            UpdatePens();

            BackColor = Color.Transparent;
            DoubleBuffered = true;
            Font = new Font(@"Segoe UI", 12);
            ForeColor = Color.FromArgb(255, 255, 255);
            TextAlignment = StringAlignment.Center;
            Size = new Size(172, 50);
            ImageSize = new Size(46, 46);
            RoundCorners = true;
        }

        #endregion
    }
}