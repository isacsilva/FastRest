using AutoMapper;
using Core;
using Core.Service;
using FastRest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FastRest.Controllers
{
    public class HomeController : Controller
    {
        IOrdertableService _ordertableService;
        IMapper _mapper;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IOrdertableService ordertableService, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _ordertableService = ordertableService;
        }

        public IActionResult Index()
        {
            return View();
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

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrdertableModel ordertableModel)
        {
            // Preenche os valores que não vêm da View
            ordertableModel.Total = 0;
            ordertableModel.Status = "PENDING";
            ordertableModel.RestaurantId = 1;

            // Limpa possíveis validações anteriores
            ModelState.Clear();
            TryValidateModel(ordertableModel);

            if (ModelState.IsValid)
            {
                var ordertable = _mapper.Map<Ordertable>(ordertableModel);
                int id = _ordertableService.Inserir(ordertable);

                // Redireciona para a página de produtos com o idPedido pela URL
                return RedirectToAction("Index", "Product", new { idPedido = id });
            }

            return RedirectToAction("Index", "Home");
        }
    }
}