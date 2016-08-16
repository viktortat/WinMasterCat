using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterCatCore.Dto
{
    public class  Product
    {
        public int Id { get; set; }
        public string InternalCode { get; set; }

        //public string Description { get; set; }

        public string DefaultImage { get; set; }

        public virtual Guid OId { get; set; }

        public virtual int? FamilyId { get; set; }
        public int UnitId { get; set; }
        public virtual List<ProductLoc> ProductsLoc { get; set; }
        public EnumObjectsState State { get; set; }
        public DateTimeOffset ModifyDate { get; set; }

    }

    public class ProductLoc 
    {
        [Column(Order = 0), Key, ForeignKey("Product")]
        public int Id { get; set; }

        [Column(Order = 1, TypeName = "char"), Key, MaxLength(2), ForeignKey("Language")]
        public string Lang { get; set; }

        public Language Language { get; set; }

        public Product Product { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string DefaultImage { get; set; }

        public string FullName { get; set; }
    }


    public class ProductProp
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        public string PropertyVal { get; set; }
    }
}
