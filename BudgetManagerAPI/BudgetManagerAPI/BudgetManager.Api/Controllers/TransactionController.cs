using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IDalTransaction Dal;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dal"></param>
        public TransactionController(IDalTransaction dal)
        {
            Dal = dal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("{type}/{id}")]
        public IEnumerable<Transaction> Get(int type, int id)
        {
            return Dal.ListFiltered(type, id);
        }

        [HttpPost]
        public Transaction Post([FromBody]Transaction transaction)
        {
            return Dal.Insert(transaction);
        }

        //TO BE CHECKED
        // [HttpPut]
        // public Transaction Put([FromBody]Transaction transaction)
        // {
        //     return Dal.Edit(transaction);
        // }
    }
}