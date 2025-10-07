using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Payments.Application.Producer.PaymentCompletedProducer;

namespace Payments.Application.Producer
{
    public interface IPaymentCompletedProducer
    {
        void PublishUserActiveEvent(PaymentCompleted payment);
    }
}
