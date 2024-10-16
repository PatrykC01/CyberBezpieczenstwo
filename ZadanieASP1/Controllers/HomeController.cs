using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ZadanieASP1.Models;
using ZadanieASP1.ViewModel;

namespace ZadanieASP1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IEnumerable<TripDetailViewModel> _trips = new List<TripDetailViewModel>
        {
            new TripDetailViewModel
            {
                Id = 1,
                Title = "Delfiny i snorkling przy wyspie Mnemba, żółwie wodne i zachód słońca",
                Description = "Jest to wycieczka, na której zobaczymy wyspę Mnemba. Będziemy mieli okazję nurkować z rurką  przy najładniejszych rafach w pobliżu Zanzibaru. Zatrzymamy się przy słynnym sand banku – jest to  znikająca piękna plaża na oceanie. Przy odrobinie szczęścia zobaczymy delfiny i będziemy mieli okazję z nimi popływać. Następnie możemy pływać wśród żółwi wodnych i je karmić, wycieczka kończy się zachodem słońca na plaży Kendwa Rock.",
                URLImg = "https://www.wycieczkionline.com/upload/images/06-62ef9103e7222135239239.jpg",
                Price = 289.00f,
                Date = new DateOnly(2024, 3, 2)
            },
            new TripDetailViewModel
            {
                Id = 2,
                Title = "Zanzibar w pigułce - Kamienne miasto, Wyspa Więzienna i Farma przypraw",
                Description = "Całodniowa niesamowita wycieczka po największych atrakcjach wyspy Zanzibar. Wycieczkę rozpoczynamy ok. 8 rano, jedziemy busem do jednej z farm przypraw, z których słynie wyspa na całym świecie. Będziecie mieli okazję na własne oczy zobaczyć dojrzewające w słońcu ziarna kawy, pieprzu, kardamonu czy dotknąć świeżej kory cynamonu. Zobaczycie jak rosną i jak smakują świeżo zerwane owoce takie jak karambola, ananas, durian, mango czy papaja. Na plantacji będzie również możliwość zakupu świeżych przypraw. Obaczymy lokalny dom i zjemy tam domowy obiad aby poznać prawdziwą kuchnię Zanzibaru. Następnie wybierzemy się do stolicy wyspy - Stone Town nazywane też Kamiennym Miastem. Tam popłyniemy na ok. 20 minutowy rejs łodzią na wyspę więzienną – Prison Island. Aktualnie na wyspie możemy spotkać gigantyczne żółwie Aldabra.",
                URLImg = "https://www.wycieczkionline.com/upload/images/13-62eb71bb253e2071476482.jpg",
                Price = 295.00f,
                Date = new DateOnly(2024, 3, 7)
            },
            new TripDetailViewModel
            {
                Id = 3,
                Title = "Quady na Zanzibarze z Kiwengwa",
                Description = "Jest to wspaniała wyprawa Quadami w głąb wyspy gdzie zobaczymy prawdziwe życie lokalnych mieszkańców, odwiedzimy farmę przypraw, malowniczą lokalną wioskę, przejedziemy przez pola ryżowe i dojedziemy do wspaniałej plaży gdzie będzie można się zrelaksować.\r\n\r\nStartujemy z Kiwengwa i jedziemy w stronę Kiniasini. Powoli jedziemy główną drogą między domami rybackimi i lokalnymi atrakcjami aż do Pwani Mchangani, gdzie będzie nasz pierwszy przystanek na głównym placu. Tam można kupić prezenty dla lokalnej społeczności (np. ryż, jedzenie lub przybory szkolne).",
                URLImg = "https://www.wycieczkionline.com/upload/images/04quadzanzibar-62ef9570c87eb984781676.jpg",
                Price = 649.00f,
                Date = new DateOnly(2024, 6, 15)
            },
        };
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_trips);
        }

        [HttpPost]
        public IActionResult ChangeLanguage(string language)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(language)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Ok();
        }

        public IActionResult Detail(int id)
        {
            var trip = _trips
                .FirstOrDefault(_trips => _trips.Id == id);
            return View(trip);
        }
        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}