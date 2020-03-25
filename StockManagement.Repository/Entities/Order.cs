using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StockManagement.Repository.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }       
        public int CompanyId { get; set; }
    }
}
