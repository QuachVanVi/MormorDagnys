

namespace MormorsBageri.Entities;

    public class SupplierProduct
    {
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public string ItemNumber { get; set; }
        public double Price { get; set; } 
        public int Stock { get; set; }  
        
        public Supplier Supplier { get; set; } 

        public Product Product  { get; set; }
        
    }
