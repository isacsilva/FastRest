using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IRestaurantService
    {
        Restaurant Obter(int idRestaurant);

        IEnumerable<Restaurant> ObterTodos();
    }
}
