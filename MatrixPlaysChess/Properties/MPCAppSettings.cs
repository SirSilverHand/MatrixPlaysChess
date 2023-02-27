using MatrixPlaysChess.Models;

namespace MatrixPlaysChess.Properties
{
    public class MPCAppSettings
    {
        public string[][] Keypad { get; set; }
        public List<ChessPiece> ChessPieces { get; set; }
        public int PhoneNumberLength { get; set; }
    }
}
