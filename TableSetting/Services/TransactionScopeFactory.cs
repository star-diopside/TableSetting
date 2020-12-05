using System.Transactions;

namespace TableSetting.Services
{
    public static class TransactionScopeFactory
    {
        public static TransactionScope CreateTransactionScope()
        {
            return new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.DefaultTimeout
            });
        }
    }
}
