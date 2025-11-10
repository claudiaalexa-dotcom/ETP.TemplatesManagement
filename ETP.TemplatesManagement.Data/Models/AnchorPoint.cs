namespace ETP.TemplatesManagement.Data.Models
{
    public record AnchorPoint
    {
        public Guid DeliveryOwnerId { get; init; }
        public string DeliveryOwnerName { get; init; } = string.Empty;

        public Guid ServiceLineId { get; init; }
        public string ServiceLineName { get; init; } = string.Empty;

        public Guid MarketOfferingId { get; init; }
        public string MarketOfferingName { get; init; } = string.Empty;
    }
}
