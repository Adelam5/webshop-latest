using Application.Common.Interfaces.Services;
using Domain.Core.Orders;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Security.Claims;

namespace Api.Controllers;

public class PaymentsController : ApiController
{
    private const string WhSecret = "whsec_OlekHeggYC7FCJgll6YCRIkVeJtaNWMS";
    private readonly IPaymentService paymentService;
    private readonly ILogger<PaymentsController> logger;

    public PaymentsController(IPaymentService paymentService,
        ILogger<PaymentsController> logger)
    {
        this.paymentService = paymentService;
        this.logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrUpdatePaymentIntent()
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var cart = await paymentService.CreateOrUpdatePaymentIntent(userId);

        if (cart == null) return BadRequest("Problem with your cart");

        return Ok(cart);
    }

    [HttpPost("webhook")]
    public async Task<ActionResult> StripeWebhook()
    {
        var json = await new StreamReader(Request.Body).ReadToEndAsync();
        var stripeEvent = EventUtility.ConstructEvent(json,
            Request.Headers["Stripe-Signature"], WhSecret);

        PaymentIntent intent;
        Order order;

        switch (stripeEvent.Type)
        {
            case "payment_intent.succeeded":
                intent = (PaymentIntent)stripeEvent.Data.Object;
                logger.LogInformation("Payment succeeded: " + intent.Id);
                order = await paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                logger.LogInformation($"Order updated to payment received: {order.Id}");
                break;
            case "payment_intent.payment_failed":
                intent = (PaymentIntent)stripeEvent.Data.Object;
                logger.LogInformation("Payment failed: " + intent.Id);
                order = await paymentService.UpdateOrderPaymentFailed(intent.Id);
                logger.LogInformation($"Order updated to payment failed: {order.Id}");
                break;
        }

        return new EmptyResult();
    }
}
