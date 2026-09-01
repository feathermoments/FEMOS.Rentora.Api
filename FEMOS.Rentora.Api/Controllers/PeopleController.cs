using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Requests;
using FEMOS.Rentora.Shared.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        #region Property Members

        /// <summary>
        /// GET /api/people/properties/{propertyPublicId}/members
        /// Returns all active members associated with a property.
        /// Requires authentication.
        /// </summary>
        [HttpGet("properties/{propertyPublicId}/members")]
        public async Task<IActionResult> GetPropertyMembers(Guid propertyPublicId)
        {
            var userPublicId = User.GetUserPublicId();
            var result = await _peopleService.GetPropertyMembersAsync(propertyPublicId, userPublicId);
            return Ok(result);
        }

        /// <summary>
        /// GET /api/people/properties/{propertyPublicId}/owners
        /// Returns all owners of a property.
        /// Requires authentication.
        /// </summary>
        [HttpGet("properties/{propertyPublicId}/owners")]
        public async Task<IActionResult> GetPropertyOwners(Guid propertyPublicId)
        {
            var userPublicId = User.GetUserPublicId();
            var result = await _peopleService.GetPropertyOwnersAsync(propertyPublicId, userPublicId);
            return Ok(result);
        }

        /// <summary>
        /// POST /api/people/properties/{propertyPublicId}/owners
        /// Adds an existing Rentora user as a co-owner of the property.
        /// Requires authentication.
        /// </summary>
        [HttpPost("properties/{propertyPublicId}/co-owners")]
        public async Task<IActionResult> AddPropertyOwner(Guid propertyPublicId, [FromBody] PropertyCoOwnerRequestInfo request)
        {
            if (request == null)
                return BadRequest(new { Status = "Failure", Message = "Request body is required." });

            if (request.objPropertyOwnerInfo == null)
                return BadRequest(new { Status = "Failure", Message = "Property owner information is required." });

            if (request.objPropertyOwnerInfo.OwnershipPercentage <= 0 || request.objPropertyOwnerInfo.OwnershipPercentage > 100)
                return BadRequest(new { Status = "Failure", Message = "OwnershipPercentage must be between 0 and 100." });

            var userPublicId = User.GetUserPublicId();
            request.UserPublicId = userPublicId;
            request.objPropertyOwnerInfo.PropertyPublicId = propertyPublicId;

            var result = await _peopleService.AddPropertyCoOwnerAsync(request);

            if (result.Status == "Failure")
                return BadRequest(result);

            return CreatedAtAction(nameof(GetPropertyOwners), new { propertyPublicId }, result);
        }

        /// <summary>
        /// POST /api/people/properties/{propertyPublicId}/owners
        /// Adds an existing Rentora user as a co-owner of the property.
        /// Requires authentication.
        /// </summary>
        [HttpPut("properties/{propertyPublicId}/co-owners/{propertyOwnerPublicId}")]
        public async Task<IActionResult> UpdatePropertyOwner(Guid propertyPublicId, Guid propertyOwnerPublicId, [FromBody] UpdatePropertyCoOwnerRequestInfo request)
        {
            if (request == null)
                return BadRequest(new { Status = "Failure", Message = "Request body is required." });

            if (request.objPropertyOwnerInfo == null)
                return BadRequest(new { Status = "Failure", Message = "Property owner information is required." });

            if (request.objPropertyOwnerInfo.OwnershipPercentage <= 0 || request.objPropertyOwnerInfo.OwnershipPercentage > 100)
                return BadRequest(new { Status = "Failure", Message = "OwnershipPercentage must be between 0 and 100." });

            var userPublicId = User.GetUserPublicId();
            request.UserPublicId = userPublicId;
            request.objPropertyOwnerInfo.PropertyPublicId = propertyPublicId;
            request.objPropertyOwnerInfo.PropertyOwnerPublicId = propertyOwnerPublicId;

            var result = await _peopleService.UpdatePropertyCoOwnerAsync(request);

            if (result.Status == "Failure")
                return BadRequest(result);

            return CreatedAtAction(nameof(GetPropertyOwners), new { propertyPublicId }, result);
        }

        /// <summary>
        /// DELETE /api/people/properties/{propertyPublicId}/owners/{propertyOwnerPublicId}
        /// Removes a co-owner from the property.
        /// Requires authentication.
        /// </summary>
        [HttpDelete("properties/{propertyPublicId}/co-owners/{propertyOwnerPublicId}")]
        public async Task<IActionResult> RemovePropertyOwner(Guid propertyPublicId, Guid propertyOwnerPublicId)
        {
            if (propertyPublicId == Guid.Empty || propertyOwnerPublicId == Guid.Empty)
                return BadRequest(new { Status = "Failure", Message = "PropertyPublicId and PropertyOwnerPublicId must be valid GUIDs." });

            var userPublicId = User.GetUserPublicId();
            var result = await _peopleService.RemovePropertyCoOwnerAsync(propertyPublicId, propertyOwnerPublicId, userPublicId);

            if (result.Status == "Failure")
                return BadRequest(result);

            return NoContent();
        }

        #endregion

        #region Tenant Family Members

        /// <summary>
        /// GET /api/people/agreements/{rentAgreementPublicId}/family-members
        /// Returns all family members associated with a rent agreement.
        /// Requires authentication.
        /// </summary>
        [HttpGet("agreements/{rentAgreementPublicId}/family-members")]
        public async Task<IActionResult> GetFamilyMembers(Guid rentAgreementPublicId)
        {
            var userPublicId = User.GetUserPublicId();
            var result = await _peopleService.GetTenantFamilyMembersAsync(rentAgreementPublicId, userPublicId);
            return Ok(result);
        }

        /// <summary>
        /// POST /api/people/agreements/{rentAgreementPublicId}/family-members
        /// Adds a family member to a tenant agreement.
        /// Requires authentication.
        /// </summary>
        [HttpPost("agreements/{rentAgreementPublicId}/family-members")]
        public async Task<IActionResult> AddFamilyMember(Guid rentAgreementPublicId, [FromBody] TenantFamilyMemberRequestInfo request)
        {
            if (request == null)
                return BadRequest(new { Status = "Failure", Message = "Request body is required." });

            if (request.objTenantFamilyMemberInfo == null)
                return BadRequest(new { Status = "Failure", Message = "Family member information is required." });

            if (string.IsNullOrWhiteSpace(request.objTenantFamilyMemberInfo.FullName))
                return BadRequest(new { Status = "Failure", Message = "FullName is required." });

            if (request.objTenantFamilyMemberInfo.TenantFamilyRelationId <= 0)
                return BadRequest(new { Status = "Failure", Message = "TenantFamilyRelationId must be greater than 0." });

            if (string.IsNullOrWhiteSpace(request.objTenantFamilyMemberInfo.GenderId))
                return BadRequest(new { Status = "Failure", Message = "GenderId is required." });

            var userPublicId = User.GetUserPublicId();
            request.UserPublicId = userPublicId;
            request.objTenantFamilyMemberInfo.RentAgreementPublicId = rentAgreementPublicId;

            var result = await _peopleService.SaveTenantFamilyMemberAsync(request);

            if (result.Status == "Failure")
                return BadRequest(result);

            return CreatedAtAction(nameof(GetFamilyMembers), new { rentAgreementPublicId }, result);
        }

        /// <summary>
        /// PUT /api/people/family-members/{familyMemberPublicId}
        /// Updates a family member's details.
        /// Requires authentication.
        /// </summary>
        [HttpPut("agreements/{rentAgreementPublicId}/family-members/{familyMemberPublicId}")]
        public async Task<IActionResult> UpdateFamilyMember(Guid rentAgreementPublicId, Guid familyMemberPublicId, [FromBody] TenantFamilyMemberRequestInfo request)
        {
            if (rentAgreementPublicId == Guid.Empty || familyMemberPublicId == Guid.Empty)
                return BadRequest(new { Status = "Failure", Message = "RentAgreementPublicId and FamilyMemberPublicId must be valid GUIDs." });

            if (request == null)
                return BadRequest(new { Status = "Failure", Message = "Request body is required." });

            if (request.objTenantFamilyMemberInfo == null)
                return BadRequest(new { Status = "Failure", Message = "Family member information is required." });

            if (string.IsNullOrWhiteSpace(request.objTenantFamilyMemberInfo.FullName))
                return BadRequest(new { Status = "Failure", Message = "FullName is required." });

            if (request.objTenantFamilyMemberInfo.TenantFamilyRelationId <= 0)
                return BadRequest(new { Status = "Failure", Message = "TenantFamilyRelationId must be greater than 0." });

            if (string.IsNullOrWhiteSpace(request.objTenantFamilyMemberInfo.GenderId))
                return BadRequest(new { Status = "Failure", Message = "GenderId is required." });

            var userPublicId = User.GetUserPublicId();
            request.UserPublicId = userPublicId;
            request.objTenantFamilyMemberInfo.RentAgreementPublicId = rentAgreementPublicId;
            request.objTenantFamilyMemberInfo.FamilyMemberPublicId = familyMemberPublicId;

            // Note: This would require a dedicated update method in the service if it's separate from Save
            var result = await _peopleService.SaveTenantFamilyMemberAsync(request);

            if (result.Status == "Failure")
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// DELETE /api/people/agreements/{rentAgreementPublicId}/family-members/{familyMemberPublicId}
        /// Removes a family member from a tenant agreement.
        /// Requires authentication.
        /// </summary>
        [HttpDelete("agreements/{rentAgreementPublicId}/family-members/{familyMemberPublicId}")]
        public async Task<IActionResult> RemoveFamilyMember(Guid rentAgreementPublicId, Guid familyMemberPublicId)
        {
            if (rentAgreementPublicId == Guid.Empty || familyMemberPublicId == Guid.Empty)
                return BadRequest(new { Status = "Failure", Message = "RentAgreementPublicId and FamilyMemberPublicId must be valid GUIDs." });

            var userPublicId = User.GetUserPublicId();
            var result = await _peopleService.RemoveTenantFamilyMemberAsync(rentAgreementPublicId, familyMemberPublicId, userPublicId);

            if (result.Status == "Failure")
                return BadRequest(result);

            return NoContent();
        }

        #endregion

        #region Search

        /// <summary>
        /// GET /api/people/search-user/{searchText}
        /// Searches for a user by name, email, or phone number.
        /// Requires authentication.
        /// </summary>
        [HttpGet("search-user/{searchText}")]
        public async Task<IActionResult> SearchUser(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return BadRequest(new { Status = "Failure", Message = "Search text is required." });

            var userPublicId = User.GetUserPublicId();
            var result = await _peopleService.SearchUserAsync(userPublicId, searchText);
            return Ok(result);
        }

        #endregion
    }
}
