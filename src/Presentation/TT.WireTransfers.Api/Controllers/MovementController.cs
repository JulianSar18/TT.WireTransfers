using Microsoft.AspNetCore.Mvc;
using TT.WireTransfers.Application.DTOs;
using TT.WireTransfers.Application.Services;
using TT.WireTransfers.Domain.Interfaces;
using TT.WireTransfers.Infraestructure.Persistence.Repositories;

namespace TT.WireTransfers.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovementController : ControllerBase
    {
        private readonly MovementService _MovementService;
        private readonly IMovementRepository _movementRepository;
        public MovementController(IWalletRepository walletRepository, IMovementRepository movementRepository)
        {
            _MovementService = new MovementService(walletRepository, movementRepository);
            _movementRepository = movementRepository;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAllMovements()
        {
            var response = await _movementRepository.GetAllAsync();
            return Ok(response);
        }
        [HttpGet]
        [Route("{walletId}")]
        public async Task<IActionResult> GetMovementsByWalletId(int walletId)
        {
            var response = await _movementRepository.GetByWalletIdAsync(walletId);
            return Ok(response);
        }

        [HttpPost]
        [Route("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferDto transferDto)
        {
            await _MovementService.TransferAsync(transferDto);
            return Ok(new { Message = "successful" });
        }
    }
}
