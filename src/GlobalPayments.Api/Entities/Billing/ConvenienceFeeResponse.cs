using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalPayments.Api.Entities.Billing
{
    internal class ConvenienceFeeResponse : BillingResponse
    {
        internal decimal ConvenienceFee { get; set; }
    }
}
