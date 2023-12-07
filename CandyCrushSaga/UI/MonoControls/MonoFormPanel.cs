using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CandyCrushSaga.UI.MonoControls
{
    [DefaultProperty(@"RoundCorners")]
    [DefaultEvent(@"Load")]
    public sealed class MonoPanel : ContainerControl
    {
        #region  Variables

        private GraphicsPath _shapeGp;
        private Color _fillColor = Color.FromArgb(39, 51, 63);
        private bool _roundCorners = true;

        #endregion
        #region  Properties

        public Color FillColor
        {
            get { return _fillColor; }
            set { 
                _fillColor = value;
                Invalidate();
            }
        }

        public bool RoundCorners
        {
            get { return _roundCorners; }
            set {
                _roundCorners = value;
                RefreshGraphicPath();
                Invalidate();
            }
        }


        #endregion
        #region  Events

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RefreshGraphicPath();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var img = new Bitmap(Width, Height))
            {
                using (var gfx = Graphics.FromImage(img))
                {
                    gfx.SmoothingMode = SmoothingMode.HighQuality;

                    //fill background
                    gfx.FillPath(new SolidBrush(FillColor), _shapeGp);

                    //draw border
                    gfx.DrawPath(new Pen(FillColor), _shapeGp);

                    e.Graphics.DrawImage((Image)(img.Clone()), 0, 0);
                }
            }
        }

        #endregion
        #region Methods
        
        private void RefreshGraphicPath()
        {
            _shapeGp = new GraphicsPath();

            if (_roundCorners)
            {
                _shapeGp.AddArc(0, 0, 10, 10, 180, 90);
                _shapeGp.AddArc(Width - 11, 0, 10, 10, -90, 90);
                _shapeGp.AddArc(Width - 11, Height - 11, 10, 10, 0, 90);
                _shapeGp.AddArc(0, Height - 11, 10, 10, 90, 90);
            }
            else
                _shapeGp.AddRectangle(new RectangleF(0, 0, Width, Height));

            _shapeGp.CloseAllFigures();
        }

        #endregion
        #region Constructors

        public MonoPanel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);

            DoubleBuffered = true;
            RoundCorners = true;
            BackColor = Color.Transparent;
            Size = new Size(187, 117);
            Padding = new Padding(5, 5, 5, 5);
        }

        #endregion
    }
}
