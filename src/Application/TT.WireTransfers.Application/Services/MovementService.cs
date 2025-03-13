using TT.WireTransfers.Application.DTOs;
using TT.WireTransfers.Domain.Entities;
using TT.WireTransfers.Domain.Enums;
using TT.WireTransfers.Domain.Exceptions;
using TT.WireTransfers.Domain.Interfaces;

namespace TT.WireTransfers.Application.Services
{
    public class MovementService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IMovementRepository _movementRepository;
        public MovementService(IWalletRepository walletRepository, IMovementRepository movementRepository)
        {
            _walletRepository = walletRepository;
            _movementRepository = movementRepository;
        }
        public async Task TransferAsync(TransferDto transferDto)
        {
            
            if (transferDto.Amount <= 0) throw new ArgumentException("Amount must be greater than zero");

            var sourceWallet = await _walletRepository.GetByIdAsync(transferDto.SourceWalletId);
            var destinationWallet = await _walletRepository.GetByIdAsync(transferDto.DestinationWalletId);
            if (sourceWallet == null || destinationWallet == null) throw new WalletNotFoundException("Wallet not found");

            if (sourceWallet.Balance < transferDto.Amount) throw new InsufficientBalanceException("Insufficient funds");

            sourceWallet.Balance -= transferDto.Amount;
            destinationWallet.Balance += transferDto.Amount;
            sourceWallet.UpdatedAt = DateTime.UtcNow;
            destinationWallet.UpdatedAt = DateTime.UtcNow;

            Movement movement = new Movement
            {
                WalletId = sourceWallet.Id,
                Amount = transferDto.Amount,
                Type = (MovementType)transferDto.Type,
                CreatedAt = DateTime.UtcNow
            };

            Movement movement2 = new Movement
            {
                WalletId = destinationWallet.Id,
                Amount = transferDto.Amount,
                Type = (MovementType)transferDto.Type,
                CreatedAt = DateTime.UtcNow

            };

            await _movementRepository.AddAsync(movement);
            await _movementRepository.AddAsync(movement2);


            await _walletRepository.UpdateAsync(sourceWallet);
            await _walletRepository.UpdateAsync(destinationWallet);

        }
    }
}
