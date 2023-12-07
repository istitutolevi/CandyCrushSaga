using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CandyCrushSaga.UI.MonoControls
{
    [DefaultProperty(@"Text")]
    [DefaultEvent(@"TextChanged")]
    public sealed class MonoTextBox : Control
    {
        #region  Variables

        private bool _gotfocus;
        private bool _isPasswordMasked;
        private bool _multiline;
        private bool _readOnly;
        private Color _highlightColor = Color.FromArgb(181, 41, 42);
        private Color _fillColor = Color.FromArgb(66, 76, 85);
        private GraphicsPath _shapeGp;
        private HorizontalAlignment _txtAlign;
        private Image _image;
        private int _maxLenght = 50000;
        private int _selectionStart;
        private Pen _highlightPen;
        private SolidBrush _fillBrush;

        public TextBox Innertextbox = new TextBox();

        #endregion
        #region  Properties

        public Size ImageSize { get; private set; }
        public bool Multiline
        {
            get { return _multiline; }
            set
            {
                _multiline = value;
                if (Innertextbox == null) return;
                Innertextbox.Multiline = value;

                if (value)
                    Innertextbox.Height = Height - 23;
                else
                    Height = Innertextbox.Height + 23;
            }
        }
        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                _readOnly = value;
                if (Innertextbox != null)
                    Innertextbox.ReadOnly = value;
            }
        }
        public bool UseSystemPasswordChar
        {
            get { return _isPasswordMasked; }
            set
            {
                _isPasswordMasked = value;
                if (Innertextbox != null)
                    Innertextbox.UseSystemPasswordChar = value;

                Invalidate();
            }
        }
        public Color FillColor
        {
            get { return _fillColor; }
            set
            {
                _fillColor = value;
                UpdatePenAndBrush();
                if (Innertextbox != null)
                    Innertextbox.BackColor = value;
                Invalidate();
            }
        }
        public Color HighlightColor
        {
            get { return _highlightColor; }
            set
            {
                _highlightColor = value;
                UpdatePenAndBrush();
                Invalidate();
            }
        }
        public HorizontalAlignment TextAlignment
        {
            get { return _txtAlign; }
            set { _txtAlign = value; Invalidate(); }
        }
        public Image Image
        {
            get {  return _image; }
            set
            {
                ImageSize = value != null ? value.Size : Size.Empty;

                _image = value;

                if (Innertextbox != null)
                {
                    Innertextbox.Location = _image == null ? new Point(8, 10) : new Point(35, 11);
                }

                Invalidate();
            }
        }
        public int MaxLength
        {
            get { return _maxLenght; }
            set
            {
                _maxLenght = value;
                if (Innertextbox != null)
                    Innertextbox.MaxLength = value;

                Invalidate();
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public int SelectionStart
        {
            get { return _selectionStart; }
            set
            {
                _selectionStart = value;
                if (Innertextbox != null)
                    Innertextbox.SelectionStart = value;

                Invalidate();
            }
        }

        #endregion
        #region  Events

        protected override void OnTextChanged(EventArgs e)
        {
            if (Innertextbox != null)
                Innertextbox.Text = Text;
            Invalidate();
            base.OnTextChanged(e);
            base.OnGotFocus(e);
        }
        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            Innertextbox.ForeColor = ForeColor;
            Invalidate();
        }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            Innertextbox.Font = Font;
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (Innertextbox != null)
            {
                if (_multiline)
                {
                    Innertextbox.Height = Height - 23;
                }
                else
                {
                    Height = Innertextbox.Height + 23;
                }
            }

            _shapeGp = new GraphicsPath();
            _shapeGp.AddArc(0, 0, 10, 10, 180, 90);
            _shapeGp.AddArc(Width - 11, 0, 10, 10, -90, 90);
            _shapeGp.AddArc(Width - 11, Height - 11, 10, 10, 0, 90);
            _shapeGp.AddArc(0, Height - 11, 10, 10, 90, 90);
            _shapeGp.CloseAllFigures();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            if (Innertextbox != null)
            {
                Innertextbox.BackColor = FillColor;
                Innertextbox.Invalidate();
            }
            base.OnSizeChanged(e);
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Innertextbox.Focus();
            Invalidate();
        }
        protected override void OnGotFocus(EventArgs e)
        {
            _gotfocus = true;
            base.OnGotFocus(e);
            Innertextbox.Focus();
            Invalidate();
        }
        protected override void OnLostFocus(EventArgs e)
        {
            _gotfocus = false;
            base.OnLostFocus(e);
            Invalidate();
        }
        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);
            Innertextbox.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Innertextbox != null)
                Innertextbox.BackColor = FillColor;
            base.OnPaint(e);

            var gfx = e.Graphics;
            gfx.SmoothingMode = SmoothingMode.HighQuality;
            gfx.CompositingQuality = CompositingQuality.HighQuality;

            using (var tmpimg = new Bitmap(Width, Height))
            {
                using (var imgGfx = Graphics.FromImage(tmpimg))
                {
                    imgGfx.SmoothingMode = SmoothingMode.HighQuality;
                    imgGfx.CompositingQuality = CompositingQuality.HighQuality;

                    if (Innertextbox != null)
                    {
                        if (Image == null)
                            Innertextbox.Width = Width - 18;
                        else
                            Innertextbox.Width = Width - 45;

                        Innertextbox.TextAlign = TextAlignment;
                        Innertextbox.UseSystemPasswordChar = UseSystemPasswordChar;
                    }

                    imgGfx.Clear(Color.Transparent);

                    imgGfx.FillPath(_fillBrush, _shapeGp);
                    if(_gotfocus)
                        imgGfx.DrawPath(_highlightPen, _shapeGp);

                    if (Image != null)
                        imgGfx.DrawImage(_image, 5, 8, 24, 24);

                    gfx.DrawImage((Image)(tmpimg.Clone()), 0, 0);
                }
            }
        }


        private void _DoOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                Innertextbox.SelectAll();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                Innertextbox.Copy();
                e.SuppressKeyPress = true;
            }
            OnKeyDown(e);
            Invalidate();
        }
        private void _DoOnKeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
            Invalidate();
        }
        private void _DoOnKeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);
            Invalidate();
        }
        private void _DoOnTextChanged(object sender, EventArgs e)
        {
            Text = Innertextbox.Text;
            OnTextChanged(e);
        }
        private void _DoOnEnter(object sender, EventArgs e)
        {
            _highlightPen = new Pen(HighlightColor);
            OnEnter(e);
            Refresh();
        }
        private void _DoOnLeave(object sender, EventArgs e)
        {
            OnLeave(e);
            Invalidate();
        }
        private void _DoOnLostFocus(object sender, EventArgs e)
        {
            OnLostFocus(e);
        }
        private void _DoOnGotFocus(object sender, EventArgs e)
        {
            OnGotFocus(e);
        }

        #endregion
        #region Custom Methods

        private void UpdatePenAndBrush()
        {
            _highlightPen = new Pen(HighlightColor);
            _fillBrush = new SolidBrush(FillColor);
        }
        private void InitializeTextBox()
        {
            Innertextbox.Location = new Point(8, 10);
            Innertextbox.Text = Text;
            Innertextbox.BorderStyle = BorderStyle.None;
            Innertextbox.TextAlign = HorizontalAlignment.Left;
            Innertextbox.Font = Font;
            Innertextbox.UseSystemPasswordChar = UseSystemPasswordChar;
            Innertextbox.Multiline = Multiline;
            Innertextbox.BackColor = FillColor;
            Innertextbox.ScrollBars = ScrollBars.None;
            Innertextbox.KeyDown += _DoOnKeyDown;
            Innertextbox.KeyUp += _DoOnKeyUp;
            Innertextbox.KeyPress += _DoOnKeyPress;
            Innertextbox.Enter += _DoOnEnter;
            Innertextbox.Leave += _DoOnLeave;
            Innertextbox.TextChanged += _DoOnTextChanged;
            Innertextbox.GotFocus += _DoOnGotFocus;
            Innertextbox.LostFocus += _DoOnLostFocus;
            Controls.Add(Innertextbox);
        }

        #endregion
        #region Constructors

        public MonoTextBox()
        {
            UpdatePenAndBrush();

            DoubleBuffered = true;
            UseSystemPasswordChar = false;
            Multiline = false;
            ReadOnly = false;

            InitializeTextBox();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);

            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(176, 183, 191);
            Text = string.Empty;
            Font = new Font(@"Tahoma", 11);
            Size = new Size(213, 41);
        }

        #endregion
    }
}
