using AutoMapper;
using Core;
using Core.Service;
using FastRest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace FastRest.Controllers
{
    public class ProductController : Controller
    {
        IOrderproductsService _orderproductsService;
        IProductService _produtoService;
        IMapper _mapper;

        public ProductController(IProductService produtoService, IOrderproductsService orderproductsService, IMapper mapper)
        {
            _orderproductsService = orderproductsService;
            _produtoService = produtoService;
            _mapper = mapper;
        }
        // GET: ProductController
        public ActionResult Index(int idPedido)
        {
            ViewBag.IdPedido = idPedido;

            var listaProdutos = _produtoService.ObterTodos();
            var listaProdutosModel = _mapper.Map<List<ProductModel>>(listaProdutos);
            return View(listaProdutosModel);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            Product produto = _produtoService.Obter(id);
            ProductModel produtoModel = _mapper.Map<ProductModel>(produto);
            return View(produtoModel);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel produtoModel)
        {
            if (ModelState.IsValid)
            {
                var produto = _mapper.Map<Product>(produtoModel);
                _produtoService.Inserir(produto);
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult CreateSolicitacaoProduto()
        {
            return View();
        }

        // POST: ProductController/CreateSolicitacaoProduto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSolicitacaoProduto(ProductModel produtoModel)
        {
            if (ModelState.IsValid)
            {
                var produto = _mapper.Map<Product>(produtoModel);
                _produtoService.Inserir(produto);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            Product produto = _produtoService.Obter(id);
            ProductModel produtoModel = _mapper.Map<ProductModel>(produto);
            return View(produtoModel);

        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductModel produtoModel)
        {
            if (ModelState.IsValid)
            {
                var produto = _mapper.Map<Product>(produtoModel);
                _produtoService.Editar(produto);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Painel()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToOrder(int idPedido, int idProduto, int quantidade, decimal preco)
        {
            if (idPedido <= 0 || idProduto <= 0 || quantidade <= 0 || preco <= 0)
                return BadRequest("Dados inválidos");

            var novoItem = new Orderproducts
            {
                OrderId = idPedido,
                ProductId = idProduto,
                Quantity = quantidade,
                Price = preco
            };

            _orderproductsService.Inserir(novoItem);

            return Ok("Produto adicionado ao pedido!");
        }

    }
}
