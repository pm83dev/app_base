using backend.factory.DTOs;
using backend.repository.DataManager;
using backend.repository.Model;
namespace backend.factory.ServiceManager;

public class DataBLL
{
    private readonly DataDLL _dataRepository;

    public DataBLL(DataDLL dataRepository)
    {
        _dataRepository = dataRepository;
    }

    public async Task<DataModelDtoList> GetDataAsync()
    {
        var dataEntityList = await _dataRepository.GetDataAsync();
        var dataModelDtoList = dataEntityList.Data.Select(MapToDto).ToList();
        return new DataModelDtoList { Data = dataModelDtoList };
    }

    public async Task<DataModelDto> AddDataAsync(DataModelDto data)
    {
        var entity = MapToEntity(data);
        var addedEntity = await _dataRepository.AddDataAsync(entity);
        return MapToDto(addedEntity);
    }

    public async Task<DataModelDto> UpdateDataAsync(int id, DataModelDto data)
    {
        var entity = MapToEntity(data);
        var updatedEntity = await _dataRepository.UpdateDataAsync(id, entity);
        return MapToDto(updatedEntity);
    }

    public async Task<bool> DeleteDataAsync(int id)
    {
        return await _dataRepository.DeleteDataAsync(id);
    }

    //Helper metodo per mappare tra Data e DataDto
    private DataModelDto MapToDto(DataEntity entity)
    {
        return new DataModelDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Value = entity.Value
        };
    }

    // Helper metodo per mappare tra DataDto e Data
    private DataEntity MapToEntity(DataModelDto dto)
    {
        return new DataEntity
        {
            Id = dto.Id,
            Name = dto.Name,
            Value = dto.Value
        };
    }
}