using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;

namespace BudgetManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IDalWallet Dal;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dal"></param>
        public WalletController(IDalWallet dal)
        {
            Dal = dal;
        }

        // GET: WalletController
        [HttpGet]
        public int Index()
        {
            return 1;
        }

        /// <summary>
        /// Obtem a carteira de um usuario especafico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{userId:int}")] 
        public Wallet Get(int userId)
        {
            return Dal.Find(userId);
        }

        [HttpPost]
        public Wallet Post([FromBody]Wallet wallet)
        {
            return Dal.Insert(wallet);
        }
    }
}