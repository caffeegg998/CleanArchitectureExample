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
    public class ShippingInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShippingInfoId { get; set; }
        public string SendMethod { get; set; }
        public string TimeReceived { get; set; }
        public string Note { get; set; }
        public DateTime DateSend { get; set; }
        public string TrackingNumber { get; set; }
        public RequestShippingStatusEnum Status { get; set; }
        public int ShippingPartnerId { get; set; }
        public ShippingPartner ShippingPartner { get; set; }
    }
}
