using backend.repository.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.repository.DataManager;

public class DataDLL
{
    private readonly AppDbContext _context;
    public DataDLL(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DataEntity> AddDataAsync(DataEntity entity)
    {
        try
        {
            _context.Data.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here as needed
            throw new Exception($"Error adding data: {ex.Message}");
        }
    }

    public async Task<bool> DeleteDataAsync(int id)
    {
        try
        {
            var entity = await _context.Data.FindAsync(id);
            if (entity == null)
                return false;

            _context.Data.Remove(entity);
            await _context.SaveChangesAsync();
            return true;

        }
        catch (Exception ex)
        {
            // Log the exception (ex) here as needed
            throw new Exception($"Error deleting data: {ex.Message}");
        }
    }

    public async Task<DataEntityList> GetDataAsync()
    {
        try
        {
            var result = await _context.Data.ToListAsync();  // List<DataItem> dal DB
            return new DataEntityList { Data = result };                   // wrappa nel contenitore
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here as needed
            throw new Exception($"Error retrieving data: {ex.Message}");
        }
    }

    public async Task<DataEntity> UpdateDataAsync(int id, DataEntity entity)
    {
        try
        {
            var existingEntity = await _context.Data.FindAsync(id);
            if (existingEntity == null)
                return null;

            existingEntity.Name = entity.Name;
            existingEntity.Value = entity.Value;

            await _context.SaveChangesAsync();
            return existingEntity;
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here as needed
            throw new Exception($"Error updating data: {ex.Message}");
        }
    }
}