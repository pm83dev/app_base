using backend.repository.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.repository.DataManager;

public class ProductDLL
{
    private readonly AppDbContext _context;
    public ProductDLL(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProductEntity> AddProductAsync(ProductEntity entity)
    {
        try
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here as needed
            throw new Exception($"Error adding product: {ex.Message}");
        }
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        try
        {
            var entity = await _context.Products.FindAsync(id);
            if (entity == null)
                return false;

            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here as needed
            throw new Exception($"Error deleting product: {ex.Message}");
        }
    }

    public async Task<ProductEntityList> GetProductsAsync()
    {
        try
        {
            var result = await _context.Products.ToListAsync();
            return new ProductEntityList { Products = result };
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here as needed
            throw new Exception($"Error retrieving products: {ex.Message}");
        }
    }

    public async Task<ProductEntity> UpdateProductAsync(int id, ProductEntity entity)
    {
        try
        {
            var existingEntity = await _context.Products.FindAsync(id);
            if (existingEntity == null)
                return null;

            existingEntity.Name = entity.Name;
            existingEntity.Price = entity.Price;

            await _context.SaveChangesAsync();
            return existingEntity;
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here as needed
            throw new Exception($"Error updating product: {ex.Message}");
        }
    }
}
