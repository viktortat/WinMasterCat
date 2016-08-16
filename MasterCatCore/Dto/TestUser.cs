using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinMasterCat
{
    [Table("Tabl14")] 
    public class TestUser
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string SName { get; set; }
        public int? Age { get; set; }
    }
}