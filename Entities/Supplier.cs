

namespace MormorsBageri.Entities;

    public class Supplier
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPhone { get; set; }  
        public string SupplierEmail { get; set; } 
        public string ContactPerson { get; set; }

        public IList<SupplierProduct> SupplierProducts {get; set;}
 
        
    }
