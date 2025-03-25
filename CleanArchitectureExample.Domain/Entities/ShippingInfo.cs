using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureExample.Domain.Enums;
using CleanArchitectureExample.Domain.Entities.Identity;

namespace CleanArchitectureExample.Domain.Entities
{
    public class ShippingInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShippingInfoId { get; set; }
        public string SendMethod { get; set; }
        public string? TimeReceived { get; set; }
        public string? Note { get; set; }
        public string DateSend { get; set; }
        public string? TrackingNumber { get; set; }
        public RequestShippingStatusEnum Status { get; set; } = RequestShippingStatusEnum.Pending;
        public List<ActionBy> actionBies { get; set; }
        public int? ShippingPartnerId { get; set; }
        public ShippingPartner ShippingPartner { get; set; }
    }
    public class ActionBy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActionId { get; set; }
        public ActionEnum ActionName { get; set; }
        public string UserProfileUserId { get; set; }
        public UserProfile UserAction { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int ShippingInfoId { get; set; }
        public ShippingInfo ShippingInfo { get; set; }
    }
}
