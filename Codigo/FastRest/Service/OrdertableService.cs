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
    public class OrdertableService : IOrdertableService
    {
        private readonly FastRestContext _context;

        public OrdertableService(FastRestContext context)
        {
            _context = context;
        }


        /// <summary>
		/// Atualiza os dados dos produtos na base de dados
		/// </summary>
		/// <param name="produto">dados do produto</param>

        public void Editar(Ordertable produto)
        {
            //throw new NotImplementedException();
            _context.Update(produto);
            _context.SaveChanges();
        }

        /// <summary>
		/// Consulta genérica aos dados do produto
		/// </summary>
		/// <returns></returns>
		private IQueryable<Ordertable> GetQuery()
        {
            IQueryable<Ordertable> tb_produto = _context.Ordertable;
            var query = from produto in tb_produto
                        select produto;
            return query;
        }

        /// <summary>
        /// Obtém todos os produtos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Ordertable> ObterTodos()
        {
            return _context.Ordertable.AsNoTracking();
        }
        /// <summary>
        /// Retorna o número de produtos cadastrados
        /// </summary>
        /// <returns></returns>
        public int GetNumeroProdutos()
        {
            return _context.Ordertable.Count();
        }

        /// <summary>
		/// Obtém pelo identificado do produto
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public Ordertable Obter(int idOrdertable)
        {
            IEnumerable<Ordertable> produtos = GetQuery().Where(OrdertableModel => OrdertableModel.Id.Equals(idOrdertable));

            return produtos.ElementAtOrDefault(0);
        }

        /// <summary>
        /// Insere um novo produto na base de dados
        /// </summary>
        /// <param name="produto">dados da produto</param>
        /// <returns></returns>
        public int Inserir(Ordertable produto)
        {

            _context.Add(produto);
            _context.SaveChanges();
            return produto.Id;

        }

        /// <summary>
		/// Remover produto pelo id
		/// </summary>
		/// <returns></returns>
        public void Remover(int idOrdertable)
        {
            var __produto = _context.Product.Find(idOrdertable);
            _context.Product.Remove(__produto);
            _context.SaveChanges();
        }
        public void AtualizarTotalPedido(int idOrdertable)
        {
            var orderProducts = _context.Orderproducts
                                        .Where(op => op.OrderId == idOrdertable)
                                        .ToList();

            decimal total = 0;
            foreach (var orderProduct in orderProducts)
            {
                total += orderProduct.Quantity * orderProduct.Price;
            }

            var orderTable = _context.Ordertable.Find(idOrdertable);
            if (orderTable != null)
            {
                orderTable.Total = total;
                _context.SaveChanges();
            }
        }

        public void AtualizarStatus(int idOrdertable, string newStatus)
        {
            var pedido = _context.Ordertable.Find(idOrdertable); // busca pelo ID
            if (pedido != null)
            {
                pedido.Status = newStatus; // atualiza o status
                _context.Update(pedido);   // marca como modificado
                _context.SaveChanges();    // salva as alterações no banco
            }
            else
            {
                Console.WriteLine($"[OrdertableService] Pedido com ID {idOrdertable} não encontrado.");
            }
        }
    }
}
