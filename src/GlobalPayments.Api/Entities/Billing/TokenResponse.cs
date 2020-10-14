using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalPayments.Api.Entities.Billing
{
    internal class TokenResponse: BillingResponse
    {
        internal string Token { get; set; }
    }
}
