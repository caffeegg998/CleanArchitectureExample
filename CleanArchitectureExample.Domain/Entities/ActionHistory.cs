using CleanArchitectureExample.Domain.Entities.Identity;
using CleanArchitectureExample.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Entities
{
    public class ActionHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActionHistoryId { get; set; }
        public ActionEnum ActionName { get; set; }
        public string UserProfileUserId { get; set; }
        public UserProfile UserAction { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Note { get; set; }
        public int ShippingInfoId { get; set; }
        public ShippingInfo ShippingInfo { get; set; }
    }
}
