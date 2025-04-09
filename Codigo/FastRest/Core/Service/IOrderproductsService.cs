using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IOrderproductsService
    {
        void Editar(Orderproducts orderProducts);

        int Inserir(Orderproducts orderProducts);

        Orderproducts Obter(int idOrderProducts);

        IEnumerable<Orderproducts> ObterTodos();

        void Remover(int idOrderproducts);

    }
}
