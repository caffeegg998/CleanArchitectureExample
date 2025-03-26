using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Enums
{
    public enum ActionEnum
    {
        ShippingInfo_Update, //Đơn hàng đang chờ xử lý.
        RequestShipping_Create, //Đơn hàng đã được xử lý, đang trong quá trình vận chuyển.
        RequestShipping_Update, //Đơn hàng đã được gửi đi.
        Page_Create, //Đơn hàng đã được giao.
        Page_Update, //Đơn hàng đã bị hủy.
        Unknows, //Đơn hàng đã được trả lại.
    }
}
