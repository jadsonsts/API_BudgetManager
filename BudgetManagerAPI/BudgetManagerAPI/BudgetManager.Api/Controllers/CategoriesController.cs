using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IDalCategories Dal;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dal"></param>
        public CategoriesController(IDalCategories dal)
        {
            Dal = dal;
        }

        /// <summary>
        /// Lista todas as categorias
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Categories> Get()
        {
            return Dal.List();
        }

        /// <summary>
        /// Consulta a categoria por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("byid/{id}")]
        public Categories Get(int id)
        {
            return Dal.Find(id);
        }
    }
}