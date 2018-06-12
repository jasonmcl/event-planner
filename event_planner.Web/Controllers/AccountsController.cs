using event_planner.Models;
using event_planner.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace event_planner.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Accounts")]
    public class AccountsController : Controller
    {
        private AccountService svc;

        [HttpPost]
        public int AddAccount([FromBody] Account model)
        {
            return svc.AddAccount(model);
        }

        [HttpGet]
        public List<Account> GetAccounts()
        {
            return svc.GetAccounts();
        }

        [HttpGet("{id}")]
        public Account GetAccountById(int id)
        {
            return svc.GetAccountById(id);
        }

        [HttpPut("{id}")]
        public void UpdateAccount(int id, [FromBody]Account model)
        {
            svc.UpdateAccount(model);
        }

        [HttpDelete("{id}")]
        public void DeleteAccount(int id)
        {
            svc.DeleteAccount(id);
        }

        public AccountsController()
        {
            svc = new AccountService();
        }
    }
}