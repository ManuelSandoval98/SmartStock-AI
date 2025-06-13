using MediatR;
using SmartStock_AI.Application.Products.Commands;

namespace SmartStock_AI.Application.Products.Requests;

public record PatchProductRequest(int Id, PatchProductCommand Command) : IRequest<Unit>;