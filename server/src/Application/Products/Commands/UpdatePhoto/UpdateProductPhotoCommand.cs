using Application.Common.Interfaces.Messaging;
using Microsoft.AspNetCore.Http;

namespace Application.Products.Commands.UpdatePhoto;
public sealed record UpdateProductPhotoCommand(Guid ProductId, IFormFile Photo) : ICommand<Guid>;
