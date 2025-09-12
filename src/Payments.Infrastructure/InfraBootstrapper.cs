using Microsoft.Extensions.DependencyInjection;
using Payments.Application.Repository;
using Payments.Infrastructure.DataBase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Infrastructure
{
    public static class InfraBootstrapper
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IPaymentRepository, PaymentRepository>();
        }
    }
}
