using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IProductService
    {
        void Editar(Product product);

        int Inserir(Product product);

        Product Obter(int idProduct);

        IEnumerable<Product> ObterPorNome(string Nome);

        IEnumerable<Product> ObterTodos();

        void Remover(int idProduct);
    }
}