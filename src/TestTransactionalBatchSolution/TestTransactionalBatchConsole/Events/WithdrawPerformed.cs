namespace TestTransactionalBatchConsole
{
    public class WithdrawPerformed
    {
        public string id { get; set; }
        public string AccountNumber { get; set; }
        public int Amount { get; set; }
        public string DocumentType { get; set; } = nameof(WithdrawPerformed);
    }
}
