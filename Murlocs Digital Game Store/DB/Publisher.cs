namespace DB; 

public class Publisher {
    public int Publisher_ID { get; set; }
    public string? Name { get; set; }
    
    public ICollection<Product>? Products { get; set; }
}