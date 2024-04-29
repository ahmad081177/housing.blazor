using HousingModels.Models;
using HousingWebApp.DB;
using Microsoft.EntityFrameworkCore;

namespace HousingWebApp.Services
{
    public class AppAuthService
    {
        private readonly HousingDBContext db;
        private readonly IDbTransactionService TransactionService;
        public AppAuthService(HousingDBContext db, IDbTransactionService TransactionService)
        {
            this.db = db;
            this.TransactionService = TransactionService;
        }
        public async Task<bool> IsAdmin(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                AppUser? user = await GetUserAsync(email);
                if(user != null)
                {
                    return user.IsAdmin;
                }
            }
            return false;
        }
        public async Task<AppUser?> GetUserAsync(string? email)
        {
            AppUser? user = null;
            if (!string.IsNullOrWhiteSpace(email))
            {
                //await TransactionService.ExecuteInTransactionAsync(async () =>
                //{
                    user = await db.AppUsers.Where(u => u.Email == email).Include(h=>h.Address).FirstOrDefaultAsync();
                    user = user?.IsBlocked == false ? user : null;
                //});
            }
            return user;
        }
    }
}
