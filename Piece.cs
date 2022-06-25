namespace Chess
{
    public abstract class Piece : PictureBox
    {
        public Piece(bool color, Point cordinates)
        {
            Location = new Point((cordinates.X * 60) + 10, (cordinates.Y * 60) + 10);
            CurrentCordinates = cordinates;
            PieceColor = color;
            Size = new Size(60, 60);
            BackColor =
                CurrentCordinates.X % 2 == 0 && CurrentCordinates.Y % 2 == 0 ? Color.White :
                CurrentCordinates.X % 2 != 0 && CurrentCordinates.Y % 2 != 0 ? Color.White : Color.Black;

        }
        public Point CurrentCordinates { get; set; }
        public bool PieceColor { get; private set; }
        protected readonly int[,] BishopPath = new int[,] { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } };
        protected readonly int[,] CastlePath = new int[,] { { 0, 1 }, { 1, 0 }, { -1, 0 }, { 0, -1 } };
        public abstract LinkedList<(Point, Moves)>? BuildPath(Field field);

        protected LinkedList<(Point, Moves)>? BuildPath(Field field, int[,] path)
        {
            LinkedList<(Point, Moves)>? result = new();
            for (int i = 0; i < path.GetLength(0); i++)
            {
                Point current = CurrentCordinates;
                while (true)
                {
                    if (!(current.X + path[i, 0] >= 0 && current.X + path[i, 0] <= 7 && current.Y + path[i, 1] >= 0 && current.Y + path[i, 1] <= 7))
                        break;

                    current.X += path[i, 0];
                    current.Y += path[i, 1];

                    if (field[current.Y, current.X] is Piece piece)
                    {
                        if (piece.PieceColor != PieceColor)
                            result.AddLast((current, Moves.Capture));
                        break;
                    }
                    else
                        result.AddLast((current, Moves.Move));
                }
            }
            return result;
        }
    }

    public class Pawn : Piece
    {
        public bool HasMovedOnce { get; set; } = false;
        public bool IsUnPassant { get; set; } = false;
        
        public Pawn(bool color, Point cordinates) : base(color, cordinates) => Image = Image.FromFile(@"images\" + (PieceColor ? "White " : "Black ") + "Pawn.png");
        
        public override LinkedList<(Point, Moves)>? BuildPath(Field field)
        {
            LinkedList<(Point, Moves)>? result = new();
            int direction = PieceColor ? -1 : 1;
            Point current = new Point() { X = CurrentCordinates.X, Y = CurrentCordinates.Y + direction };
            if (field[current.Y, current.X] is not Piece)
            {
                result.AddLast((current, Moves.Move));
                if (!HasMovedOnce && field[CurrentCordinates.Y + (direction * 2), current.X] is not Piece)
                {
                    current.Y = CurrentCordinates.Y + (direction * 2);
                    result.AddLast((current, Moves.Passant));
                }
            }
            current.X++;
            current.Y = CurrentCordinates.Y + direction;
            for (int i = 0; i < 2; i++)
            {
                if (current.X <= 7 && current.X >= 0 && field[current.Y, current.X] is Piece
                    && (field[current.Y, current.X] as Piece).PieceColor != PieceColor)
                    result.AddLast((current, Moves.Capture));
                current.X -= 2;
            }
            return new LinkedList<(Point, Moves)>(result.Concat(UnPassant(field)));
        }

        public LinkedList<(Point, Moves)> UnPassant(Field field)
        {
            LinkedList<(Point, Moves)> result = new();
            if (CurrentCordinates.X + 1 <= 7 && field[CurrentCordinates.Y, CurrentCordinates.X + 1] is Pawn pawn1 && pawn1.PieceColor != PieceColor && pawn1.IsUnPassant)
                result.AddLast((new Point() { X = CurrentCordinates.X + 1, Y = CurrentCordinates.Y + (PieceColor ? -1 : 1) }, Moves.UnPassant));
            if (CurrentCordinates.X - 1 >= 0 && field[CurrentCordinates.Y, CurrentCordinates.X - 1] is Pawn pawn2 && pawn2.PieceColor != PieceColor && pawn2.IsUnPassant)
                result.AddLast((new Point() { X = CurrentCordinates.X - 1, Y = CurrentCordinates.Y + (PieceColor ? -1 : 1) }, Moves.UnPassant));
            return result;
        }

        public Transformations Transform()
        {
            PiecePicker resault = new();
            resault.ShowDialog();
            return resault.Transformations;
        }
    }

    public class Bishop : Piece
    {
        public Bishop(bool color, Point cordinates) : base(color, cordinates) => Image = Image.FromFile(@"images\" + (PieceColor ? "White " : "Black ") + "Bishop.png");
        
        public override LinkedList<(Point, Moves)>? BuildPath(Field field) => BuildPath(field, BishopPath);
    }

    public class Castle : Piece
    {
        public Castle(bool color, Point cordinates) : base(color, cordinates) => Image = Image.FromFile(@"images\" + (PieceColor ? "White " : "Black ") + "Castle.png");
        
        public override LinkedList<(Point, Moves)>? BuildPath(Field field) => BuildPath(field, CastlePath);
    }

    public class Queen : Piece
    {
        public Queen(bool color, Point cordinates) : base(color, cordinates) => Image = Image.FromFile(@"images\" + (PieceColor ? "White " : "Black ") + "Queen.png");
       
        public override LinkedList<(Point, Moves)>? BuildPath(Field field) =>
            new LinkedList<(Point, Moves)>(
                BuildPath(field, BishopPath)
                .Concat(BuildPath(field, CastlePath))
                );
    }

    public class King : Piece
    {
        public bool Check { get; set; } = false;
        
        public King(bool color, Point cordinates) : base(color, cordinates) => Image = Image.FromFile(@"images\" + (PieceColor ? "White " : "Black ") + "King.png");
        
        public override LinkedList<(Point, Moves)>? BuildPath(Field field)
        {
            LinkedList<(Point, Moves)>? resault = new();
            int[,] kingPath = { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 }, { 0, 1 }, { 1, 0 }, { -1, 0 }, { 0, -1 } };
            for (int i = 0; i < kingPath.GetLength(0); i++)
            {
                Point current = new Point() { Y = CurrentCordinates.Y + kingPath[i, 1], X = CurrentCordinates.X + kingPath[i, 0] };
                if (current.X >= 0 && current.X <= 7 && current.Y >= 0 && current.Y <= 7)
                {
                    if (field[current.Y, current.X] is Piece)
                    {
                        if ((field[current.Y, current.X] as Piece).PieceColor != PieceColor && field[current.Y, current.X] is not Queen)
                            resault.AddLast((current, Moves.Capture));
                    }
                    else
                        resault.AddLast((current, Moves.Move));
                }
            }
            return resault;
        }
    }

    public class Knight : Piece
    {
        public Knight(bool color, Point cordinates) : base(color, cordinates) => Image = Image.FromFile(@"images\" + (PieceColor ? "White " : "Black ") + "Knight.png");
        
        public override LinkedList<(Point, Moves)>? BuildPath(Field field)
        {
            LinkedList<(Point, Moves)>? resault = new();
            int[,] knightPath = { { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }, { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 } };
            for (int i = 0; i < knightPath.GetLength(0); i++)
            {
                Point newPoint = new Point() { X = CurrentCordinates.X + knightPath[i, 0], Y = CurrentCordinates.Y + knightPath[i, 1] };
                if (newPoint.X <= 7 && newPoint.X >= 0 && newPoint.Y <= 7 && newPoint.Y >= 0)
                {
                    if (field[newPoint.Y, newPoint.X].Image is not null)
                    {
                        if ((field[newPoint.Y, newPoint.X] as Piece).PieceColor != PieceColor)
                            resault.AddLast((newPoint, Moves.Capture));
                    }
                    else
                        resault.AddLast((newPoint, Moves.Move));
                }
            }
            return resault;
        }
    }
}
