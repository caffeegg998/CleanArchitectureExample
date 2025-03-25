using CleanArchitectureExample.Application.DTOs;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Features.Commands
{
    public class RequestShippingCommand
    {
        public class CreateRequestShippingCommand : IRequest<RequestShippingDTO>
        {
            [Required]
            public DateTime NgayChotDon { get; set; }

            [JsonIgnore]
            public string CreatedBy { get; set; } = string.Empty;
            [Required]
            public Recipient Recipient { get; set; } = new();

            [Required]
            public int ProductId { get; set; }
            [Required]
            public int PageId { get; set; }

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
            public int Quantity { get; set; }
            [Required]
            public string SendMethod { get; set; }
            public string Note { get; set; }
            public string DateSend { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"); // Format chuẩn
            public int ShippingPartnerId { get; set; }


        }
        public class UpdateRequestShippingCommand : IRequest<RequestShippingDTO>
        {
            [Required]
            public RequestShippingDTO requestShippingDTO { get; set; }


        }
    }
}
