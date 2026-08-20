using System;
using System.Collections.Generic;

namespace FEMOS.Rentora.Domain.Responses
{
    /// <summary>
    /// Response for selecting a property and establishing property context.
    /// </summary>
    public class PropertySelectionResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }

        public PropertyContextDetail PropertyContext { get; set; }
        public List<string> Permissions { get; set; }
    }

    public class PropertyContextDetail
    {
        public Guid PropertyUniqueId { get; set; }
        public string PropertyName { get; set; }
        public long InternalRoleId { get; set; }
        public string InternalRoleCode { get; set; }
        public string InternalRoleName { get; set; }
    }
}
