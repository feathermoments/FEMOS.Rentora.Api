using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FEMOS.Rentora.Infrastructure.Connections;
using FEMOS.Rentora.Infrastructure.Interfaces;

namespace FEMOS.Rentora.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public PropertyRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
    }
}
