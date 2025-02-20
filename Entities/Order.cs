
using System.Text.Json.Serialization;

namespace MormorsBageri.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double Total => Math.Round(Price * Quantity, 2);
    
    public int ProductId { get; set; }
    [JsonIgnore]
    public Product Product { get; set; }

    public int CustomerId { get; set; }
    [JsonIgnore]
    public Customer Customer { get; set; }
}
