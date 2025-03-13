using TT.WireTransfers.Application.DTOs;
using TT.WireTransfers.Domain.Entities;
using TT.WireTransfers.Domain.Interfaces;

namespace TT.WireTransfers.Application.Services
{
    public class CreateWallet
    {
        private readonly IWalletRepository _walletRepository;

        public CreateWallet(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task CreateAsync(WalletDto wallet)
        {
            Wallet mapWallet = new Wallet
            {
                DocumentId = wallet.DocumentId,
                Name = wallet.Name,
                Balance = wallet.Balance,

            };
            await _walletRepository.AddAsync(mapWallet);
        }
    }
}
