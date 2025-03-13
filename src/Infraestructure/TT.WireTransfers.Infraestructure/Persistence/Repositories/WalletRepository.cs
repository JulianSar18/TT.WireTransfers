using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using TT.WireTransfers.Domain.Entities;
using TT.WireTransfers.Domain.Interfaces;
using TT.WireTransfers.Infraestructure.Persistence.Data;

namespace TT.WireTransfers.Infraestructure.Persistence.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly WalletDbContext _context;

        public WalletRepository(WalletDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Wallet wallet)
        {
            wallet.CreatedAt = DateTime.UtcNow;
            wallet.UpdatedAt = DateTime.UtcNow;
            await _context.Wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int walletId)
        {
            var getWallet = await GetByIdWithOutMovements(walletId);
            if (getWallet == null)
                return;
            _context.Remove(getWallet);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Wallet>> GetAllAsync()
        {
            return await _context.Wallets.ToListAsync();
        }

        public async Task<Wallet> GetByIdAsync(int id)
        {
            return await _context.Wallets
                .Include(w => w.Movements)
                .FirstOrDefaultAsync(w => w.Id == id) ?? new Wallet();
        }

        public async Task UpdateAsync(Wallet wallet)
        {
            wallet.UpdatedAt = DateTime.UtcNow;
            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();
        }
        public async Task<Wallet> PatchUpdateAsync(int walletId, JsonPatchDocument walletDocument)
        {
            var getWallet = await GetByIdWithOutMovements(walletId);
            if (getWallet == null)
                return new Wallet();
            walletDocument.ApplyTo(getWallet);
            getWallet.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return getWallet;
        }

        private async Task<Wallet?> GetByIdWithOutMovements(int walletId)
        {
            return await _context.Wallets.FirstOrDefaultAsync(w => w.Id == walletId);
        }
    }
}
