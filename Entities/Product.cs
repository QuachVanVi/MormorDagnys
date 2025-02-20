using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MormorsBageri.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string ItemNumber { get; set; }
    public int QuantityPerPackage { get; set; }
    public string Weight { get; set; }
    public double Price { get; set; }

    public DateTime BestBeforeDate { get; set; }
    public DateTime ProductionDate { get; set; }



    // public int DateId { get; set; }
    // public Date Date { get; set; }
    public IList<SupplierProduct> SupplierProducts { get; set; }

    [JsonIgnore]
    public IList<Order> Orders { get; set; }

}
