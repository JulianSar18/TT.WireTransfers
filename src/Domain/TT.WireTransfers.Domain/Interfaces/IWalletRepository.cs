using Microsoft.AspNetCore.JsonPatch;
using TT.WireTransfers.Domain.Entities;
namespace TT.WireTransfers.Domain.Interfaces
{
    public interface IWalletRepository
    {
        Task<Wallet> GetByIdAsync(int id);
        Task<IEnumerable<Wallet>> GetAllAsync();
        Task AddAsync(Wallet wallet);
        Task<Wallet> PatchUpdateAsync(int walletId, JsonPatchDocument walletDocument);
        Task UpdateAsync(Wallet wallet);
        Task DeleteAsync(int walletId);
    }
}
