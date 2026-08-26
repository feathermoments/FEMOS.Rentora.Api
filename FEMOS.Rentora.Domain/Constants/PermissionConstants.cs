using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Domain.Constants
{
    /// <summary>
    /// Defines all permission constants used throughout the application.
    /// Permissions follow a hierarchical naming convention: DOMAIN.ACTION
    /// </summary>
    public class PermissionConstants
    {
        // Default Permissions - Available to all users regardless of role
        /// <summary>
        /// Permission to view user's own properties
        /// </summary>
        public const string MY_PROPERTIES = "MY.PROPERTIES";

        /// <summary>
        /// Permission to create new properties
        /// </summary>
        public const string PROPERTY_CREATE = "PROPERTY.CREATE";

        // Property Permissions
        public const string PROPERTY_VIEW = "PROPERTY.VIEW";
        public const string PROPERTY_EDIT = "PROPERTY.EDIT";
        public const string PROPERTY_DELETE = "PROPERTY.DELETE";

        // Unit Permissions
        public const string UNIT_VIEW = "UNIT.VIEW";
        public const string UNIT_CREATE = "UNIT.CREATE";
        public const string UNIT_EDIT = "UNIT.EDIT";
        public const string UNIT_DELETE = "UNIT.DELETE";

        // Tenant Permissions
        public const string TENANT_VIEW = "TENANT.VIEW";
        public const string TENANT_CREATE = "TENANT.CREATE";
        public const string TENANT_EDIT = "TENANT.EDIT";
        public const string TENANT_DELETE = "TENANT.DELETE";

        // Rent Agreement Permissions
        public const string AGREEMENT_VIEW = "AGREEMENT.VIEW";
        public const string AGREEMENT_CREATE = "AGREEMENT.CREATE";
        public const string AGREEMENT_EDIT = "AGREEMENT.EDIT";
        public const string AGREEMENT_DELETE = "AGREEMENT.DELETE";

        // Rent Permissions
        public const string RENT_VIEW = "RENT.VIEW";
        public const string RENT_COLLECT = "RENT.COLLECT";
        public const string RENT_REPORT = "RENT.REPORT";

        // Maintenance Permissions
        public const string MAINTENANCE_VIEW = "MAINTENANCE.VIEW";
        public const string MAINTENANCE_CREATE = "MAINTENANCE.CREATE";
        public const string MAINTENANCE_EDIT = "MAINTENANCE.EDIT";

        /// <summary>
        /// Gets the default permissions available to all users
        /// </summary>
        /// <returns>List of default permission codes</returns>
        public static List<string> GetDefaultPermissions()
        {
            return new List<string>
            {
                MY_PROPERTIES,
                PROPERTY_CREATE
            };
        }
    }
}
