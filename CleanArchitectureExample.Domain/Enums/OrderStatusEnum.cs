using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Enums
{
    public enum OrderStatusEnum
    {
        Pending, //Đơn hàng đang chờ xử lý.
        Processed, //Đơn hàng đã được xử lý, đang trong quá trình vận chuyển.
        Shipped, //Đơn hàng đã được gửi đi.
        Delivered, //Đơn hàng đã được giao.
        Cancelled, //Đơn hàng đã bị hủy.
        Returned, //Đơn hàng đã được trả lại.
    }
}
