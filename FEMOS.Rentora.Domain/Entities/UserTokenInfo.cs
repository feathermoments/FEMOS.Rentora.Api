using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Entities
{
    public class UserTokenInfo
    {
        public string Token { get; set; }
        public int DeviceTypeId { get; set; }
        public string DeviceName { get; set; }
        public string AppVersion { get; set; }
    }
}
