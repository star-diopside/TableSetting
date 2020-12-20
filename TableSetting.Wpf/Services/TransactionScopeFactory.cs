using System.Transactions;

namespace TableSetting.Wpf.Services
{
    static class TransactionScopeFactory
    {
        internal static TransactionScope CreateTransactionScope()
        {
            return new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.DefaultTimeout
            });
        }

    }
}
