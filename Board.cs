namespace Chess
{
    public partial class Board : Form
    {
        private Field _field = new();
        private bool _isSelected = false;
        private Point _selected;
        private LinkedList<(Point, Moves)>? _path;

        public Board()
        {
            InitializeComponent();
            foreach (PictureBox square in _field)
                _pieces.Controls.Add(square);
        }

        private void OnMouseEnterTheSquere(object sender, EventArgs e)
        {
            if (sender is PictureBox squere)
            {
                if (!_isSelected && squere is Piece piece && piece.PieceColor == _field.Turn)
                {
                    if (squere.BackColor == Color.White)
                        squere.BackColor = Color.LimeGreen;
                    else if (squere.BackColor == Color.Black)
                        squere.BackColor = Color.Green;
                }
                else if (_isSelected)
                {
                    if (squere.BackColor == Color.Red)
                        squere.BackColor = Color.Yellow;
                    else if (squere.BackColor == Color.Brown)
                        squere.BackColor = Color.Orange;
                    else if (squere.BackColor == Color.Green)
                        squere.BackColor = Color.DarkGreen;
                    else if (squere.BackColor == Color.LimeGreen)
                        squere.BackColor = Color.LightGreen;
                }
            }
        }
        private void OnMouseLeaveTheSquere(object sender, EventArgs e)
        {
            if (sender is PictureBox square)
            {
                if (!_isSelected)
                {
                    if (square.BackColor == Color.Green)
                        square.BackColor = Color.Black;
                    else if (square.BackColor == Color.LimeGreen)
                        square.BackColor = Color.White;
                }
                else
                {
                    if (square.BackColor == Color.Yellow)
                        square.BackColor = Color.Red;
                    else if (square.BackColor == Color.Orange)
                        square.BackColor = Color.Brown;
                    else if (square.BackColor == Color.LightGreen)
                        square.BackColor = Color.LimeGreen;
                    else if (square.BackColor == Color.DarkGreen)
                        square.BackColor = Color.Green;
                }
            }
        }
        private void OnMouseClickTheSquere(object sender, EventArgs e)
        {
            if (sender is PictureBox square)
            {
                if (!_isSelected && square is Piece piece && square.BackColor != Color.Black && square.BackColor != Color.White)
                {
                    _selected = piece.CurrentCordinates;
                    _isSelected = true;
                    LinkedList<Point> possibleChecks = _field.PossibleChecks(_field[_selected.Y, _selected.X] as Piece);
                    var filter = piece
                        ?.BuildPath(_field)
                        ?.Where(i => !possibleChecks.Contains(i.Item1));
                    _path = new LinkedList<(Point, Moves)>(filter);
                    foreach ((Point, Moves) item in _path)
                    {
                        if (_field[item.Item1.Y, item.Item1.X].BackColor == Color.White)
                            _field[item.Item1.Y, item.Item1.X].BackColor = Color.Red;
                        else if (_field[item.Item1.Y, item.Item1.X].BackColor == Color.Black)
                            _field[item.Item1.Y, item.Item1.X].BackColor = Color.Brown;
                    }
                }
                else if (_isSelected && square.BackColor != Color.Black && square.BackColor != Color.White)
                {
                    if (square.BackColor == Color.Yellow || square.BackColor == Color.Orange)
                    {
                        Point location = new Point() { X = (square.Location.X - 10) / 60, Y = (square.Location.Y - 10) / 60 };
                        Moves move = _path
                                        .Where(i => i.Item1 == location)
                                        .Select(i => i.Item2)
                                        .First();
                        switch (move)
                        {
                            case Moves.Capture:
                                Capture(location.X, location.Y);
                                MakeMove(location.X, location.Y);
                                break;
                            case Moves.Move:
                                MakeMove(location.X, location.Y);
                                break;
                            case Moves.Passant:
                                MakeMove(location.X, location.Y);
                                (_field[location.Y, location.X] as Pawn).IsUnPassant = true;
                                break;
                            case Moves.UnPassant:
                                UnPassant(location.X, location.Y);
                                break;
                        }
                        _field.Turn = !_field.Turn;
                        _field.DeclareCheckIfPossibleOrRemoveIt();
                        Resaults resaults = _field.EndGame();
                        switch (resaults)
                        { 
                            case Resaults.Checkmate:
                                MessageBox.Show($"{(_field.Turn ? "Black " : "White ")} have won!");
                                Close();
                                break;
                            case Resaults.Stalemate:
                                MessageBox.Show("Draw!");
                                Close();
                                break;
                        }

                    }
                    _isSelected = false;
                    foreach (PictureBox item in _field)
                    {
                        if (item.BackColor == Color.Red || item.BackColor == Color.Yellow || item.BackColor == Color.LimeGreen || item.BackColor == Color.LightGreen)
                            item.BackColor = Color.White;
                        else if (item.BackColor == Color.Brown || item.BackColor == Color.Orange || item.BackColor == Color.Green || item.BackColor == Color.DarkGreen)
                            item.BackColor = Color.Black;
                    }
                }
            }
        }
        private void MakeMove(int x, int y)
        {
            if (_field[_selected.Y, _selected.X] is Pawn pawn1)
            {
                if (!pawn1.HasMovedOnce)
                    pawn1.HasMovedOnce = true;
                else if (pawn1.IsUnPassant)
                    pawn1.IsUnPassant = false;
            }

            (_field[_selected.Y, _selected.X] as Piece).CurrentCordinates = new Point() { X = x, Y = y };

            Color tempColor = _field[y, x].BackColor;
            _field[y, x].BackColor = _field[_selected.Y, _selected.X].BackColor;
            _field[_selected.Y, _selected.X].BackColor = tempColor;

            Point tempPoint = _field[_selected.Y, _selected.X].Location;
            _field[_selected.Y, _selected.X].Location = _field[y, x].Location;
            _field[y, x].Location = tempPoint;

            PictureBox tempSquare = _field[_selected.Y, _selected.X];
            _field[_selected.Y, _selected.X] = _field[y, x];
            _field[y, x] = tempSquare;

            if ((y == 7 || y == 0) && _field[y, x] is Pawn pawn2)
            {
                _pieces.Controls.Remove(pawn2);
                Piece newPiece = null;
                switch (pawn2.Transform())
                {
                    case Transformations.Queen:
                        newPiece = new Queen(pawn2.PieceColor, pawn2.CurrentCordinates);
                        break;
                    case Transformations.Knight:
                        newPiece = new Knight(pawn2.PieceColor, pawn2.CurrentCordinates);
                        break;
                    case Transformations.Castle:
                        newPiece = new Castle(pawn2.PieceColor, pawn2.CurrentCordinates);
                        break;
                    case Transformations.Bishop:
                        newPiece = new Bishop(pawn2.PieceColor, pawn2.CurrentCordinates);
                        break;
                }
                _field[y, x] = newPiece;
                AddEvents(newPiece);
                _pieces.Controls.Add(newPiece);
            }
        }

        private void Capture(int x, int y)
        {
            PictureBox pictureBox = new()
            {
                Location = _field[y, x].Location,
                Size = _field[y, x].Size,
                BackColor = _field[y, x].BackColor,
            };
            AddEvents(pictureBox);
            _pieces.Controls.Remove(_field[y, x]);
            _field[y, x] = pictureBox;
            _pieces.Controls.Add(pictureBox);
        }
        private void UnPassant(int x, int y)
        {
            int direction = (_field[_selected.Y, _selected.X] as Pawn).PieceColor ? 1 : -1;
            Capture(x, y + direction);
            MakeMove(x, y);
        }
        private void AddEvents()
        {
            foreach (PictureBox squere in _field)
                AddEvents(squere);
        }
        private void AddEvents(Control control)
        {
            control.MouseEnter += OnMouseEnterTheSquere;
            control.MouseLeave += OnMouseLeaveTheSquere;
            control.MouseClick += OnMouseClickTheSquere;
        }
        
    }
}