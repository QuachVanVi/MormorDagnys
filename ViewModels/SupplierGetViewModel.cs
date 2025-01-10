
namespace MormorsBageri.ViewModels;

    public class SupplierGetViewModel
    {
       
        public string SupplierName { get; set; }
        public string SupplierPhone { get; set; }  
        public string SupplierEmail { get; set; } 
        public string ContactPerson { get; set; }
        

        public IList<SupplierProductViewModel> SupplierProduct { get; set; }
    }
