using event_planner.Models;
using event_planner.Services;
using Xunit;

namespace event_planner.Tests
{
    public class AccountServiceTest
    {
        private static AccountService svc = new AccountService();

        [Fact]
        public void AddAccountTest()
        {
            Account testAccount = new Account();
            testAccount.Email = "unit@test.com";
            testAccount.Password = "pass";
            int newId = svc.AddAccount(testAccount);
            Assert.True(newId >= 0);
        }

        [Fact]
        public void GetAccountTest()
        {
            var accList = svc.GetAccounts();
            Assert.True(accList.Count > 0);
        }

        [Fact]
        public void GetAccountByIdTest()
        {
            var acc = svc.GetAccountById(1);
            Assert.True(acc != null);
        }

        [Fact]
        public void UpdateAccountTest()
        {
            var updateAcc = new Account{
                AccountId = 1,
                Email = "updatedAccount@email.com",
                Password = "updatedPassword"
            };
            var updatedRows = svc.UpdateAccount(updateAcc);
            var updated = svc.GetAccountById(updateAcc.AccountId);
            Assert.True(updatedRows == 1 && (updated.Email == updateAcc.Email && updated.Password == updateAcc.Password));
        }

        [Fact]
        public void DeleteAccountTest()
        {
            int deleteId = 4;
            var updatedRows = svc.DeleteAccount(deleteId);
            var deleted = svc.GetAccountById(deleteId);
            Assert.True(updatedRows == 1 && deleted.AccountId == 0);
        }
    }
}
