using System;
using System.Collections.Generic;

namespace WebHomelab.Models
{
    public partial class ProductSubcategory
    {
        public ProductSubcategory()
        {
            Audit = new HashSet<Audit>();
            Product = new HashSet<Product>();
        }

        public int ProductSubcategoryId { get; set; }
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ProductCategory ProductCategory { get; set; }
        public ICollection<Audit> Audit { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
