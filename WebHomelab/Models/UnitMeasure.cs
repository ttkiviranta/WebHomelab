using System;
using System.Collections.Generic;

namespace WebHomelab.Models
{
    public partial class UnitMeasure
    {
        public UnitMeasure()
        {
            AuditSizeUnitMeasureCodeNavigation = new HashSet<Audit>();
            AuditWeightUnitMeasureCodeNavigation = new HashSet<Audit>();
            ProductSizeUnitMeasureCodeNavigation = new HashSet<Product>();
            ProductWeightUnitMeasureCodeNavigation = new HashSet<Product>();
        }

        public string UnitMeasureCode { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<Audit> AuditSizeUnitMeasureCodeNavigation { get; set; }
        public ICollection<Audit> AuditWeightUnitMeasureCodeNavigation { get; set; }
        public ICollection<Product> ProductSizeUnitMeasureCodeNavigation { get; set; }
        public ICollection<Product> ProductWeightUnitMeasureCodeNavigation { get; set; }
    }
}
