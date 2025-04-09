using Core;
using Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly FastRestContext _context;

        public RestaurantService(FastRestContext context)
        {
            _context = context;
        }

        /// <summary>
		/// Consulta genérica aos dados do restaurante
		/// </summary>
		/// <returns></returns>
		private IQueryable<Restaurant> GetQuery()
        {
            IQueryable<Restaurant> tb_restaurante = _context.Restaurant;
            var query = from restaurante in tb_restaurante
                        select restaurante;
            return query;
        }

        /// <summary>
        /// Obtém todos os restaurantes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Restaurant> ObterTodos()
        {
            return GetQuery();
        }

        /// <summary>
		/// Obtém pelo identificado do restaurante
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public Restaurant Obter(int idRestaurante)
        {
            IEnumerable<Restaurant> produtos = GetQuery().Where(RestauranteModel => RestauranteModel.Id.Equals(idRestaurante));

            return produtos.ElementAtOrDefault(0);
        }
    }
}
