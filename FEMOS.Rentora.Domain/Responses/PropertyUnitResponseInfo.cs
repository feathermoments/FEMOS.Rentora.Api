using FEMOS.Rentora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Responses
{
    public class PropertyUnitResponseInfo : BaseResponseInfo
    {
        public Guid? UnitPublicId { get; set; }
        public List<MyPropertyUnitInfo> objMyPropertyUnits { get; set; }
        public PropertyUnitInfo objPropertyUnitInfo { get; set; }
    }
}
