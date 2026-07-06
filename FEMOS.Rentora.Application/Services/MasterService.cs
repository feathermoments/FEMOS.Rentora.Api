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

        
    }
}
