using CandyCrushSaga.Utilities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CandyCrushSaga.UI.MonoControls
{
    [DefaultProperty(@"RoundedCorners")]
    [DefaultEvent(@"Load")]
    public sealed class MonoTheme : ContainerControl
    {
        #region  Variables

        private bool _cap;
        private bool _controlMode;
        private bool _hasShown;
        private bool _roundCorners = true;
        private Color _headerBackColor = Color.FromArgb(181, 41, 42);
        private int _headerHeight;
        private int _borderRadius;
        private Point _mouseP = new Point(0, 0);
        private Rectangle _headerRect;

        #endregion
        #region  Properties

        public bool Sizable { get; set; }
        public bool SmartBounds { get; set; }
        public bool RoundCorners
        {
            get
            {
                return _roundCorners;
            }
            set
            {
                _roundCorners = value;
                Invalidate();
            }
        }
        public bool IsParentForm { get; private set; }
        public bool IsParentMdi { get { return Parent.Parent != null; } }
        public bool ControlMode
        {
            get
            {
                return _controlMode;
            }
            set
            {
                _controlMode = value;
                Invalidate();
            }
        }
        public Color HeaderBackColor
        {
            get { return _headerBackColor; }
            set
            {
                _headerBackColor = value;
                try
                {
                    if (_headerBackColor == Color.Transparent & BackColor != TransparencyKey)
                        _headerBackColor = BackColor;
                    else if (_headerBackColor == Color.Transparent & Parent.BackColor != TransparencyKey)
                        _headerBackColor = Parent.BackColor;
                }
                catch
                { }

                Invalidate();
                OnHeaderAttributeChanged(EventArgs.Empty);
            }
        }
        public int HeaderHeight
        {
            get { return _headerHeight; }
            set
            {
                _headerHeight = value;
                Padding = new Padding(10, _headerHeight + 4, 10, 9);
                Invalidate();
                OnHeaderAttributeChanged(EventArgs.Empty);
            }
        }
        public int BorderRadius
        {
            get { return _borderRadius; }
            set
            {
                _borderRadius = value;
                Invalidate();
            }
        }

        public event EventHandler HeaderAttributeChanged;
        #endregion
        #region Form Mirror

        #region Form Mirror Variables

        private IButtonControl _acceptButton;
        private bool _autoScroll;
        private bool _autoSize;
        private AutoSizeMode _autoSizeMode;
        private AutoValidate _autoValidate;
        private IButtonControl _cancelButton;
        private bool _controlBox;
        private bool _isMdiContainer;
        private Icon _icon;
        private bool _keyPreview;
        private MenuStrip _mainMenuStrip;
        private Size _maximumSize;
        private bool _minimizeBox;
        private Size _minimumSize;
        private double _opacity;
        private bool _rightToLeftLayout;
        private bool _showIcon;
        private bool _showInTaskbar;
        private SizeGripStyle _sizeGripStyle;
        private FormStartPosition _startPosition = default(FormStartPosition);
        private bool _topMost;
        private Color _transparencyKey;
        private FormWindowState _windowState;

        #endregion
        #region Form Mirror Properties

        public IButtonControl AcceptButton
        {
            get
            {
                if (!IsParentForm || _controlMode) return _acceptButton;
                return ParentForm != null ? ParentForm.AcceptButton : _acceptButton;
            }
            set
            {
                _acceptButton = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.AcceptButton = value;
            }
        }
        public new bool AutoScroll
        {
            get
            {
                if (IsParentForm && !_controlMode) return ParentForm != null && ParentForm.AutoScroll;
                return _autoScroll;
            }
            set
            {
                _autoScroll = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.AutoScroll = value;
            }
        }
        public new bool AutoSize
        {
            get
            {
                if (IsParentForm && !_controlMode) return ParentForm != null && ParentForm.AutoSize;
                return _autoSize;
            }
            set
            {
                _autoSize = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.AutoSize = value;
            }
        }
        public AutoSizeMode AutoSizeMode
        {
            get
            {
                if (!IsParentForm || _controlMode) return _autoSizeMode;
                return ParentForm != null ? ParentForm.AutoSizeMode : _autoSizeMode;
            }
            set
            {
                _autoSizeMode = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.AutoSizeMode = value;
            }
        }
        public new AutoValidate AutoValidate
        {
            get
            {
                if (!IsParentForm || _controlMode) return _autoValidate;
                return ParentForm != null ? ParentForm.AutoValidate : _autoValidate;
            }
            set
            {
                _autoValidate = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.AutoValidate = value;
            }
        }
        public new Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.BackColor = value;
            }
        }
        public IButtonControl CancelButton
        {
            get
            {
                if (!IsParentForm || _controlMode) return _cancelButton;
                return ParentForm != null ? ParentForm.CancelButton : _cancelButton;
            }
            set
            {
                _cancelButton = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.CancelButton = value;
            }
        }
        public bool ControlBox
        {
            get
            {
                if (IsParentForm && !_controlMode) return ParentForm != null && ParentForm.ControlBox;
                return _controlBox;
            }
            set
            {
                _controlBox = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.ControlBox = value;
            }
        }
        public Icon Icon
        {
            get
            {
                if (!IsParentForm || _controlMode) return _icon;
                return ParentForm != null ? ParentForm.Icon : _icon;
            }
            set
            {
                _icon = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.Icon = value;
                Invalidate();
            }
        }
        public bool IsMdiContainer
        {
            get
            {
                if (IsParentForm && !_controlMode) return ParentForm != null && ParentForm.IsMdiContainer;
                return _isMdiContainer;
            }
            set
            {
                _isMdiContainer = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.IsMdiContainer = value;
            }
        }
        public bool KeyPreview
        {
            get
            {
                if (IsParentForm && !_controlMode) return ParentForm != null && ParentForm.KeyPreview;
                return _keyPreview;
            }
            set
            {
                _keyPreview = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.KeyPreview = value;
            }
        }
        [DefaultValue(@"")]
        [TypeConverter(typeof(ReferenceConverter))]
        public MenuStrip MainMenuStrip
        {
            get
            {
                if (!IsParentForm || _controlMode) return _mainMenuStrip;
                return ParentForm != null ? ParentForm.MainMenuStrip : _mainMenuStrip;
            }
            set
            {
                _mainMenuStrip = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.MainMenuStrip = value;
            }
        }
        public new Size MaximumSize
        {
            get
            {
                if (!IsParentForm || _controlMode) return _maximumSize;
                return ParentForm != null ? ParentForm.MaximumSize : _maximumSize;
            }
            set
            {
                _maximumSize = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.MaximumSize = value;
            }
        }
        public bool MinimizeBox
        {
            get
            {
                if (IsParentForm && !_controlMode) return ParentForm != null && ParentForm.MinimizeBox;
                return _minimizeBox;
            }
            set
            {
                _minimizeBox = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.MinimizeBox = value;
            }
        }
        public new Size MinimumSize
        {
            get
            {
                if (!IsParentForm || _controlMode) return _minimumSize;
                return ParentForm != null ? ParentForm.MinimumSize : _minimumSize;
            }
            set
            {
                _minimumSize = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.MinimumSize = value;
            }
        }
        [DefaultValue(1)]
        [TypeConverter(typeof(OpacityConverter))]
        public double Opacity
        {
            get
            {
                if (!IsParentForm || _controlMode) return _opacity;
                return ParentForm != null ? ParentForm.Opacity : _opacity;
            }
            set
            {
                _opacity = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.Opacity = value;
            }
        }
        public bool RightToLeftLayout
        {
            get
            {
                if (IsParentForm && !_controlMode) return ParentForm != null && ParentForm.RightToLeftLayout;
                return _rightToLeftLayout;
            }
            set
            {
                _rightToLeftLayout = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.RightToLeftLayout = value;
            }
        }
        public bool ShowIcon
        {
            get
            {
                if (IsParentForm && !_controlMode) return ParentForm != null && ParentForm.ShowIcon;
                return _showIcon;
            }
            set
            {
                _showIcon = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.ShowIcon = value;
            }
        }
        public bool ShowInTaskbar
        {
            get
            {
                if (IsParentForm && !_controlMode) return ParentForm != null && ParentForm.ShowInTaskbar;
                return _showInTaskbar;
            }
            set
            {
                _showInTaskbar = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.ShowInTaskbar = value;
            }
        }
        public SizeGripStyle SizeGripStyle
        {
            get
            {
                if (!IsParentForm || _controlMode) return _sizeGripStyle;
                return ParentForm != null ? ParentForm.SizeGripStyle : _sizeGripStyle;
            }
            set
            {
                _sizeGripStyle = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.SizeGripStyle = value;
            }
        }
        public FormStartPosition StartPosition
        {
            get
            {
                if (!IsParentForm || _controlMode) return _startPosition;
                return ParentForm != null ? ParentForm.StartPosition : _startPosition;
            }
            set
            {
                _startPosition = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.StartPosition = value;
            }
        }
        public bool TopMost
        {
            get { return IsParentForm && !_controlMode ? ParentForm != null && ParentForm.TopMost : _topMost; }
            set
            {
                _topMost = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.TopMost = value;
            }
        }
        public Color TransparencyKey
        {
            get
            {
                if (!IsParentForm || _controlMode) return _transparencyKey;
                return ParentForm != null ? ParentForm.TransparencyKey : _transparencyKey;
            }
            set
            {
                _transparencyKey = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.TransparencyKey = value;
            }
        }
        public FormWindowState WindowState
        {
            get
            {
                if (!IsParentForm || _controlMode) return _windowState;
                return ParentForm != null ? ParentForm.WindowState : _windowState;
            }
            set
            {
                _windowState = value;
                if (!IsParentForm || _controlMode) return;
                if (ParentForm != null) ParentForm.WindowState = value;
            }
        }

        #endregion

        #endregion
        #region  Sizing Feature

        private bool _b1X;
        private bool _b2X;
        private bool _b3;
        private bool _b4;
        private bool _wmLmbuttondown;
        private int _current;
        private int _previous;
        private readonly Message[] _messages = new Message[9];
        private Point _getIndexPoint = default(Point);

        private int GetIndex()
        {
            _getIndexPoint = PointToClient(MousePosition);
            _b1X = _getIndexPoint.X < 7;
            _b2X = _getIndexPoint.X > Width - 7;
            _b3 = _getIndexPoint.Y < 7;
            _b4 = _getIndexPoint.Y > Height - 7;

            if (_b1X && _b3)
            {
                return 4;
            }
            if (_b1X && _b4)
            {
                return 7;
            }
            if (_b2X && _b3)
            {
                return 5;
            }
            if (_b2X && _b4)
            {
                return 8;
            }
            if (_b1X)
            {
                return 1;
            }
            if (_b2X)
            {
                return 2;
            }
            if (_b3)
            {
                return 3;
            }
            if (_b4)
            {
                return 6;
            }
            return 0;
        }

        private void InvalidateMouse()
        {
            _current = GetIndex();
            if (_current == _previous)
            {
                return;
            }

            _previous = _current;
            switch (_previous)
            {
                case 0:
                    Cursor = Cursors.Default;
                    break;
                case 6:
                    Cursor = Cursors.SizeNS;
                    break;
                case 8:
                    Cursor = Cursors.SizeNWSE;
                    break;
                case 7:
                    Cursor = Cursors.SizeNESW;
                    break;
            }
        }

        private void InitializeMessages()
        {
            _messages[0] = Message.Create(Parent.Handle, 161, new IntPtr(2), IntPtr.Zero);
            for (var I = 1; I <= 8; I++)
            {
                _messages[I] = Message.Create(Parent.Handle, 161, new IntPtr(I + 9), IntPtr.Zero);
            }
        }

        private void CorrectBounds(Rectangle bounds)
        {
            if (Parent.Width > bounds.Width)
            {
                Parent.Width = bounds.Width;
            }
            if (Parent.Height > bounds.Height)
            {
                Parent.Height = bounds.Height;
            }

            var x = Parent.Location.X;
            var y = Parent.Location.Y;

            if (x < bounds.X)
            {
                x = bounds.X;
            }
            if (y < bounds.Y)
            {
                y = bounds.Y;
            }

            var width = bounds.X + bounds.Width;
            var height = bounds.Y + bounds.Height;

            if (x + Parent.Width > width)
            {
                x = width - Parent.Width;
            }
            if (y + Parent.Height > height)
            {
                y = height - Parent.Height;
            }

            Parent.Location = new Point(x, y);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (!_wmLmbuttondown || m.Msg != 513) return;
            _wmLmbuttondown = false;

            if (SmartBounds & Parent != null)
            {
                CorrectBounds(IsParentMdi
                    ? new Rectangle(Point.Empty, Parent.Parent.Size)
                    : Screen.FromControl(Parent).WorkingArea);
            }
        }

        public void DoSmartBounds()
        {
            if (SmartBounds & Parent != null)
            {
                CorrectBounds(IsParentMdi
                    ? new Rectangle(Point.Empty, Parent.Parent.Size)
                    : Screen.FromControl(Parent).WorkingArea);
            }
        }

        #endregion
        #region  Events

        protected override void CreateHandle()
        {
            base.CreateHandle();
            var parentForm = FindForm();
            if (parentForm == null || !parentForm.Controls.Contains(this)) return;

            parentForm.Controls.Remove(this);
            foreach (Control item in parentForm.Controls)
            {
                parentForm.Controls.Remove(item);
                Controls.Add(item);
            }

            parentForm.Controls.Add(this);
            Parent = parentForm;
        }
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            if (Parent == null) return;

            IsParentForm = Parent is Form;

            if (_controlMode) return;
            InitializeMessages();

            if (!IsParentForm) return;
            if (ParentForm == null) return;
            ParentForm.TransparencyKey = Color.Fuchsia;
            ParentForm.TextChanged += ParentFormTextChanged;

            if (!DesignMode)
            {
                ParentForm.Shown += ParentFormShown;
            }
            foreach (Control item in Parent.Controls)
            {
                if (item != this)
                {
                    Parent.Controls.Remove(item);
                    Controls.Add(item);
                }
            }
            Parent.BackColor = BackColor;
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (!_controlMode)
            {
                _headerRect = new Rectangle(0, 0, Width - 14, _headerHeight - 7);
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
            if (ParentForm == null ||
                (IsParentForm && ParentForm.WindowState == FormWindowState.Maximized || _controlMode))
                return;
            if (_headerRect.Contains(e.Location))
            {
                Capture = false;
                _wmLmbuttondown = true;
                DefWndProc(ref _messages[0]);
            }
            else if (Sizable && _previous != 0)
            {
                Capture = false;
                _wmLmbuttondown = true;
                DefWndProc(ref _messages[_previous]);
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _cap = false;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (ParentForm != null && !(IsParentForm && ParentForm.WindowState == FormWindowState.Maximized))
            {
                if (Sizable && !_controlMode)
                {
                    InvalidateMouse();
                }
            }
            if (_cap)
            {
                Parent.Location = (Point)((object)(Convert.ToDouble(MousePosition) - Convert.ToDouble(_mouseP)));
            }
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var _shape = new GraphicsPath();

            var gfx = e.Graphics;
            gfx.SmoothingMode = SmoothingMode.HighQuality;
            gfx.CompositingQuality = CompositingQuality.HighQuality;

            using (var transBrush = new SolidBrush(TransparencyKey))
            {
                gfx.FillRectangle(
                    transBrush,
                    e.ClipRectangle.X,
                    e.ClipRectangle.Y,
                    e.ClipRectangle.Width,
                    e.ClipRectangle.Height);
            }

            if (RoundCorners)
            {
                using (var headerBrush = new SolidBrush(HeaderBackColor))
                {
                    using (var headerPen = new Pen(HeaderBackColor, 5))
                    {
                        var headerRect = new Rectangle(0, 0, Width, HeaderHeight + 1);
                        gfx.FillPath(
                            headerBrush,
                            Design.RoundTopRect(headerRect, BorderRadius));
                        gfx.DrawPath(
                            headerPen,
                            Design.RoundTopRect(headerRect, BorderRadius));
                    }
                }
                using (var bodyBrush = new SolidBrush(BackColor))
                {
                    using (var headerPen = new Pen(BackColor, 5))
                    {
                        var bodyRect = new Rectangle(0, HeaderHeight, Width, Height - HeaderHeight);
                        gfx.FillPath(
                            new SolidBrush(BackColor),
                            Design.RoundBottomRect(bodyRect, BorderRadius));
                        gfx.DrawPath(
                            headerPen,
                    Design.RoundBottomRect(bodyRect, BorderRadius));
                    }
                }
            }
            else
            {
                gfx.FillRectangle(
                    new SolidBrush(HeaderBackColor),
                    new Rectangle(0, 0, Width, HeaderHeight + 2));
                gfx.FillRectangle(
                    new SolidBrush(BackColor),
                    new Rectangle(0, HeaderHeight + 1, Width, Height - HeaderHeight));
            }

            if (Icon != null & HeaderHeight > 0)
            {
                gfx.CompositingQuality = CompositingQuality.HighQuality;
                var rect = new Rectangle(0, 0, HeaderHeight, HeaderHeight);
                rect.Inflate(-(int)(HeaderHeight / 5f), -(int)(HeaderHeight / 5f));
                gfx.DrawIcon(Icon, rect);

                gfx.DrawString(
                    Text, Font,
                    new SolidBrush(ForeColor),
                    new Rectangle(HeaderHeight, 0, Width - 1, HeaderHeight),
                    Design.GetStringFormat(ContentAlignment.MiddleLeft));
            }
            else
                gfx.DrawString(
                    Text, Font,
                    new SolidBrush(ForeColor),
                    new Rectangle(Padding.Left, 0, Width - 1, HeaderHeight),
                    Design.GetStringFormat(ContentAlignment.MiddleLeft));
        }

        private void OnHeaderAttributeChanged(EventArgs e)
        {
            if (HeaderAttributeChanged != null)
                HeaderAttributeChanged.Invoke(this, e);
        }
        private void ParentFormShown(object sender, EventArgs e)
        {
            if (_controlMode || _hasShown) return;

            if (_startPosition == FormStartPosition.CenterParent || _startPosition == FormStartPosition.CenterScreen)
            {
                var sb = Screen.PrimaryScreen.Bounds;
                if (ParentForm != null)
                {
                    var cb = ParentForm.Bounds;
                    ParentForm.Location = new Point(sb.Width / 2 - cb.Width / 2, sb.Height / 2 - cb.Height / 2);
                }
            }
            _hasShown = true;
        }
        private void ParentFormTextChanged(object sender, EventArgs e)
        {
            if (Parent != null)
                Text = Parent.Text;
        }

        #endregion
        #region Constructors

        public MonoTheme()
        {
            Sizable = true;
            SmartBounds = true;
            SetStyle((ControlStyles)(139270), true);
            BackColor = Color.FromArgb(32, 41, 50);
            ForeColor = Color.White;
            Padding = new Padding(10, 70, 10, 9);
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
            _headerHeight = 36;
            Padding = new Padding(10, _headerHeight + 4, 10, 9);
            Font = new Font(@"Segoe UI", 9);
            BorderRadius = 4;
        }

        #endregion
    }
}
