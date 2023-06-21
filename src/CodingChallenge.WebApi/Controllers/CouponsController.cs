using System.Net.Mime;
using System.Text;
using System.Text.Json;
using CodingChallenge.Application.Commands.CreateOrUpdateCoupon;
using CodingChallenge.Application.Queries.GetCouponByCode;
using CodingChallenge.Application.Queries.GetCouponById;
using CodingChallenge.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace CodingChallenge.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CouponsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CouponsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("")]
    [OutputCache]
    public async Task<ActionResult<Coupon>> GetCouponByCouponCode(string couponCode)
    {
        var query = new GetCouponByCodeQuery(couponCode);
        var queryResponse = await _mediator.Send(query);
        var response = new Coupon(
            queryResponse.Id,
            queryResponse.Name,
            queryResponse.Description,
            queryResponse.Code,
            (double)queryResponse.Price,
            queryResponse.MaxUsages,
            queryResponse.Usages,
            queryResponse.ProductCodes.ToArray());

        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [OutputCache]
    public async Task<ActionResult<Coupon>> GetCoupon(Guid id)
    {
        var query = new GetCouponByIdQuery(id);
        var queryResponse = await _mediator.Send(query);
        var response = new Coupon(
           queryResponse.Id,
           queryResponse.Name,
           queryResponse.Description,
           queryResponse.Code,
           (double)queryResponse.Price,
           queryResponse.MaxUsages,
           queryResponse.Usages,
           queryResponse.ProductCodes.ToArray());

        return Ok(response);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> CreateOrUpdateCoupon(Guid id, [FromBody] Coupon request)
    {
        // iivc comment:
        // I can't change APIs per requirenments, and I don't know how the 'Automation Tests' from Domino's
        // team works so I assume the most strict scenario - id from the rout and body
        // must be equal and used during creation/update.
        // I personaly would split api into two separate: one for create (without any sort of ID in rout or body)
        // and another for update with mandatory id in the rout and no id in the body. 
        if (id != request.Id)
        {
            return BadRequest($"Rout id '{id}' must be the same as body id '{request.Id}'");
        }

        var command = new CreateOrUpdateCouponCommand(
           request.Id,
           request.Name,
           request.Description,
           request.CouponCode,
           (decimal)request.Price,
           request.MaxUsages,
           request.Usages,
           request.ProductCodes);

        var commandResponse = await _mediator.Send(command);

        var response = new Coupon(
           commandResponse.Id,
           commandResponse.Name,
           commandResponse.Description,
           commandResponse.Code,
           (double)commandResponse.Price, // iivc comment: I am 99.9% sure if I change Price to decimail it will not break Domino's tests, but lets keep it original as double
           commandResponse.MaxUsages,
           commandResponse.Usages,
           commandResponse.ProductCodes.ToArray());

        return commandResponse.Status switch
        {
            CreateOrUpdateCouponCommandResponseStatus.Created => Created($"/coupons/{response.Id}", response),
            CreateOrUpdateCouponCommandResponseStatus.Updated => Ok(response),
            _ => BadRequest($"Unknown status '{commandResponse.Status}'!")
        };
    }

    #region All code below is not going to be reviewed. This is just some helpers to load the data.

    private const string DataFileName = @"data.json";

    /// <summary>
    /// This endpoint is created so that you can easily populate your data.
    /// By doing it like this, we simulating the user to call the CreateOrUpdateCoupon endpoint.
    /// You can do it any other way you like. We did it like this so that you are not forced in any direction on how you will store your data.
    /// If you leave it like this, make sure you run this endpoint every time you start up though.
    /// </summary>
    [HttpGet]
    [Route("Test/[action]")]
    public async Task<ActionResult> Populate()
    {
        var data = await System.IO.File.ReadAllTextAsync(DataFileName);
        var coupons = JsonSerializer.Deserialize<List<Coupon>>(data);
        
        var httpClient = new HttpClient();
        foreach (var coupon in coupons)
        {
            var url = $"https://localhost:{Request.Host.Port}/Coupons/{coupon.Id}";
            var body = new StringContent(JsonSerializer.Serialize(coupon), Encoding.UTF8, MediaTypeNames.Application.Json);

            await httpClient.PutAsync(url, body);
        }

        return Ok();
    }

    #endregion
}