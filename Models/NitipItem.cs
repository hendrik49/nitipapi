using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nitipApi.Models
{
    public class NitipItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public int IdUser { get; set; }
        public int IdProduct { get; set; }
        public bool IsComplete { get; set; }
    }
}