using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.WebApplication.Models
{
    [Table("peoples")]
    public class People
    {
        [Key()]
        public int Id { get; set; }

        [Required()]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required()]
        [MaxLength(100)]
        public string PasswordHashed { get; set; }
                
        [MaxLength(100)]
        public string PasswordSalt { get; set; }
    }
}
