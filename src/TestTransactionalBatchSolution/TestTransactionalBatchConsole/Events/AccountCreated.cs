namespace TestTransactionalBatchConsole
{
    public class AccountCreated
    {
        public string id { get; set; } = "1";
        public string AccountNumber { get; set; }
        public string Holder { get; set; }

        public string DocumentType { get; set; } = nameof(AccountCreated);
    }
}
