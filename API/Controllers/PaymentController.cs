using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace API.Controllers;

public class PaymentController : BaseApiController
{
    private readonly StripeSettings _settings;

    public PaymentController(IOptions<StripeSettings> settings)
    {
        _settings = settings.Value;
        //StripeConfiguration.ApiKey = "sk_test_51L71r8CHLmgQslOwRa3nsVtzU5I7plqWQWBGh2hpVNFirKzDJfE4f4zohlmivq06VV4VQrqcKwLfcdlV33x2jBRu00LKbZl8Zu";
    }

    [HttpPost("create-checkout-session")]
    public async Task<IActionResult> CreateCheckoutSession([FromBody]CreateCheckoutSessionRequest req)
    {
        

// The price ID passed from the front end.
// You can extract the form value with the following:
//   var priceId = Request.Form["priceId"];
        

        var options = new SessionCreateOptions
        {
            
            SuccessUrl = "https://localhost:4200/home",
            CancelUrl = "https://example.com/canceled.html",
            PaymentMethodTypes = new List<string> {"card"},
            Mode = "subscription",
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    Price = req.PriceId,
                    // For metered billing, do not pass quantity
                    Quantity = 1,
                },
            },
        };

        var service = new SessionService();
        try
        {
            var session = await service.CreateAsync(options);
            return Ok(new CreateCheckoutSessionResponse
            {
                SessionId = session.Id,
                PublicKey = _settings.PublicKey
            });
        }
        catch (StripeException e)
        {
            Console.WriteLine(e);
            return BadRequest(new ErrorResponse
				{
					ErrorMessage = new ErrorMessage
					{
						Message = e.StripeError.Message,
					}
				});
        }
        

// Redirect to the URL returned on the Checkout Session.
//   Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
    }
}