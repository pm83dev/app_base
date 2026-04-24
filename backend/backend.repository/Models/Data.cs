namespace backend.repository.Model;

public class DataEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}

public class DataEntityList
{
    public List<DataEntity> Data { get; set; } = new List<DataEntity>();

}