using System;
using System.Collections.Generic;

namespace WebHomelab.Models
{
    public partial class ProductModel
    {
        public ProductModel()
        {
            Audit = new HashSet<Audit>();
            Product = new HashSet<Product>();
        }

        public int ProductModelId { get; set; }
        public string Name { get; set; }
        public string CatalogDescription { get; set; }
        public string Instructions { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<Audit> Audit { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
