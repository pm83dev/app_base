namespace backend.repository.Model;

public class ProductEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class ProductEntityList
{
    public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}
