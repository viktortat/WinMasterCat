using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterCatCore.Dto
{
    public class Brand
    {
        [System.Data.Linq.Mapping.Column(Name = "id", IsDbGenerated = true, IsPrimaryKey = true)]
        public int id { get; set; }
        public string Code { get; set; }
        public int State { get; set; }
        public string Logo { get; set; }
    }

    public class BrandLoc
    {
        [Column(Order = 0), Key, ForeignKey("Brand")]
        public int id { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public string Logo { get; set; }
    }
}