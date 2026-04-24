namespace backend.factory.DTOs;

public class DataModelDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}

public class DataModelDtoList
{
    public List<DataModelDto> Data { get; set; } = new List<DataModelDto>();
}