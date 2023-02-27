using MatrixPlaysChess.Models;
using MatrixPlaysChess.Properties;
using Microsoft.Extensions.Options;

namespace MatrixPlaysChess.Extension
{
    static class IfValidExtension
    {
        public static bool IfValid(this ((int r, int c) position, string phoneNumber, IOptions<MPCAppSettings> config) args, Action<((int r, int c) position, string phoneNumber)> action)
        {
            int r = args.position.r;
            int c = args.position.c;
            string phoneNumber = args.phoneNumber;

            if (phoneNumber.Count() < args.config.Value.PhoneNumberLength)
            {
                if (r >= 0 && r < 4 && c >= 0 && c < 3)
                {
                    action((args.position, args.phoneNumber));
                }
                return false;
            }
            else return true;
        }
    }
}
