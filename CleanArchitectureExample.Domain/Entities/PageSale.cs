using CleanArchitectureExample.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Entities
{
    public class PageSale
    {
        public int PageId { get; set; }
        public Page Page { get; set; }
        public string SaleUserId { get; set; }
        public UserProfile UserProfile { get; set; }

    }
}
