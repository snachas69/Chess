using System.Collections;

namespace Chess
{
    public class Field : IEnumerable<PictureBox>
    {
        private PictureBox[,] _field = new PictureBox[8, 8];
        public bool Turn { get; set; } = true;

        public Field()
        {
            _field[0, 0] = new Castle(false, new Point() { X = 0, Y = 0 }); _field[0, 7] = new Castle(false, new Point() { X = 7, Y = 0 });
            _field[0, 1] = new Knight(false, new Point() { X = 1, Y = 0 }); _field[0, 6] = new Knight(false, new Point() { X = 6, Y = 0 });
            _field[0, 2] = new Bishop(false, new Point() { X = 2, Y = 0 }); _field[0, 5] = new Bishop(false, new Point() { X = 5, Y = 0 });
            _field[0, 3] = new Queen(false, new Point() { X = 3, Y = 0 }); _field[0, 4] = new King(false, new Point() { X = 4, Y = 0 });

            for (int i = 0; i < 8; i++)
                _field[1, i] = new Pawn(false, new Point() { X = i, Y = 1 });

            for (int i = 2; i < 6; i++)
                for (int j = 0; j < 8; j++)
                    _field[i, j] = new PictureBox()
                    {
                        Location = new Point((j * 60) + 10, (i * 60) + 10),
                        Size = new Size(60, 60),
                        BackColor =
                            i % 2 == 0 && j % 2 == 0 ? Color.White :
                            i % 2 != 0 && j % 2 != 0 ? Color.White : Color.Black,
                    };

            for (int i = 0; i < 8; i++)
                _field[6, i] = new Pawn(true, new Point() { X = i, Y = 6 });

            _field[7, 3] = new Queen(true, new Point() { X = 3, Y = 7 }); _field[7, 4] = new King(true, new Point() { X = 4, Y = 7 });
            _field[7, 2] = new Bishop(true, new Point() { X = 2, Y = 7 }); _field[7, 5] = new Bishop(true, new Point() { X = 5, Y = 7 });
            _field[7, 1] = new Knight(true, new Point() { X = 1, Y = 7 }); _field[7, 6] = new Knight(true, new Point() { X = 6, Y = 7 });
            _field[7, 0] = new Castle(true, new Point() { X = 0, Y = 7 }); _field[7, 7] = new Castle(true, new Point() { X = 7, Y = 7 });
        }

        public PictureBox this[int indexY, int indexX]
        {
            get => _field[indexY, indexX];
            set => _field[indexY, indexX] = value;
        }

        public Resaults EndGame()
        {
            King king = _field.OfType<King>().Where(k => k.PieceColor == Turn).First();
            bool isCheck = king.Check;
            LinkedList<Point> possibleChecks = PossibleChecks(king);
            bool isThereNoEscape = king
                                   ?.BuildPath(this)
                                   ?.Select(i => i.Item1)
                                   ?.Where(i => !possibleChecks.Contains(i))
                                   ?.Count() == 0;
            if (isCheck && isThereNoEscape)
                return Resaults.Checkmate;
            else if (!isCheck && isThereNoEscape)
                if (!_field
                    .OfType<Piece>()
                    .Where(p => p.PieceColor == Turn)
                    .Select(p => p.BuildPath(this).Count > 0)
                    .Any(i => i)) return Resaults.Stalemate;
            return Resaults.GameContinues;
        }

        public LinkedList<Point> PossibleChecks(Piece piece)
        {
            LinkedList<Point> possibleChecks = new();
            foreach (Point point in piece.BuildPath(this).Select(i => i.Item1))
                if (IsCheck(piece.CurrentCordinates, point))
                    possibleChecks.AddLast(point);
            return possibleChecks;
        }

        private bool IsCheck(Point from, Point to)
        {
            PictureBox piece = _field[to.Y, to.X];

            (_field[from.Y, from.X] as Piece).CurrentCordinates = to;

            PictureBox tempPB = _field[to.Y, to.X];
            _field[to.Y, to.X] = _field[from.Y, from.X];
            _field[from.Y, from.X] = tempPB;

            bool isCheck = ScanField();

            tempPB = _field[to.Y, to.X];
            _field[to.Y, to.X] = _field[from.Y, from.X];
            _field[from.Y, from.X] = tempPB;

            if (piece is not null)
                _field[to.Y, to.X] = piece;

            (_field[from.Y, from.X] as Piece).CurrentCordinates = from;

            return isCheck;
        }

        private bool ScanField() =>
            _field
                .OfType<Piece>()
                .Where(p => p.PieceColor != Turn)
                .ToList()
                .Select(i => i.BuildPath(this)
                              .Select(p => p.Item1)
                              .Contains(_field
                                        .OfType<King>()
                                        .Where(k => k.PieceColor == Turn)
                                        .First().CurrentCordinates)
                        )
            .Any(i => i);

        public void DeclareCheckIfPossibleOrRemoveIt()
        {
            King? king = _field
                ?.OfType<King>()
                ?.Where(k => k.PieceColor == Turn)
                ?.FirstOrDefault();
            if (king is not null)
                king.Check = ScanField();
        }

        public IEnumerator GetEnumerator() => _field.GetEnumerator();

        IEnumerator<PictureBox> IEnumerable<PictureBox>.GetEnumerator() => GetEnumerator() as IEnumerator<PictureBox>;
    }
}
