using FEMOS.Rentora.Domain.Constants;
using FEMOS.Rentora.Domain.Entities;
using FEMOS.Rentora.Domain.Responses;
using FEMOS.Rentora.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FEMOS.Rentora.Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly IDBHelper _dbHelper;
        public SubscriptionRepository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<SubscriptionEntitlementsResponseInfo> GetEntitlementsAsync(Guid userPublicId)
        {
            var cmd = new SqlCommand(DBConstants.usp_Subscription_GetEntitlements);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserPublicId", userPublicId);

            var ds = await _dbHelper.GetDataSetBySQLCommandAsync(cmd);
            var response = new SubscriptionEntitlementsResponseInfo();

            if (ds == null)
            {
                response.Status = "Failure";
                response.Message = "No data returned from database.";
                return response;
            }
            if (ds.Tables.Count == 1 && ds.Tables[0].Columns.Count == 2)
            {
                DBResponseInfo objResponse = _dbHelper.ConvertDataTable<DBResponseInfo>(ds.Tables[0]).FirstOrDefault();
                return new SubscriptionEntitlementsResponseInfo()
                {
                    Status = objResponse.Status,
                    Message = objResponse.Message
                };
            }
            else
            {
                DBResponseInfo objResponse = _dbHelper.ConvertDataTable<DBResponseInfo>(ds.Tables[0]).FirstOrDefault();
                response.Status = objResponse.Status;
                response.Message = objResponse.Message;
            }

            // Map subscription (table 0)
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                response.Subscription = _dbHelper.ConvertDataTable<SubscriptionInfo>(ds.Tables[0]).FirstOrDefault();
            }

            // Map features (table 1)
            if (ds.Tables.Count > 1)
            {
                List<SubscriptionFeatureInfo> features = _dbHelper.ConvertDataTable<SubscriptionFeatureInfo>(ds.Tables[1]);
                response.Features.AddRange(features);
            }

            // Map limits (table 2)
            if (ds.Tables.Count > 2)
            {
                List<SubscriptionLimitInfo> limits = _dbHelper.ConvertDataTable<SubscriptionLimitInfo>(ds.Tables[2]);
                response.Limits.AddRange(limits);
            }
            return response;
        }
    }
}
