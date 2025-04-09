using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IMenucategoryService
    {
        Menucategory Obter(int idMenucategoryService);
        IEnumerable<Menucategory> ObterTodos();
    }
}
