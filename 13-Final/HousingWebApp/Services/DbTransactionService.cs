using System;
using HousingWebApp.DB;

namespace HousingWebApp.Services
{
    public interface IDbTransactionService
    {
        Task ExecuteInTransactionAsync(Func<Task> action);
    }

    public class DbTransactionService : IDbTransactionService
    {
        private readonly HousingDBContext dbContext;

        public DbTransactionService(HousingDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Execute the action within the transaction
                    await action();

                    // Commit the transaction if successful
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    // Rollback the transaction if an exception occurs
                    await transaction.RollbackAsync();
                    throw; // Rethrow the exception for the caller to handle
                }
            }
        }
    }

}
