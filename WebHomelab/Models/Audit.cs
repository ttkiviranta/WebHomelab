﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebHomelab.Models
{
    public partial class Audit
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long AuditId { get; set; }
        public string Name { get; set; }

        [Display(Name = "Pruduct number")]
        public string ProductNumber { get; set; }

        [Display(Name = "Make flag")]
        public bool? MakeFlag { get; set; }

        [Display(Name = "Finished goods flag")]
        public bool? FinishedGoodsFlag { get; set; }

        public string Color { get; set; }

        [Display(Name = "Safety stock Level")]
        public short SafetyStockLevel { get; set; }

        [Display(Name = "Reorder point")]
        public short ReorderPoint { get; set; }

        [Display(Name = "Standard cost")]
        // [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        public decimal StandardCost { get; set; }

        [Display(Name = "List price")]
        // [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        public decimal ListPrice { get; set; }

        public string Size { get; set; }

        [Display(Name = "Size unit measure code")]
        public string SizeUnitMeasureCode { get; set; }

        [Display(Name = "Weight unit measure code")]
        public string WeightUnitMeasureCode { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? Weight { get; set; }

        [Display(Name = "Days to manufacture")]
        public int DaysToManufacture { get; set; }

        [Display(Name = "Product line")]
        public string ProductLine { get; set; }

        public string Class { get; set; }

        public string Style { get; set; }

        [Display(Name = "Product subcategory ID")]
        public int? ProductSubcategoryId { get; set; }

        [Display(Name = "Product model ID")]
        public int? ProductModelId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Sell start date      ")]
        [StringLength(20)]
        public DateTime SellStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Sell end date      ")]
        [StringLength(20)]
        public DateTime? SellEndDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Discontinued date")]
       
        public DateTime? DiscontinuedDate { get; set; }

        public Guid Rowguid { get; set; }

        [DataType(DataType.Date)]
   //     [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Modified date")]
        [StringLength(20)]
        public DateTime ModifiedDate { get; set; }


        [Display(Name = "Modified by")]
        public string UserIdentifier { get; set; }

        [Display(Name = "Product model")]
        public ProductModel ProductModel { get; set; }

        [Display(Name = "Product subcategory")]
        public ProductSubcategory ProductSubcategory { get; set; }

        [Display(Name = "Size unit measure code navigation")]
        public UnitMeasure SizeUnitMeasureCodeNavigation { get; set; }

        [Display(Name = "Weight unit measure code navigation")]
        public UnitMeasure WeightUnitMeasureCodeNavigation { get; set; }
    }
}
