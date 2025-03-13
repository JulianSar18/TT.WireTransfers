using Microsoft.EntityFrameworkCore;
using TT.WireTransfers.Domain.Entities;
using TT.WireTransfers.Domain.Interfaces;
using TT.WireTransfers.Infraestructure.Persistence.Data;

namespace TT.WireTransfers.Infraestructure.Persistence.Repositories
{
    public class MovementRepository : IMovementRepository
    {
        private readonly WalletDbContext _context;
        public MovementRepository(WalletDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Movement movement)
        {
            _context.Movements.Add(movement);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Movement>> GetAllAsync()
        {
            return await _context.Movements.ToListAsync();
        }

        public async Task<IEnumerable<Movement>> GetByWalletIdAsync(int walletId)
        {
            return await _context.Movements
                .Where(w => w.WalletId == walletId)
                .ToListAsync();

        }
    }
}
