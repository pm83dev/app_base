namespace backend.factory.DTOs;

public class ProductModelDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class ProductModelDtoList
{
    public List<ProductModelDto> Products { get; set; } = new List<ProductModelDto>();
}
