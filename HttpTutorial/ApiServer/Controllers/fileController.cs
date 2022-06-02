using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class filesdController : ControllerBase
    {
        private readonly InAppfileServer inAppfileServer;

        public filesdController(InAppfileServer inAppfileServer )
        {
            this.inAppfileServer = inAppfileServer;
        }
        [HttpPost("savefile")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] IFormFile file )
        {
            await inAppfileServer.Save(file, "test");
            return Ok();
        }
    }
}
