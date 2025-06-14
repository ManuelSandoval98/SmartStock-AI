using MediatR;
using SmartStock_AI.Application.Categories.DTOs;
using SmartStock_AI.Application.UnitOfWork;
using SmartStock_AI.Application.UnitOfWork.Negocio;

namespace SmartStock_AI.Application.Categories.Queries;

public record GetAllCategoriesQuery() : IRequest<List<CategoryDto>>;

public class GetAllCategoriesHandler(INegocioUnitOfWork _negocioUnitOfWork) : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
{
    public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categorias = await _negocioUnitOfWork.CategoryRepository.GetAllAsync();

        var categoriasDto = categorias.Select(c => new CategoryDto
        {
            Id = c.Id,
            Nombre = c.Nombre
        }).ToList();

        return categoriasDto;
    }
}