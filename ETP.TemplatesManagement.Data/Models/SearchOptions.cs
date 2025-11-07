using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETP.TemplatesManagement.Data.Models
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
