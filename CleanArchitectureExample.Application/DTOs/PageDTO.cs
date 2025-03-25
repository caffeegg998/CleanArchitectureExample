using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureExample.Domain.Entities.Identity;
using CleanArchitectureExample.Domain.Entities;

namespace CleanArchitectureExample.Application.DTOs
{
    public class PageDTO
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string PageLink { get; set; }
    }
}
