using Microsoft.AspNetCore.Mvc;
using Data.Entities.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILogger<HomeController> _logger;


    }
}
