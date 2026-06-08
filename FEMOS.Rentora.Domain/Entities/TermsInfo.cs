using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class TermsInfo
    {
        public int Version { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsMajorUpdate { get; set; }
        public string EffectiveFrom { get; set; } = string.Empty; // ISO string
    }
}
