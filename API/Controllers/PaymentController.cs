using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using API.Dtos;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using Order = Core.Entities.Order;

namespace API.Controllers;

public class PaymentController : BaseApiController
{
    private readonly IConfiguration _configuration;
    private readonly ICartRepository _cartRepo;


    private readonly StripeSettings _settings;
    private static string s_wasmCLientURL = string.Empty;

    public PaymentController(IOptions<StripeSettings> settings, IConfiguration configuration, ICartRepository cartRepo)
    {
        _configuration = configuration;
        _cartRepo = cartRepo;


        _settings = settings.Value;
        //StripeConfiguration.ApiKey = "sk_test_51L71r8CHLmgQslOwRa3nsVtzU5I7plqWQWBGh2hpVNFirKzDJfE4f4zohlmivq06VV4VQrqcKwLfcdlV33x2jBRu00LKbZl8Zu";
    }


    [HttpPost("checkout/{cartId}")]
    public async Task<ActionResult> CheckoutOrder(int cartId, [FromServices] IServiceProvider sp)
    {
        
        
        //var referer = Request.Headers.Referer;

        Cart cart = new Cart();
        cart = _cartRepo.GetCartById(cartId).Result;
        Console.WriteLine(cart);
        ICollection<Track> tracks = new List<Track>();
        List<CreateCheckoutSessionResponse> checkoutSessionResponses = new List<CreateCheckoutSessionResponse>();
        //s_wasmCLientURL = referer[0];
        var server = sp.GetRequiredService<IServer>();
        var serverAddressFeature = server.Features.Get<IServerAddressesFeature>();
        string? thisApiUrl = null;

        if (serverAddressFeature is not null)
        {
            thisApiUrl = serverAddressFeature.Addresses.FirstOrDefault();
            
        }
        
            if (thisApiUrl is not null)
            {
            
                var sessionId = await CheckoutOut(cart, thisApiUrl);
                var pubKey = _configuration["StripeSettings:PublicKey"];

                var checkoutOrderResponse = new CreateCheckoutSessionResponse()
                {
                    SessionId = sessionId,
                    PublicKey = _settings.PublicKey

                };
                //return Ok(checkoutOrderResponse);
                checkoutSessionResponses.Add(checkoutOrderResponse);
            }
            
        

        if (checkoutSessionResponses.Count > 0)
        {
            return Ok(checkoutSessionResponses[0]);
        }
        else
        {
            return StatusCode(500);
        }

        
        


    }

    
    [NonAction]
    public async Task<string> CheckoutOut(Cart cart, string thisApiUrl)
    {
        
        var options = new SessionCreateOptions
        {
            SuccessUrl = $"{thisApiUrl}/api/payment/success?sessionId" + "{CHECKOUT_SESSION_ID}",
            //CancelUrl = s_wasmCLientURL + "failed",
            CancelUrl = "https://example.com/canceled.html",
            PaymentMethodTypes = new List<string>
            {
                "card"
            },
            LineItems = new List<SessionLineItemOptions>
            {
                new ()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        UnitAmount = (long)cart.Subtotal*100,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = cart.FirstName + " " + cart.LastName
                            
                        },
                    },
                    Quantity = 1,
                },
            },
            Mode = "payment"
        };
        var service = new SessionService();
        
        var session = await service.CreateAsync(options);
        return session.Id;
    }




    [HttpGet("success")]
    public ActionResult CheckoutSuccess(string sessionId)
    {
        var sessionService = new SessionService();
        var session = sessionService.Get(sessionId);

        //var total = session.AmountTotal.Value;
        //var customerEmail = session.CustomerDetails.Email;
        Console.WriteLine(session);
        return Ok(session);
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
        


    }
 
}