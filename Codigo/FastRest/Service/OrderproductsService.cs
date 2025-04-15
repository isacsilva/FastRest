using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class OrderproductsService : IOrderproductsService
    {
        private readonly FastRestContext _context;

        public OrderproductsService(FastRestContext context)
        {
            _context = context;
        }

        public void Editar(Orderproducts orderProducts)
        {
            _context.Update(orderProducts);
            _context.SaveChanges();
        }

        private IQueryable<Orderproducts> GetQuery()
        {
            return _context.Orderproducts.Include(op => op.Product); // Aqui é essencial
        }

        public IEnumerable<Orderproducts> ObterTodos()
        {
            return GetQuery().AsNoTracking(); // Assim o Product é incluído também
        }

        public int GetNumeroOrderProducts()
        {
            return _context.Orderproducts.Count();
        }

        public Orderproducts Obter(int idOrderproducts)
        {
            return GetQuery().FirstOrDefault(op => op.Id == idOrderproducts);
        }

        public int Inserir(Orderproducts orderProduct)
        {
            _context.Add(orderProduct);
            _context.SaveChanges();
            return orderProduct.Id;
        }

        public void Remover(int idOrderproducts)
        {
            var orderProduct = _context.Orderproducts.Find(idOrderproducts);
            if (orderProduct != null)
            {
                _context.Orderproducts.Remove(orderProduct);
                _context.SaveChanges();
            }
        }

        public void AtualizarTotalPedido(int idPedido, decimal total)
        {
            var order = _context.Ordertable.Find(idPedido);
            if (order != null)
            {
                order.Total = total;
                _context.SaveChanges();
            }
        }
    }
}