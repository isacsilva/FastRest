using AutoMapper;
using Core;
using Core.Service;
using FastRest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastRest.Controllers
{
    public class OrdertableController : Controller
    {
        IOrdertableService _ordertableService;
        IMapper _mapper;

        public OrdertableController(IOrdertableService ordertableService, IMapper mapper)
        {
            _ordertableService = ordertableService;
            _mapper = mapper;
        }

        // GET: OrdertableController
        public ActionResult Index()
        {
            var listaOrdertable = _ordertableService.ObterTodos();
            var listaOrdertableModel = _mapper.Map<List<OrdertableModel>>(listaOrdertable);
            return View(listaOrdertableModel);
        }

        // GET: OrdertableController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrdertableController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrdertableController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrdertableModel ordertableModel)
        {
            if (ModelState.IsValid)
            {
                var ordertable = _mapper.Map<Ordertable>(ordertableModel);
                _ordertableService.Inserir(ordertable);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: OrdertableController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrdertableController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: OrdertableController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrdertableController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
