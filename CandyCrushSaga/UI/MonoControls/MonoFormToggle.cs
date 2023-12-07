using CandyCrushSaga.Utilities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace CandyCrushSaga.UI.MonoControls
{
    [DefaultProperty(@"Toggled")]
    [DefaultEvent(@"ToggledChanged")]
    public class MonoToggle : Control
    {
        #region  Enums

        public enum ToggleType
        {
            CheckMark,
            OnOff,
            YesNo,
            IO
        }

        #endregion
        #region  Variables


        public event EventHandler ToggledChanged;

        private bool _toggled;
        private ToggleType _toggleType;
        private int _width;
        private int _height;
        private Color _fillColor;
        private Color _thumbColor2;
        private Color _foreColor2;

        #endregion
        #region  Properties


        public Color FillColor
        {
            get { return _fillColor; }
            set { _fillColor = value;
                Invalidate();
            }
        }

        public Color ThumbColor
        {
            get { return _thumbColor2; }
            set { _thumbColor2 = value;
                Invalidate();
            }
        }


        public Color ForeColor2
        {
            get { return _foreColor2; }
            set { _foreColor2 = value;
                Invalidate();
            }
        }


        public bool Toggled
        {
            get
            {
                return _toggled;
            }
            set
            {
                _toggled = value;
                Invalidate();
                OnToggleChanged();
            }
        }

        public ToggleType Type
        {
            get
            {
                return _toggleType;
            }
            set
            {
                _toggleType = value;
                Invalidate();
            }
        }

        #endregion
        #region  Events

        protected virtual void OnToggleChanged()
        {
            if (ToggledChanged != null)
                ToggledChanged(this, EventArgs.Empty);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new Size(76, 33);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Toggled = !Toggled;
            Focus();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var gfx = e.Graphics;

            gfx.SmoothingMode = SmoothingMode.HighQuality;
            _width = Width - 1;
            _height = Height - 1;

            var fullRect = new Rectangle(0, 0, _width, _height);

            gfx.PixelOffsetMode = (PixelOffsetMode)2;
            gfx.TextRenderingHint = (TextRenderingHint)5;

            var gpBase = Design.RoundRect(fullRect, 4);
            var gpInnerRect = 
                !_toggled ?
                Design.RoundRect(new Rectangle(4, 4, 36, _height - 8), 4) :
                Design.RoundRect(new Rectangle((_width / 2) - 2, 4, 36, _height - 8), 4);

            gfx.FillPath(new SolidBrush(_fillColor), gpBase);
            gfx.FillPath(new SolidBrush(_thumbColor2), gpInnerRect);


            // Draw string
            using (var foreColorBrush = new SolidBrush(ForeColor))
            {
                using (var foreColorBrush2 = new SolidBrush(ForeColor2))
                {
                    switch (_toggleType)
                    {
                        case ToggleType.CheckMark:
                            if (Toggled)
                                gfx.DrawString(
                                    @"ü",
                                    new Font(@"Wingdings", 18, FontStyle.Regular),
                                    foreColorBrush2,
                                    fullRect.X + 18,
                                    fullRect.Y + 19,
                                    new StringFormat
                                    {
                                        Alignment = StringAlignment.Center,
                                        LineAlignment = StringAlignment.Center
                                    });
                            else
                                gfx.DrawString(
                                    @"r",
                                    new Font(@"Marlett", 14, FontStyle.Regular),
                                    foreColorBrush,
                                    fullRect.X + 59,
                                    fullRect.Y + 18,
                                    new StringFormat
                                    {
                                        Alignment = StringAlignment.Center,
                                        LineAlignment = StringAlignment.Center
                                    });
                            break;


                        case ToggleType.OnOff:
                            if (Toggled)
                                gfx.DrawString(
                                    @"ON",
                                    new Font(@"Segoe UI", 12, FontStyle.Regular),
                                    foreColorBrush2,
                                    fullRect.X + 18,
                                    fullRect.Y + 16,
                                    new StringFormat
                                    {
                                        Alignment = StringAlignment.Center,
                                        LineAlignment = StringAlignment.Center
                                    });
                            else
                                gfx.DrawString(
                                    @"OFF",
                                    new Font(@"Segoe UI", 12, FontStyle.Regular),
                                    foreColorBrush,
                                    fullRect.X + 57,
                                    fullRect.Y + 16,
                                    new StringFormat
                                    {
                                        Alignment = StringAlignment.Center,
                                        LineAlignment = StringAlignment.Center
                                    });
                            break;


                        case ToggleType.YesNo:
                            if (Toggled)
                                gfx.DrawString(
                                    @"YES",
                                    new Font(@"Segoe UI", 12, FontStyle.Regular),
                                    foreColorBrush2,
                                    fullRect.X + 19,
                                    fullRect.Y + 16,
                                    new StringFormat
                                    {
                                        Alignment = StringAlignment.Center,
                                        LineAlignment = StringAlignment.Center
                                    });
                            else
                                gfx.DrawString(
                                    @"NO",
                                    new Font(@"Segoe UI", 12, FontStyle.Regular),
                                    foreColorBrush,
                                    fullRect.X + 56,
                                    fullRect.Y + 16,
                                    new StringFormat
                                    {
                                        Alignment = StringAlignment.Center,
                                        LineAlignment = StringAlignment.Center
                                    });
                            break;


                        case ToggleType.IO:
                            if (Toggled)
                                gfx.DrawString(
                                    @"I",
                                    new Font(@"Segoe UI", 12, FontStyle.Regular),
                                    foreColorBrush2,
                                    fullRect.X + 18,
                                    fullRect.Y + 16,
                                    new StringFormat
                                    {
                                        Alignment = StringAlignment.Center,
                                        LineAlignment = StringAlignment.Center
                                    });
                            else
                                gfx.DrawString(
                                    @"O",
                                    new Font(@"Segoe UI", 12, FontStyle.Regular),
                                    foreColorBrush,
                                    fullRect.X + 57,
                                    fullRect.Y + 16,
                                    new StringFormat
                                    {
                                        Alignment = StringAlignment.Center,
                                        LineAlignment = StringAlignment.Center
                                    });
                            break;
                    }
                }
            }
        }

        #endregion
        #region Constructors

        public MonoToggle()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            ForeColor = Color.Black;
            ForeColor2 = Color.DarkGray;
            FillColor = Color.Snow;
            ThumbColor = Color.LightGray;
        }

        #endregion
    }
}
