namespace TestTransactionalBatchConsole
{
    public class AccountClosed
    {
        public string id { get; set; }
        public string AccountNumber { get; set; }
        public string Reason { get; set; }
        public string DocumentType { get; set; } = nameof(AccountClosed);
    }
}
