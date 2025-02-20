
namespace MormorsBageri.ViewModels.Order;

public class OrderBaseViewModel
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string ProductName { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public DateTime OrderDate { get; set; }
    

}
