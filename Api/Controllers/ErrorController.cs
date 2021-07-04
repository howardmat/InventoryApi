using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : InventoryControllerBase
    {
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
