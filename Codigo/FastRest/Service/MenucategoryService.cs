using Core;
using Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MenucategoryService : IMenucategoryService
    {
        private readonly FastRestContext _context;

        public MenucategoryService(FastRestContext context)
        {
            _context = context;
        }

        /// <summary>
		/// Consulta genérica aos dados do Menucategory
		/// </summary>
		/// <returns></returns>
		private IQueryable<Menucategory> GetQuery()
        {
            IQueryable<Menucategory> tb_menucategory = _context.Menucategory;
            var query = from menucategory in tb_menucategory
                        select menucategory;
            return query;
        }

        /// <summary>
        /// Obtém todos os menucategory
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Menucategory> ObterTodos()
        {
            return GetQuery();
        }

        /// <summary>
		/// Obtém pelo identificado do menucategory
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public Menucategory Obter(int idMenucategory)
        {
            IEnumerable<Menucategory> menucategory = GetQuery().Where(MenucategoryModel => MenucategoryModel.Id.Equals(idMenucategory));

            return menucategory.ElementAtOrDefault(0);
        }
    }
}
