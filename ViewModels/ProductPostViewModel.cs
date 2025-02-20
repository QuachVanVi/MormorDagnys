

namespace MormorsBageri.ViewModels;

public class ProductPostViewModel
{
  public string ItemNumber { get; set; }
  public string ProductName { get; set; }
  public double Price { get; set; }
  public int QuantityPerPackage { get; set; }
  public string Weight { get; set; }
   public DateTime BestBeforeDate { get; set; }
    public DateTime ProductionDate { get; set; }

}
