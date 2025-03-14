using CleanArchitectureExample.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Entities
{
    public class RequestShipping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestShippingId { get; set; }
        public DateTime NgayChotDon { get; set; }
        public string CreateBy { get; set; }
        public UserProfile UserProfile { get; set; }
        public int RecipientId { get; set; }
        public Recipient Recipient { get; set; }
        public int ProductId { get; set; }
        public Product Product{ get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }

        public int ShippingInfoId { get; set; }
        public ShippingInfo ShippingInfo { get; set; }
        
        public string Status { get; set; }
        public DateTime NgayDoiSoat { get; set; }
    }
}
