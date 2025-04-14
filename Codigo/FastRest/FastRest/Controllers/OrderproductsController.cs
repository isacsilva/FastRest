using AutoMapper;
using Core;
using Core.Service;
using FastRest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace FastRest.Controllers
{
    public class OrderproductsController : Controller
    {
        IOrderproductsService _orderproductsService;
        IOrdertableService _ordertableService;
        IMapper _mapper;

        public OrderproductsController(IOrderproductsService orderproductsService,IMapper mapper)
        {
            _orderproductsService = orderproductsService;
            _mapper = mapper;
        }
        // GET: OrderproductsController
        public ActionResult Index()
        {
            var listaOrderproducts = _orderproductsService.ObterTodos();
            var listaOrderproductsModel = _mapper.Map<List<OrderproductsModel>>(listaOrderproducts);
            return View(listaOrderproductsModel);
        }

        // GET: OrderproductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderproductsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderproductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderproductsModel orderproductsModel)
        {
            if (ModelState.IsValid)
            {
                var orderproducts = _mapper.Map<Orderproducts>(orderproductsModel);
                _orderproductsService.Inserir(orderproducts);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: OrderproductsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderproductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OrderproductsModel orderproductsModel)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderproductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderproductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, OrderproductsModel orderproductsModel)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
