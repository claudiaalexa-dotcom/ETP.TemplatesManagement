namespace ETP.TemplatesManagement.Data.Models
{
    public record DeliveryOwner(Guid Id, string Name);

    public record ServiceLine(Guid Id, string Name);

    public record MarketOffering(Guid Id, string Name);

    public record AnchorPoint
    {
        public DeliveryOwner DeliveryOwner { get; init; } = new DeliveryOwner(Guid.Empty, string.Empty);
        public ServiceLine ServiceLine { get; init; } = new ServiceLine(Guid.Empty, string.Empty);
        public MarketOffering MarketOffering { get; init; } = new MarketOffering(Guid.Empty, string.Empty);
    }
}
