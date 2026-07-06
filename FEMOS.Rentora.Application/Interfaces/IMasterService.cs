using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Application.Interfaces
{
    public interface IMasterService
    {
        Task<PropertyTypeResponseInfo> GetPropertyTypes();

        Task<CountryResponseInfo> GetCountries();
        Task<StateResponseInfo> GetStatesByCountryId(int countryId);
        Task<CityResponseInfo> GetCitiesByStateId(int stateId);
    }
}
