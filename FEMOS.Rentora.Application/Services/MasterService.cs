using FEMOS.Rentora.Application.Interfaces;
using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Responses;
using FEMOS.Rentora.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Services
{
    internal class MasterService : IMasterService
    {
        private readonly IMasterRepository _masterRepository;
        public MasterService(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }

        public async Task<CityResponseInfo> GetCitiesByStateId(int stateId)
        {
            List<CityInfo> objCities = await _masterRepository.GetCitiesByStateId(stateId);
            return new CityResponseInfo
            {
                objCities = objCities,
                Status = StatusConstants.Success,
                Message = "Cities retrieved successfully."
            };
        }

        public async Task<StateResponseInfo> GetStatesByCountryId(int countryId)
        {
            List<StateInfo> objStates = await _masterRepository.GetStatesByCountryId(countryId);
            return new StateResponseInfo
            {
                objStates = objStates,
                Status = StatusConstants.Success,
                Message = "States retrieved successfully."
            };
        }

        public async Task<CountryResponseInfo> GetCountries()
        {
            List<CountryInfo> objCountries = await _masterRepository.GetCountries();
            return new CountryResponseInfo
            {
                objCountries = objCountries,
                Status = StatusConstants.Success,
                Message = "Countries retrieved successfully."
            };
        }

        public async Task<PropertyTypeResponseInfo> GetPropertyTypes()
        {
            List<PropertyTypeInfo> objPropertyTypes = await _masterRepository.GetPropertyTypes();
            return new PropertyTypeResponseInfo
            {
                objPropertyTypes = objPropertyTypes,
                Status = StatusConstants.Success,
                Message = "Property types retrieved successfully."
            };
        }

        public async Task<UnitTypeResponseInfo> GetUnitTypesAsync()
        {
            List<UnitTypeInfo> objUnitTypes = await _masterRepository.GetUnitTypesAsync();
            return new UnitTypeResponseInfo
            {
                objUnitTypes = objUnitTypes,
                Status = StatusConstants.Success,
                Message = "Unit types retrieved successfully."
            };
        }

        public async Task<BhkTypeResponseInfo> GetBHKTypesAsync()
        {
            List<BhkTypeInfo> objBhkTypes = await _masterRepository.GetBHKTypesAsync();
            return new BhkTypeResponseInfo
            {
                objBhkTypes = objBhkTypes,
                Status = StatusConstants.Success,
                Message = "BHK types retrieved successfully."
            };
        }

        public async Task<FurnishingTypeResponseInfo> GetFurnishingTypesAsync()
        {
            List<FurnishingTypeInfo> objFurnishingTypes = await _masterRepository.GetFurnishingTypesAsync();
            return new FurnishingTypeResponseInfo
            {
                objFurnishingTypes = objFurnishingTypes,
                Status = StatusConstants.Success,
                Message = "Furnishing types retrieved successfully."
            };
        }

        public async Task<UnitStatusTypeResponseInfo> GetUnitStatusTypesAsync()
        {
            List<UnitStatusTypeInfo> objUnitStatusTypes = await _masterRepository.GetUnitStatusTypesAsync();
            return new UnitStatusTypeResponseInfo
            {
                objUnitStatusTypes = objUnitStatusTypes,
                Status = StatusConstants.Success,
                Message = "Unit status types retrieved successfully."
            };
        }

        public async Task<PropertyStatusTypeResponseInfo> GetPropertyStatusTypesAsync()
        {
            List<PropertyStatusTypeInfo> objPropertyStatusTypes = await _masterRepository.GetPropertyStatusTypesAsync();
            return new PropertyStatusTypeResponseInfo
            {
                objPropertyStatusTypes = objPropertyStatusTypes,
                Status = StatusConstants.Success,
                Message = "Property status types retrieved successfully."
            };
        }

        public async Task<AgreementStatusTypeResponseInfo> GetAgreementStatusTypesAsync()
        {
            List<AgreementStatusTypeInfo> objAgreementStatusTypes = await _masterRepository.GetAgreementStatusTypesAsync();
            return new AgreementStatusTypeResponseInfo
            {
                objAgreementStatusTypes = objAgreementStatusTypes,
                Status = StatusConstants.Success,
                Message = "Agreement status types retrieved successfully."
            };
        }

        public async Task<TenantStatusTypeResponseInfo> GetTenantStatusTypesAsync()
        {
            List<TenantStatusTypeInfo> objTenantStatusTypes = await _masterRepository.GetTenantStatusTypesAsync();
            return new TenantStatusTypeResponseInfo
            {
                objTenantStatusTypes = objTenantStatusTypes,
                Status = StatusConstants.Success,
                Message = "Tenant status types retrieved successfully."
            };
        }
    }
}
