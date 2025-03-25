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
    public class Page
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string PageLink { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string CreateBy { get; set; }
        public UserProfile Creator { get; set; }
        public DateTime CreateAt { get;set; }
        public List<PageSale> PageSales { get; set; }
    }
}
