using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETP.TemplatesManagement.Data.Models
{
    public class Template
    {
        public Guid Id { get; set; }
        public AnchorPoint AnchorPoint { get; set; } = new AnchorPoint();
        public string Title { get; set; } = string.Empty;
        public List<Attribute> Attributes { get; set; } = [];
    }
}
