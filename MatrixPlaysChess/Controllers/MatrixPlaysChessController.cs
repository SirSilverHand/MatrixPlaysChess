using MatrixPlaysChess.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MatrixPlaysChess.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatrixPlaysChessController : ControllerBase
    {
        private readonly IOptions<MPCAppSettings> _configuration;

        public MatrixPlaysChessController(IOptions<MPCAppSettings> configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetPhoneNumbers()
        {
            var query = new MatrixPlaysChessQuery(_configuration);

            await Task.Run(() => query.RunQuery());

            return Ok(query.PieceCounts);
        }
    }
}