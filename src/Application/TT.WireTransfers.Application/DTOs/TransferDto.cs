namespace TT.WireTransfers.Application.DTOs
{
    public class TransferDto
    {
        public int SourceWalletId { get; set; }
        public int DestinationWalletId { get; set; }
        public decimal Amount { get; set; }
        public int Type { get; set; }
    }
}
