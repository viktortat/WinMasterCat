using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterCatCore.Dto
{
    [System.Data.Linq.Mapping.Table(Name = "Company")]
    public class Company
    {
        [System.Data.Linq.Mapping.Column(Name = "id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }


        [ForeignKey("ParentId")]
        public virtual Company Parent { get; set; }
        public virtual int? ParentId { get; set; }

        public string Code { get; set; }

        public string GlnCode { get; set; }

        public string OkpoCode { get; set; }

        public virtual CompanyType Type { get; set; }
        public string ContactPhone { get; set; }

        public string WebSite { get; set; }

        public string ContactEmail { get; set; }

        public Guid OId { get; set; }

        public EnumObjectsState State { get; set; }

        public virtual List<Company> Children { get; set; }
    }

    public class CompanyLoc 
    {
        [Column(Order = 0), Key, ForeignKey("Company")]
        public int Id { get; set; }
        [Column(Order = 1, TypeName = "char"), Key, MaxLength(2), ForeignKey("Language")]
        public string Lang { get; set; }

        public Language Language { get; set; }

        public Company Company { get; set; }
        //update-database -verbose
        public string Name { get; set; }
        public string JurName { get; set; }
        public string Description { get; set; }

    }
    public class Language
    {
        [Column(Order = 1, TypeName = "char"), Key, MaxLength(2)]
        public string Code { get; set; }

        public string NativeName { get; set; }

        public string IntName { get; set; }

        public string CultureName { get; set; }
        public bool? IsUseInRelease { get; set; }
    }
    
}