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
        private readonly IOrderproductsService _orderproductsService;
        private readonly IMapper _mapper;

        public OrderproductsController(IOrderproductsService orderproductsService, IMapper mapper)
        {
            _orderproductsService = orderproductsService;
            _mapper = mapper;
        }

        // GET: OrderproductsController
        public IActionResult Index()
        {
            var listaOrderproducts = _orderproductsService.ObterTodos();
            var listaOrderproductsModel = _mapper.Map<List<OrderproductsModel>>(listaOrderproducts);
            return View(listaOrderproductsModel);
        }

        // GET: OrderproductsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderproductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderproductsModel orderproductsModel)
        {
            if (ModelState.IsValid)
            {
                var orderproducts = _mapper.Map<Orderproducts>(orderproductsModel);
                _orderproductsService.Inserir(orderproducts);
                return RedirectToAction(nameof(Index));
            }

            return View(orderproductsModel);
        }

        // GET: OrderproductsController/Edit/5
        public IActionResult Edit(int id)
        {
            var orderproduct = _orderproductsService.Obter(id);
            if (orderproduct == null)
                return NotFound();

            var model = _mapper.Map<OrderproductsModel>(orderproduct);
            return View(model);
        }

        // POST: OrderproductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, OrderproductsModel orderproductsModel)
        {
            if (ModelState.IsValid)
            {
                var orderproducts = _mapper.Map<Orderproducts>(orderproductsModel);
                orderproducts.Id = id;
                _orderproductsService.Editar(orderproducts);
                return RedirectToAction(nameof(Index));
            }

            return View(orderproductsModel);
        }

        // GET: OrderproductsController/Delete/5
        public IActionResult Delete(int id)
        {
            var orderproduct = _orderproductsService.Obter(id);
            if (orderproduct == null)
                return NotFound();

            var model = _mapper.Map<OrderproductsModel>(orderproduct);
            return View(model);
        }

        // POST: OrderproductsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _orderproductsService.Remover(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
