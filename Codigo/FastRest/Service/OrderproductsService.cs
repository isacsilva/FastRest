using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderproductsService : IOrderproductsService
    {
        private readonly FastRestContext _context;

        public OrderproductsService(FastRestContext context)
        {
            _context = context;
        }


        /// <summary>
		/// Atualiza os dados dos orderProducts na base de dados
		/// </summary>
		/// <param name="orderProducts">dados do orderProducts</param>

        public void Editar(Orderproducts orderProducts)
        {
            //throw new NotImplementedException();
            _context.Update(orderProducts);
            _context.SaveChanges();
        }

        /// <summary>
		/// Consulta genérica aos dados do orderProducts
		/// </summary>
		/// <returns></returns>
		private IQueryable<Orderproducts> GetQuery()
        {
            IQueryable<Orderproducts> tb_orderProducts = _context.Orderproducts;
            var query = from orderProducts in tb_orderProducts
                        select orderProducts;
            return query;
        }

        /// <summary>
        /// Obtém todos os orderProducts
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Orderproducts> ObterTodos()
        {
            return _context.Orderproducts.AsNoTracking();
        }
        /// <summary>
        /// Retorna o número de orderProducts cadastrados
        /// </summary>
        /// <returns></returns>
        public int GetNumeroOrderProducts()
        {
            return _context.Orderproducts.Count();
        }

        /// <summary>
		/// Obtém pelo identificado do orderProduct
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public Orderproducts Obter(int idOrderproducts)
        {
            IEnumerable<Orderproducts> orderProducts = GetQuery().Where(OrderproductsModel => OrderproductsModel.Id.Equals(idOrderproducts));

            return orderProducts.ElementAtOrDefault(0);
        }

        /// <summary>
        /// Insere um novo orderProduct na base de dados
        /// </summary>
        /// <param name="orderProducts">dados do orderProduct</param>
        /// <returns></returns>
        public int Inserir(Orderproducts orderProduct)
        {

            _context.Add(orderProduct);
            _context.SaveChanges();
            return orderProduct.Id;

        }

        /// <summary>
		/// Remover produto pelo id
		/// </summary>
		/// <returns></returns>
        public void Remover(int idOrderproducts)
        {
            var __produto = _context.Product.Find(idOrderproducts);
            _context.Product.Remove(__produto);
            _context.SaveChanges();
        }
    }
}
