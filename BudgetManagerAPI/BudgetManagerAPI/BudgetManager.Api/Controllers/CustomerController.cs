using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;

namespace BudgetManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IDalCustomer Dal;
        public CustomerController(IDalCustomer dal)
        {
            Dal = dal;
        }

        // GET: CustomerController
        [HttpGet]
        public int Index()
        {
            return 1;
        }

        /// <summary>
        /// Obtem as informações de um usuário específico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("byid/{id}")]
        public Customer Get(string id)
        {
            return Dal.Find(id);
        }

        /// <summary>
        /// Adiciona um cadastro de usuário
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public Customer Post([FromBody]Customer customer)
        {
            return Dal.Insert(customer);
        }

        /// <summary>
        /// edita um cadastro de usuário
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>

        
        [HttpPut]
        public bool Put([FromBody]Customer customer)
        {
            return Dal.Edit(customer);
        }
    }
}