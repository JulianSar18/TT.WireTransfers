using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TT.WireTransfers.Application.DTOs;
using TT.WireTransfers.Application.Services;
using TT.WireTransfers.Domain.Interfaces;

namespace TT.WireTransfers.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly CreateWallet createWallet;
        private readonly IWalletRepository _walletRepository;
        public WalletController(IWalletRepository walletRepository)
        {
            createWallet = new CreateWallet(walletRepository);
            _walletRepository = walletRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddWallet([FromBody] WalletDto wallet)
        {
            try
            {
                await createWallet.CreateAsync(wallet);
                return Ok(new { Message = "successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"error {ex.Message}" });
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetWallet(int walletId)
        {
            var response = await _walletRepository.GetByIdAsync(walletId);
            return Ok(response);
        }

        [HttpPatch]
        [Route("{walletId}")]
        public async Task<IActionResult> UpdateBalance(int walletId, [FromBody] JsonPatchDocument walletDocument)
        {
            var uptadeWallet = await _walletRepository.PatchUpdateAsync(walletId, walletDocument);

            return Ok(uptadeWallet);
        }
        [HttpDelete]
        [Route("{walletId}")]
        public async Task<IActionResult> DeleteWallet(int walletId)
        {
            await _walletRepository.DeleteAsync(walletId);
            return Ok(new { Message = "successful" });
        }
    }
}
