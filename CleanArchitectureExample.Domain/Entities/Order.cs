using CleanArchitectureExample.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureExample.Domain.Enums;

namespace CleanArchitectureExample.Domain.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get;set; }
        public int PageId { get; set; }
        public Page Page { get; set; }
        public string SaleUserId { get; set; }
        public UserProfile SaleUser { get; set; }
        public int MarketId { get; set; }
        public Market Market { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ShippingPartnerId { get; set; }
        public ShippingPartner ShippingPartner { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
