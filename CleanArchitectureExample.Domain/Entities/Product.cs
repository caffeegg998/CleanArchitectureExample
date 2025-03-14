using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Entities
{
    public class Product
    {
        public Product()
        {
            this.Markets = new List<Market>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string CreateAt { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<Market> Markets { get; set; }
    }
}
