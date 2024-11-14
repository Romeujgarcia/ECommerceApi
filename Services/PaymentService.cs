using Microsoft.Extensions.Options;
using Stripe;

public class PaymentService
{
   public PaymentService(IOptions<StripeSettings> stripeOptions)
    {
        var stripeSettings = stripeOptions.Value;

        if (string.IsNullOrWhiteSpace(stripeSettings.SecretKey))
        {
            throw new ArgumentNullException(nameof(stripeSettings.SecretKey), "Stripe Secret Key is not configured.");
        }

        Console.WriteLine($"Stripe Secret Key: {stripeSettings.SecretKey}"); // Log para verificar a chave
        StripeConfiguration.ApiKey = stripeSettings.SecretKey;
    }

    public async Task<string> CreateCharge(decimal amount, string currency, string source)
    {
        var options = new ChargeCreateOptions
        {
            Amount = (long)(amount * 100), // Valor em centavos
            Currency = currency,
            Source = source,
        };
        var service = new ChargeService();
        Charge charge = await service.CreateAsync(options);
        return charge.Id;
    }
}
