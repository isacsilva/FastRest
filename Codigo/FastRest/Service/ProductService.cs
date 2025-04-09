using Core;
using Core.DTO;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly FastRestContext _context;

        public ProductService(FastRestContext context)
        {
            _context = context;
        }


        /// <summary>
		/// Atualiza os dados dos produtos na base de dados
		/// </summary>
		/// <param name="produto">dados do produto</param>

        public void Editar(Product produto)
        {
            //throw new NotImplementedException();
            _context.Update(produto);
            _context.SaveChanges();
        }

        /// <summary>
		/// Consulta genérica aos dados do produto
		/// </summary>
		/// <returns></returns>
		private IQueryable<Product> GetQuery()
        {
            IQueryable<Product> tb_produto = _context.Product;
            var query = from produto in tb_produto
                        select produto;
            return query;
        }

        /// <summary>
        /// Obtém todos os produtos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> ObterTodos()
        {
            return _context.Product.AsNoTracking();
        }
        /// <summary>
        /// Retorna o número de produtos cadastrados
        /// </summary>
        /// <returns></returns>
        public int GetNumeroProdutos()
        {
            return _context.Product.Count();
        }

        /// <summary>
        /// Obtém os dados do primeiro produto cadastrado na base de dados.
        /// </summary>
        /// <returns></returns>
        public Product ObterPrimeiroProduto()
        {
            Product _tbproduto = _context.Product.FirstOrDefault();
            Product produto = new Product();
            if (_tbproduto != null)
            {
                produto.Id = _tbproduto.Id;
                produto.Name = _tbproduto.Name;
            }
            return produto;
        }

        /// <summary>
		/// Obtém pelo identificado do produto
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public Product Obter(int idProduto)
        {
            IEnumerable<Product> produtos = GetQuery().Where(ProdutoModel => ProdutoModel.Id.Equals(idProduto));

            return produtos.ElementAtOrDefault(0);
        }

        /// <summary>
        /// Insere um novo produto na base de dados
        /// </summary>
        /// <param name="produto">dados da produto</param>
        /// <returns></returns>
        public int Inserir(Product produto)
        {

            _context.Add(produto);
            _context.SaveChanges();
            return produto.Id;

        }

        /// <summary>
		/// Remover produto pelo id
		/// </summary>
		/// <returns></returns>
        public void Remover(int idProduto)
        {
            var __produto = _context.Product.Find(idProduto);
            _context.Product.Remove(__produto);
            _context.SaveChanges();
        }

        /// <summary>
		/// Consulta por nome aos dados do produto
		/// </summary>
		/// <returns></returns>
        public IEnumerable<Product> ObterPorNome(string nome)
        {
            IEnumerable<Product> produto = GetQuery().Where(ProdutoModel => ProdutoModel.Name.StartsWith(nome));
            return produto;
        }

        /// <summary>
		/// Consulta por nome aos dados do produto com ordem decrecente
		/// </summary>
		/// <returns></returns>
        public IEnumerable<ProductDTO> ObterPorNomeOrdenadoDescending(string nome)
        {
            throw new NotImplementedException();
        }
    }
}
