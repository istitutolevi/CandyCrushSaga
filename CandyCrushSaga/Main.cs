using CandyCrushSaga.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace CandyCrushSaga
{
    public partial class Main : Form
    {
        #region Struct & Object classes

        class Candy
        {
            public int Type = -1;
            public RectangleF Rect = default(RectangleF);
            public Color BackColor = Color.White;
            internal bool Active = true;

            public Candy(int type)
            {
                SetType(type);
            }
            public void SetType(int type)
            {
                if (type < -1)
                    throw new Exception("Invalid candy type");
                else
                {
                    switch (type)
                    {
                        case 0:
                            BackColor = Color.Red;
                            break;
                        case 1:
                            BackColor = Color.Green;
                            break;
                        case 2:
                            BackColor = Color.Yellow;
                            break;
                        case 3:
                            BackColor = Color.LightBlue;
                            break;
                        default:
                            BackColor = Color.Transparent;
                            break;
                    }

                    Type = type;
                }
            }
        }

        class CandyPointer
        {
            public int Row = -1;
            public int Column = -1;
            public CandyPointer(int row, int column)
            {
                Row = row;
                Column = column;
            }
            public override bool Equals(object obj)
            {
                var index = (CandyPointer)obj;
                if (index == null) return false;

                return index.Row == Row & index.Column == Column;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        #endregion
        #region Variables

        private static Candy[][] _grid;
        private static int _rowSize = 8;
        private static int _columnSize = 8;
        private static bool _mouseDown = false;
        private static List<CandyPointer> _slotsArround = new List<CandyPointer>();
        private static CandyPointer _selectedIndex = new CandyPointer(-1,-1);
        private static bool _animating = false;
        private static int _totalMatched = 0;
        private static long _totalScore = 0;
        private static Image candy1 = Properties.Resources.candy1;
        private static Image candy2 = Properties.Resources.candy2;
        private static Image candy6 = Properties.Resources.candy6;
        private static Image candy7 = Properties.Resources.candy7;
        private static int _sleepTime = 100;
        private int _maxRowSize = 60;
        private int _maxColumnSize = 150;
        private int _minColumnSize = 10;
        private int _minRowSize = 10;

        #endregion
        #region Events

        private void btnCheck_Click(object sender, EventArgs e)
        {
            ThreadManager.RunAsync(() => {
                ThreadManager.RunUnsafeCode(() => {
                    MatcheExistOnGrid(true);
                });
            });
        }

        private void btnLoadValues_Click(object sender, EventArgs e)
        {
            InitializeMap();
            RunAnimation();
        }

        private void screen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !_animating)
            {
                _mouseDown = true;
                if (GetPosOfCandyLocatedAtPoint(e.Location, ref _selectedIndex))
                {
                    _slotsArround = SlotsArround(_selectedIndex);
                    DrawCandies();
                }
            }
        }

        private void screen_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_mouseDown || _animating)
                return;

            var nextIndex = new CandyPointer(-1,-1);
            if (GetPosOfCandyLocatedAtPoint(e.Location, ref nextIndex))
            {
                if (nextIndex.Equals(_selectedIndex)) return;

                foreach (var pos in _slotsArround)
                {
                    if (nextIndex.Equals(pos))
                    {
                        SwapCandies(_selectedIndex, nextIndex);
                        if(MatcheExistOnGrid(false))
                        {
                            RelocateAll();
                            DrawCandies();
                            _mouseDown = false;
                            break;
                        }
                        else
                            SwapCandies(_selectedIndex, nextIndex);
                    }
                }
            }
        }

        private void screen_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
            _slotsArround.Clear();

            RunAnimation();
        }

        private void ThemeWrapper_Resize(object sender, EventArgs e)
        {
            ScaleScreen();

            //set all candies locations
            RelocateAll();
            //draw candies on screen
            DrawCandies();
        }

        #endregion
        #region Methods

        private void InitializeMap()
        {
            //get size from textboxes
            _rowSize = 8;
            _columnSize = 8;
            int.TryParse(txtGridHeight.Text, out _rowSize);
            int.TryParse(txtGridWidth.Text, out _columnSize);
            _rowSize = _rowSize < _minRowSize ? _minRowSize : _rowSize > _maxRowSize ? _maxRowSize : _rowSize;
            _columnSize = _columnSize < _minColumnSize ? _minColumnSize : _columnSize > _maxColumnSize ? _maxColumnSize : _columnSize;
            txtGridHeight.Text = _rowSize.ToString();
            txtGridWidth.Text = _columnSize.ToString();

            ScaleScreen();


            _totalMatched = 0;
            _totalScore = 0;
            lblScore.Text = "0";

            //initialize map with grid size
            ThreadManager.RunUnsafeCode(() =>
            {
                _grid = new Candy[_rowSize][];
                for (int row = 0; row < _grid.Length; row++)
                    _grid[row] = new Candy[_columnSize];
            });

            //fill map with random candies
            for (int row = 0; row < _rowSize; row++)
                for (int column = 0; column < _columnSize; column++)
                    _grid[row][column] = new Candy(Randomizer.Next(4));

            //set all candies locations
            RelocateAll();
            //draw candies on screen
            DrawCandies();
        }

        private void RunAnimation()
        {
            ThreadManager.RunAsync(() => {
                if (_animating) return;

                _animating = true;
                ThreadManager.RunUnsafeCode(() => {
                    while (MatcheExistOnGrid(true))
                        AnimateAll();
                });
                _animating = false;
            });
        }

        private void DrawCandies()
        {
            ThreadManager.RunUnsafeCode(() =>
            {
                int width = (screen.Width - _columnSize) / _columnSize;
                int height = (screen.Height - _rowSize) / _rowSize;

                var img = new Bitmap(screen.Width, screen.Height);

                using (var gfx = Graphics.FromImage(img))
                {
                    //set for high quality drawing
                    gfx.CompositingQuality = CompositingQuality.HighSpeed;
                    gfx.SmoothingMode = SmoothingMode.HighQuality;


                    for (int row = 0; row < _rowSize; row++)
                    {
                        for (int column = 0; column < _columnSize; column++)
                        {
                            if (_grid[row][column].Active)
                            {
                                _grid[row][column].Rect.Size = new Size(width, height);
                                var rect = new RectangleF(_grid[row][column].Rect.Location, _grid[row][column].Rect.Size);
                                rect.Inflate(-1, -1);
                                using (var brush = new SolidBrush(_grid[row][column].BackColor))
                                {
                                    switch (_grid[row][column].Type)
                                    {
                                        //case 0:
                                        //    gfx.DrawImage(candy1, rect);
                                        //    break;
                                        //case 1:
                                        //    gfx.DrawImage(candy2, rect);
                                        //    break;
                                        //case 2:
                                        //    gfx.DrawImage(candy6, rect);
                                        //    break;
                                        //case 3:
                                        //    gfx.DrawImage(candy7, rect);
                                        //    break;
                                        case 0:
                                            //draw ellipse
                                            gfx.FillEllipse(brush, rect);
                                            break;
                                        case 1:
                                            //draw rotated square
                                            gfx.FillPolygon(brush,
                                                new PointF[] {
                                                        new PointF(rect.X + rect.Width / 2, rect.Y),
                                                        new PointF(rect.X, rect.Y + rect.Height / 2),
                                                        new PointF(rect.X + rect.Width / 2, rect.Y  + rect.Height),
                                                        new PointF(rect.X + rect.Width, rect.Y  + rect.Height / 2)
                                                });
                                            break;
                                        case 2:
                                            //draw rectangle shape
                                            gfx.FillPolygon(brush,
                                                new PointF[] {
                                                        new PointF(rect.X + rect.Width / 2, rect.Y),
                                                        new PointF(rect.X, rect.Y + rect.Height),
                                                        new PointF(rect.X + rect.Width, rect.Y  + rect.Height)
                                                });
                                            break;
                                        case 3:
                                            //draw polygon shape
                                            gfx.FillPolygon(brush,
                                                new PointF[] {
                                                        new PointF(rect.X + (rect.Width / 3)*2, rect.Y),
                                                        new PointF(rect.X + rect.Width / 3, rect.Y),
                                                        new PointF(rect.X, rect.Y + rect.Height / 3),
                                                        new PointF(rect.X, rect.Y + (rect.Height / 3)*2),
                                                        new PointF(rect.X + rect.Width / 3, rect.Y + rect.Height),
                                                        new PointF(rect.X + (rect.Width / 3)*2, rect.Y + rect.Height),
                                                        new PointF(rect.X + rect.Width, rect.Y + (rect.Height / 3)*2),
                                                        new PointF(rect.X + rect.Width, rect.Y + rect.Height / 3),
                                                });
                                            break;
                                    }
                                }
                                if (_selectedIndex != null)
                                {
                                    if(_selectedIndex.Column == column & _selectedIndex.Row == row & _mouseDown)
                                    {
                                        using (var pen = new Pen(Color.Snow, 1.5f))
                                        {
                                            gfx.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //call invoke because this method will be called by cross-threading
                screen.Invoke(new Action(() =>
                {
                    screen.Image = img;
                }));
            });
        }

        private void RelocateAll()
        {
            if (_grid == null)
                return;

            int width = (screen.Width - _columnSize) / _columnSize;
            int height = (screen.Height - _rowSize) / _rowSize;

            for (int row = 0; row < _rowSize; row++)
            {
                for (int column = 0; column < _columnSize; column++)
                {
                    _grid[row][column].Rect.Size = new Size(width, height);
                    _grid[row][column].Rect.Location = new Point(column * width + column, row * height + row);
                }
            }
        }

        private void RelocateColumn(int column)
        {
            int width = (screen.Width - _columnSize) / _columnSize;
            int height = (screen.Height - _rowSize) / _rowSize;

            for (int row = 0; row < _rowSize; row++)
            {
                _grid[row][column].Rect.Size = new Size(width, height);
                _grid[row][column].Rect.Location = new Point(column * width + column, row * height + row);
            }
        }

        private bool MatcheExistOnGrid(bool remove = false)
        {
            bool combinationExists = false;

            //horizontal match check
            for (int row = 0; row < _rowSize; row++)
            {
                var combinationCount = 0;
                var startIndex = -1;

                for (int column = 0; column < _columnSize - 1; column++)
                {
                    if (_grid[row][column].Type == _grid[row][column + 1].Type)//trovato sequenza
                    {
                        combinationCount++;

                        if (combinationCount == 1)//ha trovato la prima sequenza
                        {
                            //teniamo tracia dell'indice
                            startIndex = column;
                        }
                    }
                    else//sequenza non ancora trovato o sta uscendo da una sequenza
                    {
                        if (combinationCount >= 2)//piu di due caramelle in fila
                        {
                            if (remove)
                            {
                                HideHorizontaleCandies(row, startIndex, (startIndex + combinationCount));
                                UpdateScore(combinationCount);
                            }
                            combinationExists = true;
                        }

                        //resettiamo il contattore
                        combinationCount = 0;
                    }
                }
                if (combinationCount >= 2)//piu di due caramelle in fila
                {
                    if (remove)
                    {
                        HideHorizontaleCandies(row, startIndex, (startIndex + combinationCount));
                        UpdateScore(combinationCount);
                    }
                    combinationExists = true;
                }
            }



            //vertical match check
            for (int column = 0; column < _columnSize; column++)
            {
                var combinationCount = 0;
                var startIndex = -1;

                for (int row = 0; row < _rowSize - 1; row++)
                {
                    if (_grid[row][column].Type == _grid[row + 1][column].Type)//trovato sequenza
                    {
                        combinationCount++;

                        if (combinationCount == 1)//ha trovato la prima sequenza
                        {
                            //teniamo tracia dell'indice
                            startIndex = row;
                        }
                    }
                    else//sequenza non ancora trovato o sta uscendo da una sequenza
                    {
                        if (combinationCount >= 2)//piu di due caramelle in fila
                        {
                            if (remove)
                            {
                                HideVerticaleCandies(column, startIndex, (startIndex + combinationCount));
                                UpdateScore(combinationCount);
                            }
                            combinationExists = true;
                        }

                        //resettiamo il contattore
                        combinationCount = 0;
                    }
                }
                if (combinationCount >= 2)//piu di due caramelle in fila
                {
                    if (remove)
                    {
                        HideVerticaleCandies(column, startIndex, (startIndex + combinationCount));
                        UpdateScore(combinationCount);
                    }
                    combinationExists = true;
                }
            }

            if (remove)
                DrawCandies();

            return combinationExists;
        }

        private void UpdateScore(int count)
        {
            _totalScore = count < 5 ? _totalMatched + count : ((1 / (count - 4)) + 1) * _totalScore;
            _totalMatched += count;
            lblScore.Invoke(new Action(() =>
            {
                lblScore.Text = _totalScore.ToString();
            }));
        }

        private bool InactiveExist()
        {
            //horizontal match check
            for (int rpw = 0; rpw < _rowSize; rpw++)
                for (int column = 0; column < _columnSize - 1; column++)
                    if (!_grid[rpw][column].Active)
                        return true;



            //vertical match check
            for (int column = 0; column < _columnSize; column++)
                for (int row = 0; row < _rowSize - 1; row++)
                    if (!_grid[row][column].Active)
                        return true;

            return false;
        }

        private void ScaleScreen()
        {
            ThreadManager.RunUnsafeCode(() =>
            {
                Invoke(new Action(() => {
                    if (WindowState == FormWindowState.Maximized)
                    {
                        int availableWidth = Width - lblScore.Width - Padding.Horizontal - lblScore.Margin.Horizontal;
                        int availableHeight = Height - Padding.Vertical;
                        if (availableWidth > availableHeight)
                            _columnSize = ((_rowSize * availableWidth * Height) / Width) / availableHeight;
                        else
                            _rowSize = ((availableWidth * _columnSize * Width) / (availableHeight * Height));
                        screen.Width = (screen.Height * _columnSize) / _rowSize;
                        txtGridWidth.Text = _columnSize.ToString();
                        //initialize map with grid size
                        ThreadManager.RunUnsafeCode(() =>
                        {
                            _grid = new Candy[_rowSize][];
                            for (int row = 0; row < _grid.Length; row++)
                                _grid[row] = new Candy[_columnSize];
                        });

                        //fill map with random candies
                        for (int row = 0; row < _rowSize; row++)
                            for (int column = 0; column < _columnSize; column++)
                                _grid[row][column] = new Candy(Randomizer.Next(4));
                    }
                    else
                    {
                        screen.Width = (screen.Height * _columnSize) / _rowSize;
                        int newWidth = screen.Location.X + screen.Width + 2 * Padding.Right;
                        Size = new Size(newWidth, Height);
                    }
                }));
            });
        }

        private void AnimateAll()
        {
            for (int column = 0; column < _columnSize; column++)
            {
                if (!_grid[0][column].Active)
                {
                    _grid[0][column].SetType(Randomizer.Next(4));
                    _grid[0][column].Active = true;
                }
                else
                    ShiftDownColumn(column);
            }
            DrawCandies();
            
            if (InactiveExist())
            {
                Thread.Sleep(toggleSpeed.Toggled ? 0 : _sleepTime);
                AnimateAll();
            }
        }

        private void ShiftDownColumn(int column)
        {
            for (int row = 0; row < _rowSize; row++)
            {
                if (row + 1 < _rowSize)
                {
                    if (_grid[row][column].Active && !_grid[row + 1][column].Active)
                    {
                        ShiftDownCandy(row, column);
                        row++;
                    }
                }
            }
        }

        private void ShiftDownCandy(int row, int column)
        {
            ThreadManager.RunUnsafeCode(() => {
                Candy temp = _grid[row][column];
                _grid[row][column] = _grid[row + 1][column];
                _grid[row + 1][column] = temp;

                int width = (screen.Width - _columnSize) / _columnSize;
                int height = (screen.Height - _rowSize) / _rowSize;
                _grid[row][column].Rect.Location = new Point(column * width + column, row * height + row);
                _grid[row + 1][column].Rect.Location = new Point(column * width + column, (row + 1) * height + (row + 1));
            });
        }

        private void SwapCandies(CandyPointer left, CandyPointer right)
        {
            var temp = _grid[left.Row][left.Column];
            _grid[left.Row][left.Column] = _grid[right.Row][right.Column];
            _grid[right.Row][right.Column] = temp;
        }

        private void HideHorizontaleCandies(int i, int startIndex, int stopIndex)
        {
            //no need to execute futher if this condition is false
            if (startIndex < 0 || startIndex >= _columnSize) return;

            for (int column = startIndex; column < _columnSize && column <= stopIndex; column++)
                _grid[i][column].Active = false;
        }

        private void HideVerticaleCandies(int k, int startIndex, int stopIndex)
        {
            //no need to execute futher if this condition is false
            if (startIndex < 0 || startIndex >= _rowSize) return;

            for (int row = startIndex; row < _rowSize && row <= stopIndex; row++)
                _grid[row][k].Active = false;
        }

        private List<CandyPointer> SlotsArround(CandyPointer index)
        {
            var list = new List<CandyPointer>();

            if (index.Row - 1 >= 0)
                list.Add(new CandyPointer(index.Row - 1, index.Column));
            if (index.Column - 1 >= 0)
                list.Add(new CandyPointer(index.Row, index.Column - 1));
            if (index.Row + 1 < _rowSize)
                list.Add(new CandyPointer(index.Row + 1, index.Column));
            if (index.Column + 1 < _columnSize)
                list.Add(new CandyPointer(index.Row, index.Column + 1));

            return list;
        }

        private bool GetPosOfCandyLocatedAtPoint(Point location, ref CandyPointer currentIndex)
        {
            for (int row = 0; row < _rowSize; row++)
                for (int column = 0; column < _columnSize; column++)
                    if (_grid[row][column].Rect.Contains(location))
                    {
                        currentIndex.Row = row;
                        currentIndex.Column = column;
                        return true;
                    }

            currentIndex.Row = -1;
            currentIndex.Column = -1;
            return false;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
            Environment.Exit(0);
        }

        #endregion
        #region Constructors

        public Main()
        {
            InitializeComponent();
            InitializeMap();
        }

        #endregion
    }
}