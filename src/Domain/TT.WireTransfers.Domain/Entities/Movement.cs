using System.Text.Json.Serialization;
using TT.WireTransfers.Domain.Enums;

namespace TT.WireTransfers.Domain.Entities
{
    public class Movement : BaseEntity
    {
        public int WalletId { get; set; }
        public decimal Amount { get; set; }
        public MovementType Type { get; set; }
        public Wallet? Wallet { get; set; }
    }
}
