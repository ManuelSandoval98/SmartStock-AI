using MediatR;
using SmartStock_AI.Application.Categories.DTOs;
using SmartStock_AI.Application.UnitOfWork;

namespace SmartStock_AI.Application.Categories.Queries;

public record GetAllCategoriesQuery() : IRequest<List<CategoryDto>>;

public class GetAllCategoriesHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
{
    public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categorias = await _unitOfWork.CategoryRepository.GetAllAsync();

        var categoriasDto = categorias.Select(c => new CategoryDto
        {
            Id = c.Id,
            Nombre = c.Nombre
        }).ToList();

        return categoriasDto;
    }
}