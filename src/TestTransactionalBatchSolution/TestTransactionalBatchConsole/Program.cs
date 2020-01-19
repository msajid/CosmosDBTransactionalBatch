using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace TestTransactionalBatchConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var dbName = "BankDB";
            var containerName = "AccountsContainer";

            using (var cosmosClient = new CosmosClient("https://localhost:8081", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="))
            {
                
                var dbCreated = await cosmosClient.CreateDatabaseIfNotExistsAsync(dbName, 400);
                var containerResponse = await cosmosClient.GetDatabase(dbName).CreateContainerIfNotExistsAsync(containerName, "/AccountNumber");
                var container = containerResponse.Container;

                var metadata = new Metadata() { id = "0", AccountNumber = "12345", LastSequence = "2", AccountStatus = Status.Active };
                var newAccountDetails = new AccountCreated() { id = "1", Holder = "Sajid", AccountNumber = "12345" };
                var initialDepositDetailes = new DepositPerformed() { id = "2", Amount = 10, AccountNumber = "12345" };

                var batchTransaction = container
                    .CreateTransactionalBatch(new PartitionKey(metadata.AccountNumber))
                    .CreateItem<Metadata>(metadata)
                    .CreateItem<AccountCreated>(newAccountDetails)
                    .CreateItem<DepositPerformed>(initialDepositDetailes);
               
                var transactionResult = await batchTransaction.ExecuteAsync();

                if (transactionResult.IsSuccessStatusCode)
                {
                    var metadataCreated = transactionResult.GetOperationResultAtIndex<Metadata>(0);
                    var accountCreated = transactionResult.GetOperationResultAtIndex<AccountCreated>(1);
                    var initialDepositPerformed = transactionResult.GetOperationResultAtIndex<DepositPerformed>(2);
                }
                else
                {
                    Console.WriteLine(transactionResult.ErrorMessage);
                }
            }
        }
    }
}
