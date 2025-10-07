using Microsoft.Extensions.DependencyInjection;
using Payments.Application.Producer;
using Payments.Application.Repository;
using Payments.Application.Services;
using Payments.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Application
{
    public static class ApplicationBootstrapper
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IPaymentCompletedProducer,  PaymentCompletedProducer>();
        }
    }
}
