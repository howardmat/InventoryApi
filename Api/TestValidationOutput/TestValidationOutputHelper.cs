using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.TestValidationOutput
{
    public class TestValidationOutputHelper
    {
        public static IActionResult GluwaInvalidModelStateResponseFactory(ActionContext context)
        {


            return new OkObjectResult("Hai");
        }
    }
}
