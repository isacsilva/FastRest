using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IOrdertableService
    {

        void Editar(Ordertable ordertable);

        int Inserir(Ordertable ordertable);

        Ordertable Obter(int idOrdertable);

        IEnumerable<Ordertable> ObterTodos();

        void Remover(int idOrdertable);

        void AtualizarTotalPedido(int idOrdertable);
        
        void AtualizarStatus(int idOrdertable, string newStatus);
    }
}
