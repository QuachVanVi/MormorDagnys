
using MormorsBageri.Entities;

namespace MormorsBageri.ViewModels;

    public class ProductGetViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string ItemNumber { get; set; }

        public IList<SupplierProductViewModel> SupplierProduct { get; set; }
    }
