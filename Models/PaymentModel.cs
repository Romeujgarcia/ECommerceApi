public class PaymentModel
{
    public decimal Amount { get; set; }
    public string? Currency { get; set; }
    public string? Source { get; set; } // Token de pagamento do Stripe
}
