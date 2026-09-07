using FEMOS.Rentora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Responses
{
    public class SearchUserResponseInfo : BaseResponseInfo
    {
        public List<MemberUserInfo> objMemberUsers { get; set; }
        public MemberUserInfo objMemberInfo { get; set; }
    }
}
