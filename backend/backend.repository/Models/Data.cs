namespace backend.repository.Model;

public class DataItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}

public class DataList
{
    public List<DataItem> Data { get; set; } = new List<DataItem>();

}