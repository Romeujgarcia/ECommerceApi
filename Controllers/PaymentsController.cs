using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly PaymentService _paymentService;

    public PaymentsController(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("charge")]
    public async Task<IActionResult> CreateCharge([FromBody] PaymentModel paymentModel)
    {
        var chargeId = await _paymentService.CreateCharge(paymentModel.Amount, paymentModel.Currency, paymentModel.Source);
        return Ok(new { ChargeId = chargeId });
    }
}

