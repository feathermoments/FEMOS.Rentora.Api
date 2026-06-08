using FEMOS.Rentora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Responses
{
    public class DBAuthResponseInfo : DBResponseInfo
    {
        public Guid UserPublicId { get; set; }
        public string Role { get; set; }
        public bool IsNewUser { get; set; }
        public bool IsProfileComplete { get; set; }
    }
}
