namespace ETP.TemplatesManagement.SDK.DTOs
{
    public class SearchOptions
    {
        public int? Page { get; set; }
        public int? Count { get; set; }
        public string? SearchTerm { get; set; }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }
    }
}
