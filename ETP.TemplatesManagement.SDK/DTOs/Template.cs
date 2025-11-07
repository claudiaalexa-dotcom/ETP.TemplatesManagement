namespace ETP.TemplatesManagement.SDK.DTOs
{
    public class TemplateBase
    {
        public AnchorPoint AnchorPoint { get; set; } = new AnchorPoint();
        public string Title { get; set; } = string.Empty;
        public List<Attribute> Attributes { get; set; } = [];
    }

    public class Template : TemplateBase
    {
        public Guid Id { get; set; }
    }
}
