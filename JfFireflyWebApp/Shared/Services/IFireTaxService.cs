using FireflyIII;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JfFireflyWebApp.Shared.Services
{
    public interface IFireTaxService
    {
        public Task<SystemInfo> GetSystemInfoAsync();
        public Task<List<(TransactionSplit ts, string desc)>> GetExpensesAccountsAsync(int searchId);
        public Task<TaxmanResult> GetTaxmanResultAsync(DateTimeOffset startDate, DateTimeOffset endDate);
    }
}
