using MatrixPlaysChess.Extension;
using MatrixPlaysChess.Models;
using MatrixPlaysChess.Properties;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Linq;
public class MatrixPlaysChessQuery
{
    private readonly IOptions<MPCAppSettings> _configuration;
    public ConcurrentBag<string> PhoneNumbers = new(); 
    public Dictionary<string, int> PieceCounts = new Dictionary<string, int>();
    public MatrixPlaysChessQuery(IOptions<MPCAppSettings> configuration)
    {
        _configuration = configuration;
    }
    public void RunQuery()
    {
        foreach(var position in GetStartingPositions())
        {
            Parallel.ForEach(_configuration.Value.ChessPieces, chessPiece =>
            {
                action(position.r, position.c, string.Empty, chessPiece);
            });
        };
    }
    private void action(int r, int c, string phoneNumber, ChessPiece chessPiece)
    {
        var cache = new List<string>();
        int digit;
        string key = _configuration.Value.Keypad[r][c];
        if (!int.TryParse((key), out digit))
        {
            //Should the piece e.g Knight be allowed to get on "*", and move on not storing the "*" in the number?
            return;
        }

        if (phoneNumber != string.Empty && (phoneNumber[0] == '0' || phoneNumber[0] == '1'))
        {
            return;
        }

        if (!PieceCounts.ContainsKey(chessPiece.Type))
            PieceCounts.Add(chessPiece.Type, 0);
        foreach (var move in chessPiece.Moves)
        {
            (int newRow, int newCol) = (r + move[0], c + move[1]);
            string newPhoneNumber = string.Empty;
            newPhoneNumber = phoneNumber + digit;

            if (((newRow, newCol), newPhoneNumber, _configuration).IfValid(p => action(p.position.r, p.position.c, p.phoneNumber, chessPiece)))
            {
                PhoneNumbers.Add(newPhoneNumber);
                PieceCounts[chessPiece.Type]++;
                return;
            }
        }
    }

    private IEnumerable<(int r, int c)> GetStartingPositions()
    {
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (r < 3 || c == 1)
                {
                    yield return (r, c);
                }
            }
        }
    }
}