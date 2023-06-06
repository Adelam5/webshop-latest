using Api.Contracts.Product;
using Application.Products.Commands.Create;
using Application.Products.Commands.Delete;
using Application.Products.Commands.Update;
using Application.Products.Commands.UpdatePhoto;
using Application.Products.Queries.GetById;
using Application.Products.Queries.List;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ProductsController : ApiController
{
    [HttpGet]
    public async Task<ActionResult> List(CancellationToken cancellationToken)
    {
        return HandleResult(await Mediator.Send(new ListProductsQuery(), cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        return HandleResult(await Mediator.Send(new GetProductByIdQuery(id), cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateProductRequest newProduct, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<CreateProductCommand>(newProduct);

        return HandleResult(await Mediator.Send(command, cancellationToken));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateProductRequest product, CancellationToken cancellationToken)
    {
        product.Id = id;
        var command = Mapper.Map<UpdateProductCommand>(product);

        return HandleResult(await Mediator.Send(command, cancellationToken));
    }

    [HttpPut("{id}/photo")]
    public async Task<ActionResult> UpdatePhoto(Guid id, IFormFile photo, CancellationToken cancellationToken)
    {
        return HandleResult(await Mediator.Send(
            new UpdateProductPhotoCommand(id, photo), cancellationToken));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Remove(Guid id, CancellationToken cancellationToken)
    {
        return HandleResult(await Mediator.Send(new DeleteProductCommand(id), cancellationToken));
    }
}
