using TT.WireTransfers.Domain.Entities;

namespace TT.WireTransfers.Domain.Interfaces
{
    public interface IMovementRepository
    {
        Task<IEnumerable<Movement>> GetByWalletIdAsync(int walletId);
        Task<IEnumerable<Movement>> GetAllAsync();
        Task AddAsync(Movement movement);
    }
}
