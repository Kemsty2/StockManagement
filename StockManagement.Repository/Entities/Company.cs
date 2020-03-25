using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StockManagement.Repository.Entities
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }
        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public bool Status { get; set; }

        public ICollection<Order> Order { get; set; } = new List<Order>();
    }
}
