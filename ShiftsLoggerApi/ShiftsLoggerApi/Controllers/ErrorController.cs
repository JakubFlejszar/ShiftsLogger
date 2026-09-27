using Microsoft.AspNetCore.Diagnostics; // Udostępnia IExceptionHandlerPathFeature do odczytania obsługiwanego wyjątku.
using Microsoft.AspNetCore.Mvc; // Udostępnia klasy potrzebne do tworzenia kontrolera i odpowiedzi HTTP.

namespace ShiftsLoggerApi.Controllers
{
    [ApiController] // Oznacza klasę jako kontroler Web API.
    [Route("/error")] // Ustawia adres tego kontrolera na /error.
    public class ErrorController : ControllerBase // Tworzy kontroler odpowiedzialny za obsługę błędów.
    {
        private readonly ILogger<ErrorController> logger; // Pole przechowujące logger dla tego kontrolera.

        public ErrorController(ILogger<ErrorController> loggerAssign) // Konstruktor, przez który DI przekazuje logger.
        {
            logger = loggerAssign; // Przypisuje otrzymany logger do pola klasy.
        }

        [ApiExplorerSettings(IgnoreApi = true)] // Ukrywa ten endpoint przed Swaggerem.
        public IActionResult Error(IExceptionHandlerPathFeature exception) // Odbiera informacje o wyjątku przechwyconym przez ASP.NET Core.
        {
            logger.LogError(exception.Error, "Unhandled exception occurred"); // Zapisuje wyjątek wraz ze szczegółami w logach aplikacji.

            return StatusCode(500, "Something went wrong"); // Zwraca klientowi HTTP 500 bez ujawniania szczegółów wyjątku.
        }
    }
}