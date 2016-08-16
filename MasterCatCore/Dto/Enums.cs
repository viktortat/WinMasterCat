using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterCatCore.Dto
{
    public enum CompanyType
    {
        Distributor = 1,
        Outlet = 2,
        Operator = 4,
        Manufacturer = 8,
        TradingNetwork = 16,
        Store = 32,
        InetStore = 64
    }
    public enum EnumObjectsState
    {
        Draft = 0,
        Active = 1,
        Deleted = -100
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
