using CleanArchitectureExample.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Entities
{
    public class Request
    {
        public Guid Id { get; set; }
        public RequestType RequestType { get; set; }
        public string RequestName { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public double BoxQty { get; set; }
        public int UserCreate { get; set; }
        public int UserConfirm {  get; set; }
    }
}
