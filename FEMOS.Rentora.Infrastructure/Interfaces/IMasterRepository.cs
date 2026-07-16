using FEMOS.Rentora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Interfaces
{
    public interface IMasterRepository
    {
        Task<List<PropertyTypeInfo>> GetPropertyTypes();
        
        Task<List<CountryInfo>> GetCountries();
        Task<List<StateInfo>> GetStatesByCountryId(int countryId);
        Task<List<CityInfo>> GetCitiesByStateId(int stateId);

        Task<List<UnitTypeInfo>> GetUnitTypesAsync();
        Task<List<BhkTypeInfo>> GetBHKTypesAsync();
        Task<List<FurnishingTypeInfo>> GetFurnishingTypesAsync();
        Task<List<UnitStatusTypeInfo>> GetUnitStatusTypesAsync();
        Task<List<PropertyStatusTypeInfo>> GetPropertyStatusTypesAsync();
        Task<List<AgreementStatusTypeInfo>> GetAgreementStatusTypesAsync();
        Task<List<TenantAssignmentStatusTypeInfo>> GetTenantAssignmentStatusTypesAsync();
    }
}
