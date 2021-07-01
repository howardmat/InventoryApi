using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class ErrorController : InventoryControllerBase
    {
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
