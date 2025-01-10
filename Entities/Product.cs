namespace MormorsBageri.Entities;

    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string ItemNumber { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

         public IList<SupplierProduct> SupplierProducts {get; set;}
        //  public IList<Supplier> Supplier { get; set; }
         public Supplier Supplier { get; set; }
        
        
    }
