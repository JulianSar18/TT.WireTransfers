using Moq;
using TT.WireTransfers.Application.DTOs;
using TT.WireTransfers.Application.Services;
using TT.WireTransfers.Domain.Entities;
using TT.WireTransfers.Domain.Exceptions;
using TT.WireTransfers.Domain.Interfaces;

namespace TT.WireTranfers.UnitTest;

public class MovementServiceTest
{
    [Fact]
    public async Task Transfer_Throws_When_Insufficient_Balance()
    {
        // Arrange
        var mockWalletRepo = new Mock<IWalletRepository>();
        var mockMovementRepo = new Mock<IMovementRepository>();

        var sourceWallet = new Wallet { Id = 1, Balance = 100 };
        var targetWallet = new Wallet { Id = 2, Balance = 50 };

        mockWalletRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(sourceWallet);
        mockWalletRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(targetWallet);

        var service = new MovementService(mockWalletRepo.Object, mockMovementRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InsufficientBalanceException>(() =>
            service.TransferAsync(new TransferDto
            {
                SourceWalletId = 1,
                DestinationWalletId = 2,
                Amount = 200
            }));
    }
}
