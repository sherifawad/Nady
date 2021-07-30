using Core.Models;
using DataBase.UnitOfWork;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nady
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly IUnitOfWork _unitOfWork;
        public DatabaseHealthCheck(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
               await  _unitOfWork.Repository<Member>().FindAsync(string.Empty);
                return HealthCheckResult.Healthy();
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
