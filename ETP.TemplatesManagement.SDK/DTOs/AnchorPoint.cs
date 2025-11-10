namespace ETP.TemplatesManagement.SDK.DTOs
{
    public record DeliveryOwner(Guid Id, string Name);

    public record ServiceLine(Guid Id, string Name);

    public record MarketOffering(Guid Id, string Name);

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
