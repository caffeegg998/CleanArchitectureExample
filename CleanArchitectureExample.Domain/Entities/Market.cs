using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Entities
{
    public class Market
    {
        public Market()
        {
            this.Products = new List<Product>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MarketId { get; set; }
        public string MarketName { get; set; }
        public int ShippingPartnerId { get; set; }
        public ShippingPartner ShippingPartner { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
