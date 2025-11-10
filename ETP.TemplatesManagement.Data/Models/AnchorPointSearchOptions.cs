namespace ETP.TemplatesManagement.Data.Models
{
    public class AnchorPointSearchOptions
    {
        public List<Guid> DeliveryOwnerIds { get; set; } = new List<Guid>();
        public List<Guid> ServiceLineIds { get; set; } = new List<Guid>();
        public List<Guid> MarketOfferingIds { get; set; } = new List<Guid>();
        public List<string> DeliveryOwnerNames { get; set; } = new List<string>();
        public List<string> ServiceLineNames { get; set; } = new List<string>();
        public List<string> MarketOfferingNames { get; set; } = new List<string>();
    }
}
