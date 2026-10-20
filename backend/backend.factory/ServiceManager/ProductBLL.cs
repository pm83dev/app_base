using backend.factory.DTOs;
using backend.repository.DataManager;
using backend.repository.Model;

namespace backend.factory.ServiceManager;

public class ProductBLL
{
    private readonly ProductDLL _productRepository;

    public ProductBLL(ProductDLL productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductModelDtoList> GetProductsAsync()
    {
        var productEntityList = await _productRepository.GetProductsAsync();
        var productModelDtoList = productEntityList.Products.Select(MapToDto).ToList();
        return new ProductModelDtoList { Products = productModelDtoList };
    }

    public async Task<ProductModelDto> AddProductAsync(ProductModelDto product)
    {
        var entity = MapToEntity(product);
        var addedEntity = await _productRepository.AddProductAsync(entity);
        return MapToDto(addedEntity);
    }

    public async Task<ProductModelDto> UpdateProductAsync(int id, ProductModelDto product)
    {
        var entity = MapToEntity(product);
        var updatedEntity = await _productRepository.UpdateProductAsync(id, entity);
        return MapToDto(updatedEntity);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        return await _productRepository.DeleteProductAsync(id);
    }

    // Helper metodo per mappare tra ProductEntity e ProductModelDto
    private ProductModelDto MapToDto(ProductEntity entity)
    {
        return new ProductModelDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Price = entity.Price
        };
    }

    // Helper metodo per mappare tra ProductModelDto e ProductEntity
    private ProductEntity MapToEntity(ProductModelDto dto)
    {
        return new ProductEntity
        {
            Id = dto.Id,
            Name = dto.Name,
            Price = dto.Price
        };
    }
}
