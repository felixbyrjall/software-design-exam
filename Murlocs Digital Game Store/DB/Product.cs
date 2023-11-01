
namespace DB; 

public class Product {
    
    public int Product_Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public int Price { get; set; }
    public string Date { get; set; } = string.Empty;

    public int PublisherID { get; set; }
    public Publisher? Publisher { get; set; }
}