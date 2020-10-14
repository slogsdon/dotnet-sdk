namespace GlobalPayments.Api.PaymentMethods {
    interface ITokenizable {
        string Token { get; set; }
        string Tokenize(string configName = "default");
        string Tokenize(bool validateCard, string billingPostalCode = "", string configName = "default");
        bool UpdateTokenExpiry(string configName = "default");
        bool DeleteToken(string configName = "default");
    }
}
